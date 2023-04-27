namespace ITS.PW2023.Simulator.Models
{
    public class Output
    {
        public Guid IdDevice { get; set; }
        public Guid IdActivity { get; set; }
        public Position Position { get; set; }
        public int HeartFrequency { get; set; }
        public int NLaps { get; set; }
    }
}