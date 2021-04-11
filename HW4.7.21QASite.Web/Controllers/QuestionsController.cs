using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW4._7._21QASite.Data;
using Microsoft.Extensions.Configuration;
using HW4._7._21QASite.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace HW4._7._21QASite.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly string _connectionString;
        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }

        public IActionResult Add(Question question, List<string> tags)
        {
            question.UserId = GetCurrentUserId().Value;
            question.DatePosted = DateTime.Now;
            var repo = new QuestionsRepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/home/index");
        }

        public IActionResult ViewQuestion (int id)
        {
            var repo = new QuestionsRepository(_connectionString);
            var vm = new ViewQuestionViewModel();
            vm.Question = repo.GetQuestionById(id);
            vm.AskedBy = repo.GetUserNameById(vm.Question.UserId);
            vm.CanLike = repo.CanLike(id, GetCurrentUserId().Value);
            return View(vm);
        }

        public IActionResult AddAnswer(Answer answer)
        {
            var repo = new QuestionsRepository(_connectionString);
            answer.DateAnswered = DateTime.Now;
            answer.UserId = GetCurrentUserId().Value;
            repo.AddAnswer(answer);
            return this.RedirectToAction
            ("ViewQuestion", new { id = $"{answer.QuestionId}" });
        }

        [HttpPost]
        public IActionResult AddLike(int questionId)
        {
            var repo = new QuestionsRepository(_connectionString);
            var user = repo.GetByEmail(User.Identity.Name);
            var like = new Likes()
            {
                UserId = user.Id,
                QuestionId = questionId
            };
            repo.AddLikes(like);
            return Json(null);
        }

        public IActionResult GetLikes(int questionId)
        {
            var repo = new QuestionsRepository(_connectionString);
            return Json(repo.GetLikes(questionId));
        }

        private int? GetCurrentUserId()
        {
            var repo = new QuestionsRepository(_connectionString);
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var user = repo.GetByEmail(User.Identity.Name);
            if(user == null)
            {
                return null;
            }

            return user.Id;
        }


        
    }
}
