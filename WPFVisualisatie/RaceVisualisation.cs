using Controller;
using Model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace WPFVisualisatie
{
    public static class RaceVisualisation
    {
        #region imagePaths

        private const string _start = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Start.png";
        private const string _finish = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Finish.png";
        private const string _straight = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Straight.png";
        private const string _corner = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Corner.png";
        private const string _carBlue = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Blue.png";
        private const string _carBlueBroken = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Blue_BrokenDown.png";
        private const string _carRed = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Red.png";
        private const string _carRedBroken = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Red_BrokenDown.png";
        private const string _carGreen = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Green.png";
        private const string _carGreenBroken = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Green_BrokenDown.png";
        private const string _carGrey = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Grey.png";
        private const string _carGreyBroken = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Grey_BrokenDown.png";
        private const string _carYellow = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Car_Yellow.png";
        private const string _carYellowBroken = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images//Racetrack_Car_Yellow_BrokenDown.png";

        #endregion imagePaths

        private static List<TrackPiece> trackPieces;

        public static void Initialize()
        {
            trackPieces = new List<TrackPiece>();
        }

        public static BitmapSource DrawTrack(Track track)
        {
            FillTrackPieces(track.Sections);
            Bitmap drawnTrack = DrawTrackPieces();
            Bitmap drawnParticipants = DrawParticipants(drawnTrack);

            return CreateImages.CreateBitmapSourceFromGdiBitmap(drawnParticipants);
        }

        private static Bitmap DrawTrackPieces()
        {
            Bitmap backGround = CreateImages.CreateEmptyBitmap(trackPieces.Max(piece => piece.CoordinateX) + 480, trackPieces.Max(piece => piece.CoordinateY) + 480);
            Graphics g = Graphics.FromImage(backGround);
            Bitmap currentTrackPiece = null;
            foreach (TrackPiece t in trackPieces)
            {
                currentTrackPiece = new Bitmap(CreateImages.GetImageFromCache(t.Image));
                if (t.Direction == 0 && t.Flip)
                {
                    currentTrackPiece.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                if (t.Direction == 1)
                {
                    if (t.Flip)
                        currentTrackPiece.RotateFlip(RotateFlipType.Rotate90FlipY);
                    else
                        currentTrackPiece.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                if (t.Direction == 2)
                {
                    if (t.Flip)
                        currentTrackPiece.RotateFlip(RotateFlipType.Rotate180FlipX);
                    else
                        currentTrackPiece.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                if (t.Direction == 3)
                {
                    if (t.Flip)
                        currentTrackPiece.RotateFlip(RotateFlipType.Rotate270FlipY);
                    else
                        currentTrackPiece.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                g.DrawImage(currentTrackPiece, t.CoordinateX, t.CoordinateY, 240, 240);
            }
            return backGround;
        }

        private static Bitmap DrawParticipants(Bitmap drawnTrack)
        {
            Bitmap localDrawnTrack = drawnTrack;
            Graphics g = Graphics.FromImage(localDrawnTrack);
            
            Bitmap car;
            Bitmap rotatedCar;
            bool brokenCar;

            foreach (TrackPiece t in trackPieces)
            {
                if (t.SectionData != null && t.SectionData.Left.Name.Length > 0)
                {
                    brokenCar = false;
                    if (t.SectionData.Left.Equipment.IsBroken)
                        brokenCar = true;
                    car = new Bitmap(getCarColor(t.SectionData.Left.TeamColor, brokenCar));
                    rotatedCar = rotateCar(car, t.Direction);
                    g.DrawImage(rotatedCar, t.CoordinateX + 140, t.CoordinateY + 60, 50, 50);
                }
                if (t.SectionData != null && t.SectionData.Right.Name.Length > 0)
                {
                    brokenCar = false;
                    if (t.SectionData.Right.Equipment.IsBroken)
                        brokenCar = true;
                    car = new Bitmap(getCarColor(t.SectionData.Right.TeamColor, brokenCar));
                    rotatedCar = rotateCar(car, t.Direction);
                    g.DrawImage(rotatedCar, t.CoordinateX + 20, t.CoordinateY + 140, 50, 50);
                }
            }

            return localDrawnTrack;
        }

        private static Bitmap rotateCar(Bitmap car, int direction)
        {
            Bitmap localCar = car;
            switch (direction)
            {
                case 0:
                    localCar.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;

                case 1:
                    localCar.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;

                case 2:
                    localCar.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;

                case 3:
                    localCar.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            return localCar;
        }

        private static Bitmap getCarColor(TeamColors teamColor, bool broken)
        {
            Bitmap car = null;

            switch (teamColor)
            {
                case TeamColors.Red:
                    if (broken)
                        car = CreateImages.GetImageFromCache(_carRedBroken);
                    else
                        car = CreateImages.GetImageFromCache(_carRed);
                    break;

                case TeamColors.Green:
                    if (broken)
                        car = CreateImages.GetImageFromCache(_carGreenBroken);
                    else
                        car = CreateImages.GetImageFromCache(_carGreen);
                    break;

                case TeamColors.Yellow:
                    if (broken)
                        car = CreateImages.GetImageFromCache(_carYellowBroken);
                    else
                        car = CreateImages.GetImageFromCache(_carYellow);
                    break;

                case TeamColors.Grey:
                    if (broken)
                        car = CreateImages.GetImageFromCache(_carGreyBroken);
                    else
                        car = CreateImages.GetImageFromCache(_carGrey);
                    break;

                case TeamColors.Blue:
                    if (broken)
                        car = CreateImages.GetImageFromCache(_carBlueBroken);
                    else
                        car = CreateImages.GetImageFromCache(_carBlue);
                    break;
            }

            return car;
        }

        private static void FillTrackPieces(LinkedList<Section> sections)
        {
            if (Data.CurrentRace != null)
            {
                Race currentRace = Data.CurrentRace;
                int direction = 1;
                int x = 1440, y = 1440;
                trackPieces.Clear();
                foreach (Section s in sections)
                {
                    switch (s.SectionType)
                    {
                        case SectionTypes.StartGrid:
                            trackPieces.Add(new TrackPiece(currentRace.GetSectionData(s), x, y, _start, direction));
                            break;

                        case SectionTypes.Straight:
                            trackPieces.Add(new TrackPiece(currentRace.GetSectionData(s), x, y, _straight, direction));
                            break;

                        case SectionTypes.RightCorner:
                            trackPieces.Add(new TrackPiece(currentRace.GetSectionData(s), x, y, _corner, direction));
                            direction++;
                            direction %= 4;
                            break;

                        case SectionTypes.LeftCorner:
                            trackPieces.Add(new TrackPiece(currentRace.GetSectionData(s), x, y, _corner, direction, true));
                            direction -= 1;
                            direction %= 4;
                            if (direction < 0)
                                direction = 3;
                            break;

                        case SectionTypes.Finish:
                            trackPieces.Add(new TrackPiece(currentRace.GetSectionData(s), x, y, _finish, direction));
                            break;
                    }
                    switch (direction)
                    {
                        case 0:
                            y -= 240;
                            break;

                        case 1:
                            x += 240;
                            break;

                        case 2:
                            y += 240;
                            break;

                        case 3:
                            x -= 240;
                            break;
                    }
                }
            }
        }
    }
}