using binks_forum_API.DTOs.AnswerInMessage;
using binks_forum_API.Helpers.CustomExceptions;
using binks_forum_API.Models;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Services.Interfaces;

namespace binks_forum_API.Services
{
    public class AnswerInMessageService : Service<AnswerInMessage, int>, IAnswerInMessageService
    {
        private readonly IAnswerInMessageRepository _answerInMessageRepository;
        public AnswerInMessageService(IAnswerInMessageRepository answerInMessageRepository) : base(answerInMessageRepository)
        {
            _answerInMessageRepository = answerInMessageRepository;
        }
        public async Task<AnswerInMessage> AddNewAnswerInMessageAsync(NewAnswerInMessage newAnswerInMessage, string userId)
        {
            try
            {
                return await _answerInMessageRepository.AddNewAnswerInMessageAsync(newAnswerInMessage, userId);
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch
            {
                throw new UserNotFoundException();
            }
        }

        public async Task DeleteAnswerInMessageAsync(int id, string userId)
        {
            try
            {
                await _answerInMessageRepository.DeleteAnswerInMessageAsync(id, userId);
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
        }
        public async Task<AnswerInMessage> EditAnswerInMessageAsync(int id, string userId, EditAnswerInMessage editAnswerInMessage)
        {
            try
            {
                return await _answerInMessageRepository.EditAnswerInMessageAsync(id, userId, editAnswerInMessage);
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException();
            }
        }
        public async Task<AnswerInMessage> GetAnswerInMessageByIdAsync(int id)
        {
            try
            {
                return await _answerInMessageRepository.GetAnswerInMessageByIdAsync(id);
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }
        public async Task<List<AnswerInMessage>> GetAllAnswersInMessagesAsync()
        {
            try
            {
                return await _answerInMessageRepository.GetAllAnswersInMessagesAsync();
            }
            catch (DatabaseUpdateException)
            {
                throw new DatabaseUpdateException();
            }
            catch (DatabaseGlobalException)
            {
                throw new DatabaseGlobalException();
            }
        }
    }
}