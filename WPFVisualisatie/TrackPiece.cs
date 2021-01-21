using Model;

namespace WPFVisualisatie
{
   

    public class TrackPiece
    {
        public SectionData SectionData { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public bool Flip { get; set; }
        public string Image { get; set; }
        public int Direction { get; set; }

        public TrackPiece(SectionData sectionData, int coordinateX, int coordinateY, string image, int direction, bool flip)
        {
            SectionData = sectionData;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            Flip = flip;
            Image = image;
            Direction = direction;
        }

        public TrackPiece(SectionData sectionData, int coordinateX, int coordinateY, string image, int direction) : this(sectionData,coordinateX,coordinateY,image,direction,false)
        {
            
        }
    }
}