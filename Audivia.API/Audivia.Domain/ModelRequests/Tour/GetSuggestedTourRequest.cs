namespace Audivia.Domain.ModelRequests.Tour
{
    public class GetSuggestedTourRequest
    {
        public string? UserId { get; set; }
        public double Longitude { get; set; }    
        public double Latitude { get; set; }
        public double Radius { get; set; } = 3;
        public int Top { get; set; } = 5;
    }
}
