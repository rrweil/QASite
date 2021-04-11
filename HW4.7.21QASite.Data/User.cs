using System;
using System.Collections.Generic;
using System.Text;

namespace HW4._7._21QASite.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set;  }
        public string HashPassword { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Likes> Likes { get; set; }

    }
}
