using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    
    public class PointsPerParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public PointsPerParticipant(string name, int points)
        {
            Name = name;
            Points = points;
        }
    }
    public class TimePerParticipant
    {
        public string Name { get; set; }
        public TimeSpan TimeSpan { get; set; }

        public TimePerParticipant(string name, TimeSpan timeSpan)
        {
            Name = name;
            TimeSpan = timeSpan;
        }
    }
}
