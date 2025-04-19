namespace binks_forum_API.Models
{
    public class Rank : User
    {
        private int _id;
        private string _name;
        private string _description;
        private int _minXp;
        private string _rankIcon;
        public string UserId { get; set; } 

        public Rank() {}

        public Rank
        (
            int id,
            string name,
            string description,
            int minXp,
            string rankIcon
        )
        {
            _id = id;
            _name = name;
            _description = description;
            _minXp = minXp;
            _rankIcon = rankIcon;

        }

        public int Id
        {
            get => _id;
            set => _id = value;
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
            get => _minXp;
            set => _minXp = value;
        }
        public string RankIcon
        {
            get => _rankIcon;
            set => _rankIcon = value;
        }
    }
}