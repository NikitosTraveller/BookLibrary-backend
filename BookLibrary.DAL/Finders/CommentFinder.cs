using BookLibrary.DAL.Contracts;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Finders
{
    public class CommentFinder : Finder<Comment>, ICommentFinder
    {
        public CommentFinder(ApplicationContext context) : base(context)
        {

        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            return Entities.FirstOrDefaultAsync(comment => comment.Id == id);
        }

        public Task<List<Comment>> GetCommentsForBookAsync(int bookId)
        {
            return Entities.Where(book => book.BookId == bookId).Include(c => c.User).ToListAsync();
        }

        public Task<Comment?> GetPostedCommentAsync(int commentId)
        {
            return Entities.Include(c => c.User).FirstOrDefaultAsync(comm => comm.Id == commentId);
        }
    }
}
