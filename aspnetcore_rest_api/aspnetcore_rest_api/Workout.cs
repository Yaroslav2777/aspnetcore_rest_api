namespace aspnetcore_rest_api
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public string ActivityType { get; set; } 
        public int DurationMinutes { get; set; }
        public double DistanceKm { get; set; }  
        public int CaloriesBurned { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string Notes { get; set; }
    }
}
