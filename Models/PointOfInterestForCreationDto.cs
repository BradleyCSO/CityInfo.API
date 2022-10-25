namespace CityInfo.API.Models
{
    // A model for which the consumer is responsible for updating, thus the ID has been omitted
    // General principle: use a separate DTO for creating, updating and returning resources
    public class PointOfInterestForCreationDto 
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
