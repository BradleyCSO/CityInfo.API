namespace CityInfo.API.Models
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
    }
}
