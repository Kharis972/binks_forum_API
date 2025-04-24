using binks_forum_API.DTOs.AnswerInMessage;
using binks_forum_API.Models;
using binks_forum_API.Service.Interfaces;

namespace binks_forum_API.Services.Interfaces
{
    public interface IAnswerInMessageService : IService<AnswerInMessage, int>
    {
        Task<AnswerInMessage> AddNewAnswerInMessageAsync(NewAnswerInMessage newAnswerInMessage, string userId);
        Task DeleteAnswerInMessageAsync(int id, string userId);
        Task<AnswerInMessage> EditAnswerInMessageAsync(int id, string userId, EditAnswerInMessage editAnswerInMessage);
        Task<AnswerInMessage> GetAnswerInMessageByIdAsync(int id);
        Task<List<AnswerInMessage>> GetAllAnswersInMessagesAsync();
    }
}