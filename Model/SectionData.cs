namespace Model
{
    public class SectionData
    {
        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }

        public SectionData()
        {
            Left = new Driver();
            DistanceLeft = 0;
            Right = new Driver();
            DistanceRight = 0;
        }
    }
}