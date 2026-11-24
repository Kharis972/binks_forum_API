using System.ComponentModel.DataAnnotations;

namespace binks_forum_API.DTOs.Badge
{
    public class NewBadge
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }
        [Required, StringLength(500, MinimumLength = 5)]
        public required string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int MinXp { get; set; }
        [Required, StringLength(500)]
        public required string ImageUrl { get; set; }
        [Range(0, int.MaxValue)]
        public int PointsRequired { get; set; }
    }
}
