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
            Car c1 = new Car();
            c1.IsBroken = false;
            c1.Performance = 0;
            c1.Quality = 0;
            c1.Speed = 1;

            d1.Name = "Piet1";
            d1.Points = 0;
            d1.TeamColor = TeamColors.Blue;
            d1.Equipment = c1;

            Competition.Participants.Add(d1);
        }

        public static void AddTrack()
        {
            SectionTypes[] sectionst1 = { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner };
            SectionTypes[] sectionst2 = { SectionTypes.Finish, SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner };
            Track t1 = new Track("Zandvoort", sectionst1);
            Track t2 = new Track("Spa", sectionst2);
            Competition.Tracks.Enqueue(t1);
            Competition.Tracks.Enqueue(t2);
        }
    }
}