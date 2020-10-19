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
            Driver d3 = new Driver();
            Driver d4 = new Driver();
            Driver d5 = new Driver();
            Driver d6 = new Driver();
            
            d3.Name = "CDriver 3";
            d4.Name = "DDriver 4";
            d2.Name = "BDriver 2";
            d1.Name = "ADriver 1";
            d5.Name = "EDriver 5";
            d6.Name = "FDriver 6";
            
            d3.Points = 50;
            d4.Points = 70;
            d2.Points = 75;
            d1.Points = 5;
            d5.Points = 500;
            d6.Points = 1;

            Competition.Participants.Add(d1);
            Competition.Participants.Add(d2);
            Competition.Participants.Add(d3);
            Competition.Participants.Add(d4);
            Competition.Participants.Add(d5);
            Competition.Participants.Add(d6);
            
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