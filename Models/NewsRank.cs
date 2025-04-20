#pragma warning disable

namespace binks_forum_API.Models
{
    public class NewsRank : Rank
    {
        private int _newsRankId;
        private int _newsId;  // Clé étrangère


        public News News { get; set; }   // Propriété de navigation

        // Relation avec Rank
        public int RankId { get; set; }  // Clé étrangère
        public Rank Rank { get; set; }   // Propriété de navigation

        private NewsRank() {}

        public NewsRank
        (
            string name,
            string description,
            int minXp,
            string rankIcon,
            int newsRankId,
            int newsId
        ) : base( name, description, minXp, rankIcon)
        {
            _newsRankId = newsRankId;
            _newsId = newsId;
        }

        public int NewsRankId
        {
            get => _newsRankId;
            set => _newsRankId = value;
        }
        public int NewsId
        {
            get => _newsId;
            set => _newsId = value;
        }
    }
}