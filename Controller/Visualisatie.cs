using Controller;
using Model;
using System;
using System.Linq;

namespace Racing_Simulator
{
    internal enum Directions
    { North = 0, East = 1, South = 2, West = 3 }

    public static class Visualisatie
    {
        #region graphics

        private static string[] _finishHorizontal = { "----", " 1# ", " 2# ", "----" };
        private static string[] _startHorizontal = { "----", " 1|", "2| ", "----" };

        private static string[] _bochtRightFromEast = { "--\\ ", " 1 \\", "  2|", "\\  |" };
        private static string[] _bochtRightFromSouth = { "/  |", " 1 /", "2 / ", "--  " };
        private static string[] _bochtRightFromWest = { "|  \\", "| 1 ", "\\ 2  ", " \\--" };
        private static string[] _bochtRightFromNorth = { " /-- ", "/ 1 ", "| 2 ", "|  /" };

        private static string[] _straightVertical = { "|  |", "|1 |", "| 2|", "|  |" };
        private static string[] _straightHorizontal = { "----", " 1  ", " 2  ", "----" };

        private static string[] _bochtLeftFromEast = _bochtRightFromSouth;
        private static string[] _bochtLeftFromSouth = _bochtRightFromWest;
        private static string[] _bochtLeftFromWest = _bochtRightFromNorth;
        private static string[] _bochtLeftFromNorth = _bochtRightFromEast;

        #endregion graphics

        public static void printSection(string[] section, int x, int y)
        {
            foreach (var c in section)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(c);
                y++;
            }
        }

        public static void DriverChanged(Object sender, DriversChangedEventArgs driverschangedEventArgs)
        {
            DrawTrack(driverschangedEventArgs.Track);
        }

        public static void DrawTrack(Track track)
        {
            int startX = 60;
            int startY = 40;
            Directions direction = Directions.East;
            foreach (Section c in track.Sections)
            {
                if (direction.Equals(Directions.East))
                {
                    if (c.SectionType.Equals(SectionTypes.Finish))
                        printSection(checkString(c, _finishHorizontal), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.StartGrid))
                        printSection(checkString(c, _startHorizontal), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(checkString(c, _straightHorizontal), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(checkString(c, _bochtRightFromEast), startX, startY);
                        direction++;
                        startY += 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(checkString(c, _bochtLeftFromEast), startX, startY);
                        direction--;
                        startY -= 4;
                        continue;
                    }
                    startX += 4;
                }
                if (direction == Directions.South)
                {
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(checkString(c, _straightVertical), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(checkString(c, _bochtRightFromSouth), startX, startY);
                        direction++;
                        startX -= 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(checkString(c, _bochtLeftFromSouth), startX, startY);
                        direction--;
                        startX += 4;
                        continue;
                    }
                    startY += 4;
                }
                if (direction == Directions.West)
                {
                    if (c.SectionType.Equals(SectionTypes.Finish))
                        printSection(checkString(c, _finishHorizontal), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.StartGrid))
                        printSection(checkString(c, _startHorizontal), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(checkString(c, _straightHorizontal), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(checkString(c, _bochtRightFromWest), startX, startY);
                        direction = Directions.North;
                        startY -= 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(checkString(c, _bochtLeftFromWest), startX, startY);
                        direction--;
                        startY += 4;
                        continue;
                    }
                    startX -= 4;
                }
                if (direction == Directions.North)
                {
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(checkString(c, _straightVertical), startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(checkString(c, _bochtRightFromNorth), startX, startY);
                        direction++;
                        startX += 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(checkString(c, _bochtLeftFromNorth), startX, startY);
                        direction = Directions.West;
                        startX -= 4;
                        continue;
                    }
                    startY -= 4;
                }
            }
            Console.SetCursorPosition(0, 50);
            Console.WriteLine("");
        }

        public static string placeParticipant(string s, IParticipant p1, IParticipant p2)
        {
            string returnString = s;
            if (p1.Name.Count() <= 0 || p1 == null)
            {
                returnString = returnString.Replace('1', ' ');
            }
            if (p2.Name.Count() <= 0 || p2 == null)
            {
                returnString = returnString.Replace('2', ' ');
            }
            if (s.Contains('1') && p1 != null && p1.Name.Length > 0)
                returnString = returnString.Replace('1', p1.Name.First());
            if (s.Contains('2') && p2 != null && p2.Name.Length > 0)
                returnString = returnString.Replace('2', p2.Name.First());
            return returnString;
        }

        public static string[] checkString(Section c, string[] vs)
        {
            string[] returnString = new string[4];
            Data.CurrentRace.GetSectionData(c);
            if (Data.CurrentRace.GetSectionData(c) != null)
            {
                int i = 0;
                foreach (string s in vs)
                {
                    string test = placeParticipant(s, Data.CurrentRace.GetSectionData(c).Left, Data.CurrentRace.GetSectionData(c).Right);
                    returnString[i] = test;
                    i++;
                }
                return returnString;
            }
            return vs;
        }

        public static void Initialize()
        {
        }
    }
}