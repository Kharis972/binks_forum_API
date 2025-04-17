#pragma warning disable

namespace binks_forum_API.Models
{
    public class NewsRank : News
    {
        private int _id;

        
        // Relation avec News
        public int NewsId { get; set; }  // Clé étrangère
        public News News { get; set; }   // Propriété de navigation

        // Relation avec Rank
        public int RankId { get; set; }  // Clé étrangère
        public Rank Rank { get; set; }   // Propriété de navigation

        public NewsRank() {}

        public NewsRank
        (
            int id
        )
        {
            _id = id;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }
    }
}