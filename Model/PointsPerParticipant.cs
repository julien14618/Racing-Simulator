using System;
using System.Collections.Generic;

namespace Model
{
    public class PointsPerParticipant : IRecord<PointsPerParticipant>
    {
        public IParticipant Driver { get; set; }
        public int Points { get; set; }

        public PointsPerParticipant(IParticipant driver, int points)
        {
            Driver = driver;
            Points = points;
        }

        public void Add(List<PointsPerParticipant> pointsPerParticipants)
        {
            bool found = false;
            foreach (var v in pointsPerParticipants)
            {
                if (this.Driver.Equals(v.Driver))
                {
                    v.Points += this.Points;
                    found = true;
                }
            }
            if (!found)
            {
                pointsPerParticipants.Add(this);
            }
        }

        public string BestDriver(List<PointsPerParticipant> list)
        {
            PointsPerParticipant bestDriver = new PointsPerParticipant(new Driver(), 0);
            foreach (var v in list)
            {
                if (v.Points > bestDriver.Points)
                    bestDriver = v;
            }
            return bestDriver.Driver.Name;
        }
    }

    public class TimePerParticipant : IRecord<TimePerParticipant>
    {
        public IParticipant Driver { get; set; }

        public Section Section { get; set; }
        public DateTime DateTime { get; set; }

        public TimePerParticipant(IParticipant driver, Section section, DateTime dateTime)
        {
            Driver = driver;
            Section = section;
            DateTime = dateTime;
        }

        public void Add(List<TimePerParticipant> list)
        {
            list.Add(this);
        }

        public string BestDriver(List<TimePerParticipant> list)
        {
            TimePerParticipant participant = new TimePerParticipant(new Driver(), new Section(SectionTypes.Straight), new DateTime(1, 1, 1, 0, 0, 0));
            foreach(var v in list)
            {
                if (participant.DateTime.CompareTo(v.DateTime) < 0 && v.Driver.Name.Length > 0)
                    participant = v;
            }
            return participant.Driver.Name;
        }
    }

    public class FailedEquipmentPerParticipant : IRecord<FailedEquipmentPerParticipant>
    {
        public IParticipant Driver { get; set; }
        public int Count { get; set; }

        public FailedEquipmentPerParticipant(IParticipant driver, int count)
        {
            Driver = driver;
            Count = count;
        }

        public void Add(List<FailedEquipmentPerParticipant> list)
        {
            bool found = false;
            foreach (var v in list)
            {
                if (this.Driver.Equals(v.Driver))
                {
                    v.Count += this.Count;
                    found = true;
                }
            }
            if (!found)
            {
                list.Add(this);
            }
        }

        public string BestDriver(List<FailedEquipmentPerParticipant> list)
        {
            FailedEquipmentPerParticipant bestDriver = new FailedEquipmentPerParticipant(new Driver(), 0);
            foreach (var v in list)
            {
                if (v.Count > bestDriver.Count)
                    bestDriver = v;
            }
            return bestDriver.Driver.Name;
        }
    }

    public class DriversOvertaken : IRecord<DriversOvertaken>
    {
        public IParticipant Driver { get; set; }
        public int Count { get; set; }

        public DriversOvertaken(IParticipant driver, int count)
        {
            Driver = driver;
            Count = count;
        }

        public void Add(List<DriversOvertaken> list)
        {
            bool found = false;
            foreach (var v in list)
            {
                if (this.Driver.Equals(v.Driver))
                {
                    v.Count += this.Count;
                    found = true;
                }
            }
            if (!found)
            {
                list.Add(this);
            }
        }

        public string BestDriver(List<DriversOvertaken> list)
        {
            DriversOvertaken bestDriver = new DriversOvertaken(new Driver(), 0);
            foreach (var v in list)
            {
                if (v.Count > bestDriver.Count)
                    bestDriver = v;
            }
            return bestDriver.Driver.Name;
        }
    }
}