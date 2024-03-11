using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models.DTOs
{
    // A model for which the consumer is responsible for updating, thus the ID has been omitted
    // General principle: use a separate DTO for creating, updating and returning resources
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
