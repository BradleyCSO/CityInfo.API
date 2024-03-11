namespace CityInfo.API.Models.DTOs
{
    public class CityWithoutPointsOfInterestDto
    {
        /// <summary>
        /// The id of the City
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the City
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The description of the City
        /// </summary>
        public string? Description { get; set; }

        public string? Continent { get; set; }

        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAltText { get; set; }
    }
}
