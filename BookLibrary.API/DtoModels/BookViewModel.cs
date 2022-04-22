using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string AuthorName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime Date { get; set; }
    }
}
