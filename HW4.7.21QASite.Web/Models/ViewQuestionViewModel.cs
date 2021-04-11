using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW4._7._21QASite.Data;

namespace HW4._7._21QASite.Web.Models
{
    public class ViewQuestionViewModel
    {
        public Question Question { get; set; }
        public string AskedBy { get; set; }
    }
}
