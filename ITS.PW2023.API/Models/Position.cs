namespace ITS.PW2023.API.Models
{
    public class Position
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
