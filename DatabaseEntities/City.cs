using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // A new key will be generated when a City is added
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public string? Continent { get; set; }
        public string Country { get; set; }
        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }

        public City(string name) // We want this City class to always have a name
        {
            Name = name;
        }
    }
}
