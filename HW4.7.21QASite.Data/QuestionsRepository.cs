using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW4._7._21QASite.Data
{
    public class QuestionsRepository
    {
        private readonly string _connectionString;
        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Tag GetTag(string name)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }

        public int AddTag (string name)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            ctx.Tags.Add(new Tag
            {
                Name = name
            });
            ctx.SaveChanges();
            return ctx.Tags.FirstOrDefault(t => t.Name == name).Id;
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            ctx.Questions.Add(question);
            ctx.SaveChanges();

            foreach (string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if(t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }

                ctx.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }
            ctx.SaveChanges();
        }

        public List<Question> GetQuestions()
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            return ctx.Questions
                .Include(q => q.Answers)
                .Include(q => q.QuestionsTags)
                .ThenInclude(qt => qt.Tag)
                .OrderByDescending(q => q.DatePosted)
                .ToList();
        }

        public Question GetQuestionById(int id)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            return ctx.Questions
                //.Include(q => q.Answers)
                //.ThenInclude (a => a.User)
                .Include(q => q.QuestionsTags)
                .ThenInclude (qt => qt.Tag)
                .FirstOrDefault(q => q.Id == id);
        }

        public void AddAnswer(Answer answer)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            ctx.Answers.Add(answer);
            ctx.SaveChanges();
        }

        public void AddUser(User user, string password)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.HashPassword);
            return isValid ? user : null;
        }

        public User GetByEmail (string email)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        public string GetUserNameById(int id)
        {
            var ctx = new QuestionsTagsContext(_connectionString);
            return ctx.Users.Where(u => u.Id == id).Select(u => u.Name).FirstOrDefault();
        }
    }
}
