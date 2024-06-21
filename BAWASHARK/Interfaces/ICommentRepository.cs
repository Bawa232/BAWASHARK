using BAWASHARK.Models;

namespace BAWASHARK.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
    }
}
