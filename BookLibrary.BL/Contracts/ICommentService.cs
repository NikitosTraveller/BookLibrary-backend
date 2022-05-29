using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.BL.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsForBookAsync(int bookId);

        Task DeleteCommentAsync(int commentId);

        Task<Comment?> PostCommentAsync(Comment comment, int userId);
    }
}
