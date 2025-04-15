namespace binks_forum_API.Models
{
    public class User
    {
        //Variables
        //private used only inside constructors
        private string _id;
        private string _firstName;
        private string  _lastName;
        private string _nickName;
        private int _xp;
        private int _rankId;
        private string _encryptedPassword;
        private string _country;
        private string _mail;
        private DateTime _creationDate;
        private bool _doubleAuth;
        private bool _passwordReset;
        private string? _profileImage;
        private int _age;
        private bool _isShadowBan;

        public List<Topic>? Topics { get; private set; }

        //default contructor by FluentAPI
        public User() {}

        //Constructor with parameters
        public User
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
            string? profileImage,
            int age,
            bool isShadowBan
        )
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _nickName = nickName;
            _xp = xp;
            _rankId = rankId;
            _encryptedPassword = encryptedPassword;
            _country = country;
            _mail = mail;
            _creationDate = creationDate;
            _doubleAuth = doubleAuth;
            _passwordReset = passwordReset;
            _profileImage = profileImage;
            _age = age;
            _isShadowBan = isShadowBan;
        }
        

        //Access property
        public string Id
        {
            get => _id;
            init => _id = value;
        }
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }
        public string NickName
        {
            get => _nickName;
            set => _nickName = value;
        }
        public int Xp
        {
            get => _xp;
            set => _xp = value;
        }
        public int RankId
        {
            get => _rankId;
            set => _rankId = value;
        }
        public string EncryptedPassword
        {
            get => _encryptedPassword;
            private set => _encryptedPassword = value;
        }
        public string Country
        {
            get => _country;
            set => _country = value;
        }
        public string Mail
        {
            get => _mail;
            set => _mail = value;
        }
        public DateTime CreationDate
        {
            get => _creationDate;
            set => _creationDate = value;
        }
        public bool DoubleAuth
        {
            get => _doubleAuth;
            set => _doubleAuth = value;
        }
        public bool PasswordReset
        {
            get => _passwordReset;
            init => _passwordReset = value;
        }
        public string? ProfileImage
        {
            get => _profileImage;
            set => _profileImage = value;
        }
        public int Age
        {
            get => _age;
            set => _age = value;
        }
        public bool IsShadowBan
        {
            get => _isShadowBan;
            set => _isShadowBan = value;
        }

    }
}