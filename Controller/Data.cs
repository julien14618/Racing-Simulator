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
            else
            {
                CurrentRace = null;
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

            d3.StartPosition = 3;
            d4.StartPosition = 4;
            d2.StartPosition = 1;
            d1.StartPosition = 2;
            d5.StartPosition = 5;
            d6.StartPosition = 6;

            d1.Equipment.Performance = 5;
            d2.Equipment.Performance = 8;
            d3.Equipment.Performance = 5;
            d4.Equipment.Performance = 7;
            d5.Equipment.Performance = 8;
            d6.Equipment.Performance = 9;

            d1.Equipment.Speed = 4;
            d2.Equipment.Speed = 6;
            d3.Equipment.Speed = 5;
            d4.Equipment.Speed = 7;
            d5.Equipment.Speed = 10;
            d6.Equipment.Speed = 8;

            d1.TeamColor = TeamColors.Yellow;
            d2.TeamColor = TeamColors.Grey;
            d3.TeamColor = TeamColors.Red;
            d4.TeamColor = TeamColors.Blue;
            d5.TeamColor = TeamColors.Green;
            d6.TeamColor = TeamColors.Yellow;

            Competition.Participants.Add(d1);
            Competition.Participants.Add(d2);
            Competition.Participants.Add(d3);
            //Competition.Participants.Add(d4);
            //Competition.Participants.Add(d5);
            //Competition.Participants.Add(d6);
        }

        public static void AddTrack()
        {
            SectionTypes[] sectionst1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner };
            SectionTypes[] sectionst2 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner };
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
            Competition.Tracks.Enqueue(oostendorp);
            Competition.Tracks.Enqueue(t2);
            
        }
    }
}