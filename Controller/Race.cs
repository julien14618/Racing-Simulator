using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Runtime.InteropServices;

namespace Controller
{
    public class Race
    {
        [DllImport("msvcrt.dll")]
        static extern bool system(string str);

        private Timer timer;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public event EventHandler<RaceFinishedEventArgs> RaceFinished;

        private int amountOfRounds = 2;
        public Dictionary<Driver, int> roundsPerDriver = new Dictionary<Driver, int>();

        public Race(Track track, List<IParticipant> participants)
        {
            Participants = participants;
            Track = track;
            timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;
            _random = new Random(DateTime.Now.Millisecond);
            Data.Competition.InitializeBrokenEquipmentAndOvertakenRecord(participants);
        }

        public void Start()
        {
            timer.Start();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
            foreach (Section s in Data.CurrentRace.Track.Sections)
            {
                Data.CurrentRace.GetSectionData(s);
            }
            for (int i = 0; i < Data.CurrentRace.Track.Sections.Count; i++)
            {
                _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(i), out SectionData sd);
                roundsPerDriver.TryAdd((Driver)sd.Left, 0);
                roundsPerDriver.TryAdd((Driver)sd.Right, 0);
            }
        }

        public void DisposeEventHandlers()
        {
            DriversChanged = null;
            RaceFinished = null;
        }

        private bool startnew;

        public void CheckCleanUpAndStartNewRace()
        {
            startnew = true;
            for (int i = 0; i < Data.CurrentRace.Track.Sections.Count; i++)
            {
                _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(i), out SectionData sd);
                if (sd.Left.Name.Length > 0 || sd.Right.Name.Length > 0)
                    startnew = false;
            }
            if (startnew)
            {
                
                timer.Stop();
                DriversChanged = null;
                //Add points to list and reset every round
                List<IParticipant> pBuffer = Data.Competition.Participants.FindAll(p => p.Name.Length > 0);
                Data.Competition.FillRecordAndEndRace(pBuffer);
                Data.Competition.Participants.ForEach(p => p.Points = 0);

                Data.NextRace();
                if (Data.CurrentRace == null)
                {
                    Data.Competition.printResults();
                    Data.Competition.PrintBrokenEquipmentResults();
                    Data.Competition.PrintDriversOvertakenResults();
                    //system("pause");
                    
                }
                else
                {
                    RaceFinished?.Invoke(this, new RaceFinishedEventArgs(pBuffer));
                    
                }
            }
        }

        public bool IsBrokenOrFixed(string breakOrFix)
        {
            Random r = new Random();
            int brokenOrFixedInt = 0;
            if (breakOrFix == "Break")
                brokenOrFixedInt = 10;
            if (breakOrFix == "Fix")
                brokenOrFixedInt = 20;
            if (r.Next(0, 100) < brokenOrFixedInt)
                return true;
            return false;
        }

        private int positionFinished = 1;

        public void AwardPoints(SectionData sd, string leftOrRight)
        {
            int maxPoints = 100;

            if (leftOrRight.Equals("Left"))
            {
                sd.Left.Points += (maxPoints / positionFinished);
                positionFinished++;
            }
            if (leftOrRight.Equals("Right"))
            {
                sd.Right.Points += (maxPoints / positionFinished);
                positionFinished++;
            }
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < Data.CurrentRace.Track.Sections.Count; i++)
            {
                _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(i), out SectionData sd);
                if (sd.Left.Equipment.IsBroken)
                    if (IsBrokenOrFixed("Fix"))
                    {
                        sd.Left.Equipment.IsBroken = false;
                    }
                if (sd.Right.Equipment.IsBroken)
                    if (IsBrokenOrFixed("Fix"))
                    {
                        sd.Right.Equipment.IsBroken = false;
                    }
                int setAtNextSection = i + 1;
                if (setAtNextSection == Data.CurrentRace.Track.Sections.Count)
                    setAtNextSection = 0;
                _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection), out SectionData sd1);
                if (sd.Left.Name.Length > 0 && !sd.Left.Equipment.IsBroken)
                {
                    var distanceCrossed = sd.Left.Equipment.Performance * sd.Left.Equipment.Speed;
                    sd.DistanceLeft += distanceCrossed;
                    if (sd.DistanceLeft >= 100)
                    {
                        if (IsBrokenOrFixed("Break"))
                        {
                            sd.Left.Equipment.IsBroken = true;
                            Data.Competition.FillRecordBrokenEquipmentPerParticipant(sd.Left);
                        }
                        else
                        {
                            if (Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection).SectionType == SectionTypes.Finish)
                            {
                                roundsPerDriver[(Driver)sd.Left]++;
                                if (roundsPerDriver[(Driver)sd.Left] == amountOfRounds)
                                {
                                    AwardPoints(sd, "Left");

                                    sd.Left = new Driver();
                                }
                            }
                            sd.DistanceLeft = 0;
                            //naar volgende sectie gaan niet mogelijk
                            if (sd1.Left.Name.Length > 1)
                            {
                                //andere kant volgnede sectie ook bezet
                                if (sd1.Right.Name.Length > 0)
                                {
                                    sd.DistanceLeft = 99;
                                    if (Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection).SectionType == SectionTypes.Finish)
                                    {
                                        roundsPerDriver[(Driver)sd.Left]--;
                                    }
                                }
                                else if (sd1.Right.Name.Length == 0)
                                {
                                    sd1.Right = sd.Left;
                                    Data.Competition.FillRecordTimePerSection(sd.Left, e.SignalTime, Data.CurrentRace.Track.Sections.ElementAt(i));
                                    Data.Competition.FillRecordDriversOvertaken(sd.Left);
                                    sd.Left = new Driver();
                                }
                            }
                            else
                            {
                                sd1.Left = sd.Left;
                                Data.Competition.FillRecordTimePerSection(sd.Left, e.SignalTime, Data.CurrentRace.Track.Sections.ElementAt(i));
                                sd.Left = new Driver();
                            }
                        }
                        DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
                    }
                }
                if (sd.Right.Name.Length > 0 && !sd.Right.Equipment.IsBroken)
                {
                    var distanceCrossed = sd.Right.Equipment.Performance * sd.Right.Equipment.Speed;
                    sd.DistanceRight += distanceCrossed;
                    if (sd.DistanceRight >= 100)
                    {
                        if (IsBrokenOrFixed("Break"))
                        {
                            sd.Left.Equipment.IsBroken = true;
                            Data.Competition.FillRecordBrokenEquipmentPerParticipant(sd.Right);
                        }
                        else
                        {
                            if (Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection).SectionType == SectionTypes.Finish)
                            {
                                roundsPerDriver[(Driver)sd.Right]++;
                                if (roundsPerDriver[(Driver)sd.Right] == amountOfRounds)
                                {
                                    AwardPoints(sd, "Right");

                                    sd.Right = new Driver();
                                }
                            }

                            sd.DistanceRight = 0;
                            if (sd1.Right.Name.Length > 1)
                            {
                                //andere kant volgnede sectie ook bezet
                                if (sd1.Left.Name.Length > 0)
                                {
                                    sd.DistanceRight = 99;
                                    if (Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection).SectionType == SectionTypes.Finish)
                                    {
                                        roundsPerDriver[(Driver)sd.Right]--;
                                    }
                                }
                                else if (sd1.Left.Name.Length == 0)
                                {
                                    sd1.Left = sd.Right;
                                    Data.Competition.FillRecordTimePerSection(sd.Right, e.SignalTime, Data.CurrentRace.Track.Sections.ElementAt(i));
                                    Data.Competition.FillRecordDriversOvertaken(sd.Right);
                                    sd.Right = new Driver();
                                }
                            }
                            else
                            {
                                sd1.Right = sd.Right;
                                Data.Competition.FillRecordTimePerSection(sd.Right, e.SignalTime, Data.CurrentRace.Track.Sections.ElementAt(i));
                                sd.Right = new Driver();
                            }
                        }
                        DriversChanged?.Invoke(this, new DriversChangedEventArgs(Data.CurrentRace.Track));
                    }
                }
            }
            CheckCleanUpAndStartNewRace();
        }

        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();

        public SectionData GetSectionData(Section section)
        {
            SectionData returnSection = null;
            if (!_positions.TryGetValue(section, out returnSection))
            {
                _positions.Add(section, new SectionData());
            }
            return returnSection;
        }

        public void RandomizeEquipment()
        {
            foreach (Driver d in Participants)
            {
                Random r1 = new Random();
                d.Equipment.Quality = r1.Next();
                d.Equipment.Performance = r1.Next();
            }
        }

        public void SetParticipants()
        {
            List<IParticipant> orderedParticipants = Participants.OrderBy(i => i.StartPosition).ToList(); //index 0 is de hoogste aantal punten
            int countParticipants = orderedParticipants.Count();
            int countStartGrids = 0;
            foreach (Section s in Track.Sections)
            {
                if (s.SectionType.Equals(SectionTypes.StartGrid))
                    countStartGrids++;
            }
            if (countStartGrids * 2 < countParticipants)
                throw new IndexOutOfRangeException("Er zijn te weinig startGrids aanwezig om alle deelneemers te plaatsen");
            //Console.WriteLine(countStartGrids);
            int i = 0;
            int k = 1;
            int j = countParticipants;
            while (i < j)
            {
                SectionData sd = new SectionData();
                sd.Left = orderedParticipants[i];
                countParticipants--;
                if (countParticipants != 0)
                {
                    sd.Right = orderedParticipants[i + 1];
                    countParticipants--;
                }
                i += 2;
                _positions.TryAdd(Track.Sections.ElementAt(countStartGrids - k), sd);
                k++;
            }
            //Console.SetCursorPosition(0, 100);
            //Console.WriteLine(GetSectionData(Track.Sections.ElementAt(2)).Left.Name);
        }
    }
}