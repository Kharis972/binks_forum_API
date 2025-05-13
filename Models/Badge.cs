namespace binks_forum_API.Models
{
    public class Badge
    {
        private int _badgeId;
        private string _name = null!;
        private string _description = null!;
        private int _minXp; 
        private string _imageUrl = null!;
        private int _pointsRequired;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        public Badge() {}

        public Badge
        (
            string name, 
            string description, 
            int minXp, 
            string imageUrl, 
            int pointsRequired, 
            DateTime createdAt, 
            DateTime updatedAt
        )
        {
            _name = name;
            _description = description;
            _minXp = minXp;
            _imageUrl = imageUrl;
            _pointsRequired = pointsRequired;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
        }


        public int BadgeId 
        { 
            get => _badgeId;
            set => _badgeId = value; 
        }
        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }
        public string Description 
        { 
            get => _description;
            set => _description = value; 
        }
        public int MinXp 
        { 
            get =>_minXp; 
            set => _minXp = value;
        }
        public string ImageUrl 
        { 
            get => _imageUrl; 
            set => _imageUrl = value; 
        }
        public int PointsRequired
         { 
            get => _pointsRequired; 
            set => _pointsRequired = value; 
        }
        public DateTime CreatedAt 
        { 
            get => _createdAt; 
            set => _createdAt = value; 
        }
        public DateTime UpdatedAt 
        { 
            get => _updatedAt; 
            set => _updatedAt = value; 
        }
    }
}