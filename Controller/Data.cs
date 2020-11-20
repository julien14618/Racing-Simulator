using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void NextRace()
        {
            Track track = null;
            track = Competition.NextTrack();
            if (track != null)
            {
                CurrentRace = new Race(track, Competition.Participants);
            }
        }

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
            //Driver d3 = new Driver();
            //Driver d4 = new Driver();
            
            //d3.Name = "CDriver 3";
            //d4.Name = "DDriver 4";
            d2.Name = "BDriver 2";
            d1.Name = "ADriver 1";
            
            
            //d3.Points = 50;
            //d4.Points = 70;
            d2.Points = 75;
            d1.Points = 5;

            d1.Equipment.Performance = 2;
            d2.Equipment.Performance = 4;
            //d3.Equipment.Performance = 3;
            //d4.Equipment.Performance = 4;

            d1.Equipment.Speed = 10;
            d2.Equipment.Speed = 10;
            //d3.Equipment.Speed = 6;
            //d4.Equipment.Speed = 8;
            

            Competition.Participants.Add(d1);
            Competition.Participants.Add(d2);
            //Competition.Participants.Add(d3);
            //Competition.Participants.Add(d4);
            
            
        }

        public static void AddTrack()
        {
            SectionTypes[] sectionst1 = { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner };
            SectionTypes[] sectionst2 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner };
            Track oostendorp = new Track("Oostendorp",
               new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid,SectionTypes.Finish, SectionTypes.Straight,
                SectionTypes.LeftCorner, SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight});
            Track t1 = new Track("Zandvoort", sectionst1);
            Track t2 = new Track("Spa", sectionst2);
            Competition.Tracks.Enqueue(t1);
            Competition.Tracks.Enqueue(t2);
            Competition.Tracks.Enqueue(oostendorp);
        }
    }
}