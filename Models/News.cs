namespace binks_forum_API.Models
{
    public class News 
    {
        private int _id;
        private string _name = null!;
        private string _description = null!;
        private string _body = null!;
        private DateTime _releaseDate;

        public News() {}

        public News
        (
            string name,
            string description,
            string body,
            DateTime releaseDate
        )
        {
            _name = name;
            _description = description;
            _body = body;
            _releaseDate = releaseDate;
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
        public string Body
        {
            get => _body;
            set => _body = value;
        }
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set => _releaseDate = value;
        } 
    }
}