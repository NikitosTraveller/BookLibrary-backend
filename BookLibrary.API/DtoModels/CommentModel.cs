using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.ViewModels
{
    public class CommentModel
    {
        public string Message { get; set; }

        public int BookId { get; set; }
    }
}
