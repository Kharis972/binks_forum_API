namespace binks_forum_API.DTOs.Badge
{
    public class NewBadge
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int MinXp { get; set; }
        public string? ImageUrl { get; set; }
        public int PointsRequired { get; set; }
    }
}