namespace binks_forum_API.Helpers.CustomExceptions
{
    public class EntityAlreadyExists : Exception
    {
        public EntityAlreadyExists(string message) : base(message) {}
        public EntityAlreadyExists() {}
    }
}