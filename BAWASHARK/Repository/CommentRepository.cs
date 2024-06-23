using BAWASHARK.Data;
using BAWASHARK.Interfaces;
using BAWASHARK.Models;
using Microsoft.EntityFrameworkCore;


namespace BAWASHARK.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment != null)
            {
                return null;
            }

            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existsComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (existsComment == null)
            {
                return null;
            }

            existsComment.Title = commentModel.Title;
            existsComment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return existsComment;
        }
    }
}
