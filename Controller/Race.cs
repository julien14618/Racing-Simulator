using Model;
using Racing_Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Controller
{
    public class Race
    {
        private Timer timer;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        //private int test = 1;

        public Race(Track track, List<IParticipant> participants)
        {
            Participants = participants;
            Track = track;
            timer = new Timer(100);
            timer.Elapsed += OnTimedEvent;
            _random = new Random(DateTime.Now.Millisecond);
            DriversChanged += Visualisatie.DriverChanged;
        }

        public void start()
        {
            timer.Start();
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < Data.CurrentRace.Track.Sections.Count; i++)
            {
                _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(i), out SectionData sd);
                if (sd.Left.Name.Length > 0)
                {
                    var distanceCrossed = sd.Left.Equipment.Performance + sd.Left.Equipment.Speed;
                    sd.DistanceLeft += distanceCrossed;
                }
                if (sd.Right.Name.Length > 0)
                {
                    var distanceCrossed = sd.Right.Equipment.Performance + sd.Right.Equipment.Speed;
                    sd.DistanceRight += distanceCrossed;
                }
                int setAtNextSection = i + 1;
                if (setAtNextSection == Data.CurrentRace.Track.Sections.Count)
                    setAtNextSection = 0;
                if (sd.DistanceLeft >= 100)
                {
                    sd.DistanceLeft -= 100;
                    _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection), out SectionData sd1);   
                    
                    sd1.Left = sd.Left;
                    sd.Left = new Driver();
                    DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
                }
                if (sd.DistanceRight >= 100)
                {
                    sd.DistanceRight -= 100;
                    _positions.TryGetValue(Data.CurrentRace.Track.Sections.ElementAt(setAtNextSection), out SectionData sd1); ;
                    sd1.Right = sd.Right;
                    sd.Right = new Driver();
                    DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
                }
            }
            
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
            List<IParticipant> orderedParticipants = Participants.OrderByDescending(i => i.Points).ToList(); //index 0 is de hoogste aantal punten
            int countParticipants = orderedParticipants.Count();
            int countStartGrids = 0;
            foreach (Section s in Track.Sections)
            {
                if (s.SectionType.Equals(SectionTypes.StartGrid))
                    countStartGrids++;
            }
            if (countStartGrids * 2 < countParticipants)
                throw new IndexOutOfRangeException("Er zijn te weinig startGrids aanwezig om alle deelneemers te plaatsen");
            Console.WriteLine(countStartGrids);
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
            Console.SetCursorPosition(0, 100);
            //Console.WriteLine(GetSectionData(Track.Sections.ElementAt(2)).Left.Name);
        }
    }
}