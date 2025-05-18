namespace Audivia.Domain.ModelRequests.Tour
{
    public class GetSuggestedTourRequest
    {
        public double Longitude { get; set; }    
        public double Latitude { get; set; }
        public double Radius { get; set; } = 3; // default suggest in 3 km
    }
}
