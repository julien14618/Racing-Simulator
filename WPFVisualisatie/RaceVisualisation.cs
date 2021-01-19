using Model;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace WPFVisualisatie
{
    public static class RaceVisualisation
    {
        #region imagePaths

        private const string _start = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Start.png";
        private const string _finish = "C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Start.png";
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
        private static Graphics g;
        private static Bitmap RacePiece;

        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap bm = CreateImages.CreateEmptyBitmap(1920, 1080);
            g = Graphics.FromImage(bm);
            
            
            int width = 100;
            int widthCar = 10;
            int height = 100;
            int heightCar = 10;
            int x = 0;
            int y = 0;
            
            
            
            foreach (Section section in track.Sections)
            {

                RacePiece = DrawTrackPiece(section);
                g.DrawImage(RacePiece, x, y,width,height); 
                g.DrawImage(CreateImages.GetImageFromCache(_carYellow), x+25,y,widthCar,heightCar); 
                x += 100;
            }
            return CreateImages.CreateBitmapSourceFromGdiBitmap(bm);
        }

        public static Bitmap DrawTrackPiece(Section section)
        {
            
            Bitmap returnValue = CreateImages.GetImageFromCache("empty");
            returnValue = null;
            returnValue = CreateImages.GetImageFromCache("empty");

            switch (section.SectionType)
            {
                case (SectionTypes.Straight):
                    returnValue = CreateImages.GetImageFromCache(_straight);
                    returnValue.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case (SectionTypes.StartGrid):
                    returnValue = CreateImages.GetImageFromCache(_start);
                    returnValue.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case (SectionTypes.RightCorner):
                    returnValue = CreateImages.GetImageFromCache(_corner);
                    break;
                case (SectionTypes.LeftCorner):
                    returnValue = CreateImages.GetImageFromCache(_corner);
                    break;
                case (SectionTypes.Finish):
                    returnValue = CreateImages.GetImageFromCache(_finish);
                    break;
                default:
                    returnValue = null;
                    throw new System.Exception();

            }
            return returnValue;

        }
        
    }
}