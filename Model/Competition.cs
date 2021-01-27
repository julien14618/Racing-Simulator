using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Records<PointsPerParticipant> PointsPerParticipant = new Records<PointsPerParticipant>();
        public Records<TimePerParticipant> TimePerParticipant = new Records<TimePerParticipant>();
        public Records<FailedEquipmentPerParticipant> FailedEquipmentPerParticipant = new Records<FailedEquipmentPerParticipant>();
        public Records<DriversOvertaken> DriversOvertaken = new Records<DriversOvertaken>();

        public void RaceFinished(Object sender, RaceFinishedEventArgs raceFinishedEventArgs)
        {
            FillRecordAndEndRace(raceFinishedEventArgs.Participants);
        }

        public void printResults()
        {
            foreach (var v in PointsPerParticipant._list)
            {
                Console.WriteLine($"{v.Driver.Name} got {v.Points} points");
            }

            Console.WriteLine($"Points Race has ended and the Winner is: {PointsPerParticipant.GetBestDriver()}");           
            Console.WriteLine($"Most equipmentfails {FailedEquipmentPerParticipant.GetBestDriver()}");
            Console.WriteLine($"Driver that has overtaken most other drivers {DriversOvertaken.GetBestDriver()}");
            Console.WriteLine($"Best Section time {PointsPerParticipant.GetBestDriver()}");
        }

        

        public void PrintTimedResults()
        {
            foreach (var v in TimePerParticipant._list)
            {
                Console.WriteLine($"Section: {v.Section.SectionType.ToString()} Driver:{v.Driver.Name} Time:{v.DateTime.ToString()}");
            }
        }

        public void PrintBrokenEquipmentResults()
        {
            foreach (var v in FailedEquipmentPerParticipant._list)
            {
                Console.WriteLine($"Driver: {v.Driver.Name} Equipment fails: {v.Count}");
            }
        }

        public void PrintDriversOvertakenResults()
        {
            foreach (var i in DriversOvertaken._list)
            {
                Console.WriteLine($"Driver: {i.Driver.Name} has overtaken: {i.Count} drivers");
            }
        }

        public string MostEquipmentFails()
        {
            return FailedEquipmentPerParticipant.GetBestDriver();
        }

        public void FillRecordAndEndRace(List<IParticipant> participants)
        {
            int counter = participants.Count;
            for (int i = 0; i < counter; i++)
            {
                int maxPoints = 0;
                Driver driverBuffer = new Driver();
                foreach (var v in participants)
                {
                    if (v.Points > maxPoints)
                    {
                        driverBuffer = (Driver)v;
                        maxPoints = v.Points;
                    }
                }
                PointsPerParticipant.AddToList(new Model.PointsPerParticipant(driverBuffer, driverBuffer.Points));

                participants.Remove(driverBuffer);
            }
        }

        public void FillRecordTimePerSection(IParticipant participant, DateTime dateTime, Section section)
        {
            TimePerParticipant.AddToList(new Model.TimePerParticipant(participant, section, dateTime));
        }

        public void InitializeBrokenEquipmentAndOvertakenRecord(List<IParticipant> participants)
        {
            foreach (IParticipant participant in participants)
            {
                FailedEquipmentPerParticipant.AddToList(new Model.FailedEquipmentPerParticipant(participant, 0));
                DriversOvertaken.AddToList(new Model.DriversOvertaken(participant, 0));
            }
        }

        public void FillRecordBrokenEquipmentPerParticipant(IParticipant participant)
        {
            foreach (FailedEquipmentPerParticipant p in FailedEquipmentPerParticipant._list)
            {
                if (p.Driver.Equals(participant))
                {
                    p.Count++;
                }
            }
        }

        public void FillRecordDriversOvertaken(IParticipant participant)
        {
            foreach (DriversOvertaken d in DriversOvertaken._list)
            {
                if (d.Driver.Equals(participant))
                {
                    d.Count++;
                }
            }
        }

        public Track NextTrack()
        {
            if (Tracks.Count() > 0)
            {
                return Tracks.Dequeue();
            }
            return null;
        }

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();
            Participants = participants;
            Tracks = tracks;
        }

        public Competition()
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();

            PointsPerParticipant._list = new List<PointsPerParticipant>();
            TimePerParticipant._list = new List<TimePerParticipant>();
            FailedEquipmentPerParticipant._list = new List<FailedEquipmentPerParticipant>();
            DriversOvertaken._list = new List<DriversOvertaken>();
        }
    }
}