using Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
    }
}
