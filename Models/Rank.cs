namespace binks_forum_API.Models
{
    public class Rank 
    {
        private int _id;
        private string _name = null!;
        private string _description = null!;
        private int _minXp;
        private string _rankIcon = null!;

        public List<User>? Users { get; set; }

        public Rank() {}

        public Rank
        (
            string name,
            string description,
            int minXp,
            string rankIcon
        )
        {
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