using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int LifeTime { get; set; }
    }
}
