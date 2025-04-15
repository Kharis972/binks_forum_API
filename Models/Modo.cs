namespace binks_forum_API.Models
{
    public class Modo : User
    {
        private string _modoId = null!;
        public User User { get; set; } = null!;

        private Modo() {}

        public Modo
        (
            string modoId
        )
        {
            _modoId = modoId;
        }

        public string ModoId
        {
            get => _modoId;
            init => _modoId = value;
        }
    }
}