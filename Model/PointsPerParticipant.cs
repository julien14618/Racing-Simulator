using System;
using System.Collections.Generic;
using System.Text;

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
            foreach(var v in pointsPerParticipants)
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
    }
}
