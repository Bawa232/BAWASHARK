﻿using BAWASHARK.Data;
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
    }
}
