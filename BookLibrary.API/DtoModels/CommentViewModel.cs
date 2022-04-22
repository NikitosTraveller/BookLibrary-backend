using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public string AuthorName { get; set; }

        public int AuthorId { get; set; }

        public int BookId { get; set; }
    }
}
