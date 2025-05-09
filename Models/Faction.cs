namespace binks_forum_API.Models
{
    public class Faction
    {
        private int _factionId;
        private string _name = null!;
        private string _description = null!;
        private string _imageUrl = null!;

        public List<User>? Users { get; set;}

        public Faction() {}

        public Faction
        (
            string name,
            string description,
            string imageUrl
        )
        {
            _name = name;
            _description = description;
            _imageUrl = imageUrl;
        }

        public int FactionId
        {
            get => _factionId;
            set => _factionId = value;
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
        public string ImageUrl
        {
            get => _imageUrl;
            set => _imageUrl = value;
        }
    }
}