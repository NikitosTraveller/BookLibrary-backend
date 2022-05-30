using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.API.Responses
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public string AuthorName { get; set; }

        public int AuthorId { get; set; }

        public int BookId { get; set; }
    }
}
