using HW4._7._21QASite.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HW4._7._21QASite.Data;

namespace HW4._7._21QASite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        
        public IActionResult Index()
        {
            var repo = new QuestionsRepository(_connectionString);
            var vm = new IndexViewModel()
            {
                Questions = repo.GetQuestions()
            };
            return View(vm);
        }
    }
}
