using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.API.Responses
{
    public class BookResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string AuthorName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime Date { get; set; }
    }
}
