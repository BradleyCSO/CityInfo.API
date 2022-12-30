using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("CityId")]
        [MaxLength(200)]
        public string Description { get; set; }
        public City? City { get; set; } // Navigation property. Foreign key -- thus a relationship between these classes will be created
        public int CityId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }

        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}
