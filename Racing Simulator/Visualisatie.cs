using Model;
using System;

namespace Racing_Simulator
{
    internal enum Directions
    { North = 0, East = 1, South = 2, West = 3 }

    public static class Visualisatie
    {
        #region graphics
        private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _startHorizontal = { "----", "x|   ", "  o|", "----" };

        private static string[] _bochtRightFromEast = { "--\\ ", "   \\", "   | ", "\\  |" };
        private static string[] _bochtRightFromSouth = { "/  |", "   /", "  / ", "--  " };
        private static string[] _bochtRightFromWest = { "|  \\", "|   ", "\\    ", " \\--" };
        private static string[] _bochtRightFromNorth = { " /-- ", "/   ", "|   ", "|  /" };

        private static string[] _straightVertical = { "|  |", "|  |", "|  |", "|  |" };
        private static string[] _straightHorizontal = { "----", "    ", "    ", "----" };

        private static string[] _bochtLeftFromEast = _bochtRightFromSouth;
        private static string[] _bochtLeftFromSouth = _bochtRightFromWest;
        private static string[] _bochtLeftFromWest = _bochtRightFromNorth;
        private static string[] _bochtLeftFromNorth = _bochtRightFromEast;
        #endregion
        //public static void Direction(string nameOf)
        //{
        //    if (nameOf.Contains("bochtRight"))
        //        direction++;
        //    else if (nameOf.Contains("bochtLeft"))
        //        direction--;
        //}

        public static void printSection(string[] section, int x, int y)
        {
            foreach (var c in section)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(c);
                y++;
            }
        }

        public static void DrawTrack(Track track)
        {
            int startX = 30;
            int startY = 10;
            Directions direction = Directions.East;
            foreach (Section c in track.Sections)
            {
                if (direction.Equals(Directions.East))
                {
                    if (c.SectionType.Equals(SectionTypes.Finish))
                        printSection(_finishHorizontal, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.StartGrid))
                        printSection(_startHorizontal, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(_straightHorizontal, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(_bochtRightFromEast, startX, startY);
                        direction++;
                        startY += 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(_bochtLeftFromEast, startX, startY);
                        direction--;
                        startY -= 4;
                        continue;
                    }
                    startX += 4;
                }
                if (direction == Directions.South)
                {
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(_straightVertical, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(_bochtRightFromSouth, startX, startY);
                        direction++;
                        startX -= 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(_bochtLeftFromSouth, startX, startY);
                        direction--;
                        startX += 4;
                        continue;
                    }
                    startY += 4;
                }
                if (direction == Directions.West)
                {
                    if (c.SectionType.Equals(SectionTypes.Finish))
                        printSection(_finishHorizontal, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.StartGrid))
                        printSection(_startHorizontal, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(_straightHorizontal, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(_bochtRightFromWest, startX, startY);
                        direction = Directions.North;
                        startY -= 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(_bochtLeftFromWest, startX, startY);
                        direction--;
                        startY += 4;
                        continue;
                    }
                    startX -= 4;
                }
                if (direction == Directions.North)
                {
                    if (c.SectionType.Equals(SectionTypes.Straight))
                        printSection(_straightVertical, startX, startY);
                    if (c.SectionType.Equals(SectionTypes.RightCorner))
                    {
                        printSection(_bochtRightFromNorth, startX, startY);
                        direction++;
                        startX += 4;
                        continue;
                    }
                    if (c.SectionType.Equals(SectionTypes.LeftCorner))
                    {
                        printSection(_bochtLeftFromNorth, startX, startY);
                        direction = Directions.West;
                        startX -= 4;
                        continue;
                    }
                    startY -= 4;
                }
            }
            Console.SetCursorPosition(50, 30);
            Console.WriteLine("");
        }

        public static void Initialize()
        {
        }
    }
}