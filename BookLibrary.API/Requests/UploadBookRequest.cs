using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.API.Requests
{
    public class UploadBookRequest
    {
        public string FileName { get; set; }

        public IFormFile FormFile { get; set; }

        public int UserId { get; set; }
    }
}
