namespace Model
{
    public class SectionData
    {
        public IParticipant Left { get; set; }
        public int Distance { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }

        public SectionData()
        {
            Left = new Driver();
            Distance = 0;
            Right = new Driver();
            DistanceRight = 0;
        }
    }
}