﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HW4._7._21QASite.Data
{
    public class QuestionsTags
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; } 
    }
}
