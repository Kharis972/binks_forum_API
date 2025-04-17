using ZstdSharp.Unsafe;

namespace binks_forum_API.Helpers.CustomExceptions
{
    public class UserIdToPromoteNotFoundException : Exception
    {
        public UserIdToPromoteNotFoundException(string message) : base(message) {}
        public UserIdToPromoteNotFoundException() {}
    }
}