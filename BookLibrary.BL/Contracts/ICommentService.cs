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
        IEnumerable<Comment> GetCommentsForBook(int bookId);

        void DeleteComment(int commentId);

        Comment PostComment(Comment comment, int userId);
    }
}
