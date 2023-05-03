namespace ITS.PW2023.API.Models
{
    public class Activity
    {
        public Guid guid = Guid.Empty;
        public int Laps = 0;
        public Activity(Guid activityGuid)
        {
            guid = activityGuid;
        }
    }
}
