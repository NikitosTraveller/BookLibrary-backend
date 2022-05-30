using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.API.Requests
{
    public class PostCommentRequest
    {
        public string Message { get; set; }

        public int BookId { get; set; }
    }
}
