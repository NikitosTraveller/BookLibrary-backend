using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        public List<Comment> Comments { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
