using System.Text.Json.Serialization;

namespace binks_forum_API.Models
{
    public class Admin : User
    {
        private string _adminId = null!;

        [JsonIgnore]
        public User User { get; private set; } = null!;
        private Admin() {}

        public Admin
        (
            string id,
            string firstName,
            string lastName,
            string nickName,
            int xp,
            int rankId,
            string encryptedPassword,
            string country,
            string mail,
            DateTime creationDate,
            bool doubleAuth,
            bool passwordReset,
            string profileImage,
            string adminId
        ) : base(id, firstName, lastName, nickName, xp, rankId, encryptedPassword, country, mail, creationDate, doubleAuth, passwordReset, profileImage)
        {
            _adminId = adminId;
        }

        public string AdminId
        {
            get => _adminId;
            private set => _adminId = value;
        }
    }
}