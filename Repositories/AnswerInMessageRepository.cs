namespace binks_forum_API.Repositories
{
    using binks_forum_API.Data;
    using binks_forum_API.DTOs.AnswerInMessage;
    using binks_forum_API.Helpers.CustomExceptions;
    using binks_forum_API.Models;
    using binks_forum_API.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AnswerInMessageRepository : Repository<AnswerInMessage, int>, IAnswerInMessageRepository
    {
        public AnswerInMessageRepository(ApplicationDataBaseContext context) : base(context)
        {
        }
        public async Task<AnswerInMessage> AddNewAnswerInMessageAsync(NewAnswerInMessage newAnswerInMessage, string userId)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if(user != null)
                {
                    AnswerInMessage? answerInMessage = new AnswerInMessage
                    (
                        newAnswerInMessage.AnswerId,
                        newAnswerInMessage.AnsweredMessageId
                    );
                    await _context.AnswersInMessages.AddAsync(answerInMessage);
                    await _context.SaveChangesAsync();
                    return answerInMessage;
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            catch(Exception)
            {
                throw new DatabaseGlobalException();
            }
        } 
        public async Task DeleteAnswerInMessageAsync(int id, string userId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) throw new UserNotFoundException();

            AnswerInMessage? answerInMessage = await _context.AnswersInMessages.FindAsync(id);
            if(answerInMessage == null) 
            {
                throw new AnswerInMessageNotFoundException();
            }
            else
            {
                 _context.AnswersInMessages.Remove(answerInMessage);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<AnswerInMessage> EditAnswerInMessageAsync(int id, string userId, EditAnswerInMessage editAnswerInMessage)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) throw new UserNotFoundException();

            AnswerInMessage? answerInMessage = await _context.AnswersInMessages.FindAsync(id);
            if(answerInMessage == null) throw new AnswerInMessageNotFoundException();

            answerInMessage.AnswerId = editAnswerInMessage.AnswerId;
            answerInMessage.AnsweredMessageId = editAnswerInMessage.AnsweredMessageId;

            await _context.SaveChangesAsync();
            return answerInMessage;
        }
        public async Task<AnswerInMessage> GetAnswerInMessageByIdAsync(int id)
        {
            AnswerInMessage? answerInMessage = await _context.AnswersInMessages.FindAsync(id);
            if (answerInMessage == null)
            {
                throw new AnswerInMessageNotFoundException();
            }
            return answerInMessage;
        }
        public async Task<List<AnswerInMessage>> GetAllAnswersInMessagesAsync()
        {
            List<AnswerInMessage>? answersInMessages = await _context.AnswersInMessages.ToListAsync();
            if (answersInMessages == null || answersInMessages.Count == 0)
            {
                throw new AnswerInMessageNotFoundException();
            }
            return answersInMessages;
        }
    }
}