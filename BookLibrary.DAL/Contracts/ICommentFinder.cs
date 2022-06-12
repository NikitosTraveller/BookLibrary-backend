using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Contracts
{
    public interface ICommentFinder : IFinder<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsForBookAsync(int bookId);

        Comment? GetPostedComment(int commentId);
    }
}
