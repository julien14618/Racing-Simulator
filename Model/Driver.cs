namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
        public int StartPosition { get; set; }

        public Driver()
        {
            Name = "";
            Points = 0;
            StartPosition = 0;
            Equipment = new Car();
            TeamColor = TeamColors.Blue;
        }
    }
}