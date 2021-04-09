using System;
using System.Collections.Generic;
using System.Text;

namespace HW4._7._21QASite.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionDescription { get; set; }
        public int UserId { get; set; }
        public DateTime DatePosted { get; set; }
        public int Likes { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
