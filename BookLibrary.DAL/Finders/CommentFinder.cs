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

        public Task<List<Comment>> GetCommentsForBookAsync(int bookId)
        {
            return Entities.Where(_ => _.BookId == bookId).Include(c => c.User).ToListAsync();
        }

        public Comment? GetPostedComment(int commentId)
        {
            return Entities.Include(c => c.User).FirstOrDefault(_ => _.Id == commentId);
        }
    }
}
