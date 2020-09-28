using System;
using System.Runtime.CompilerServices;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipant();
            AddTrack();
        }
        public static void AddParticipant()
        {
            Driver d1 = new Driver();
            Driver d2 = new Driver();
            Driver d3 = new Driver();
            Driver d4 = new Driver();
            d1.Name = "Piet1";
            d2.Name = "Piet2";
            d3.Name = "Piet3";
            d4.Name = "Piet4";
            Competition.Participants.Add(d1);
            Competition.Participants.Add(d2);
            Competition.Participants.Add(d3);
            Competition.Participants.Add(d4);
        }
        public static void AddTrack()
        {
            SectionTypes[] sectionst1 = { SectionTypes.StartGrid, SectionTypes.Straight };
            SectionTypes[] sectionst2 = { SectionTypes.LeftCorner, SectionTypes.RightCorner };
            Track t1 = new Track("Zandvoort", sectionst1);
            Track t2 = new Track("Pakistan", sectionst2);
        }
    }
}
