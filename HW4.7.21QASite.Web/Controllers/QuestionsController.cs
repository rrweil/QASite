using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW4._7._21QASite.Data;
using Microsoft.Extensions.Configuration;
using HW4._7._21QASite.Web.Models;

namespace HW4._7._21QASite.Web.Controllers
{
    public class QuestionsController : Controller
    {
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public QuestionsController(IConfiguration configuration)
        {
           // _configuration = configuration;
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult AskAQuestion()
        {
            return View();
        }

        public IActionResult Add(Question question, List<string> tags)
        {
            //add the user id after the login system is set up
            //question.UserId ==
            question.DatePosted = DateTime.Now;
            var repo = new QuestionsRepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/home/index");
        }

        public IActionResult ViewQuestion (int id)
        {
            var repo = new QuestionsRepository(_connectionString);
            var vm = new ViewQuestionViewModel()
            {
                Question = repo.GetQuestionById(id)
            };
            return View(vm);
        }

        public IActionResult AddAnswer(Answer answer)
        {
            var repo = new QuestionsRepository(_connectionString);
            answer.DateAnswered = DateTime.Now;
            repo.AddAnswer(answer);
            return this.RedirectToAction
  ("ViewQuestion", new { id = $"{answer.QuestionId}" });
        }
        
    }
}
