namespace binks_forum_API.Repositories.Interfaces
{
    using binks_forum_API.DTOs.AnswerInMessage;
    using binks_forum_API.Models;

    public interface IAnswerInMessageRepository : IRepository<AnswerInMessage, int>
    {
        Task<AnswerInMessage> AddNewAnswerInMessageAsync(NewAnswerInMessage newAnswerInMessage, string userId);
        Task DeleteAnswerInMessageAsync(int id, string userId);
        Task<AnswerInMessage> EditAnswerInMessageAsync(int id, string userId, NewAnswerInMessage newAnswerInMessage);
        Task<AnswerInMessage> GetAnswerInMessageByIdAsync(int id);
        Task<List<AnswerInMessage>> GetAllAnswersInMessagesAsync();
    }
}