using Microsoft.EntityFrameworkCore;
using System;

namespace HW4._7._21QASite.Data
{
    public class QuestionsTagsContext : DbContext
    {
        private readonly string _connectionString;

        public QuestionsTagsContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionsTags> QuestionsTags { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Taken from here:
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

            //set up composite primary key
            modelBuilder.Entity<QuestionsTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });

            //set up foreign key from QuestionsTags to Questions
            modelBuilder.Entity<QuestionsTags>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionsTags)
                .HasForeignKey(q => q.QuestionId);

            //set up foreign key from QuestionsTags to Tags
            modelBuilder.Entity<QuestionsTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionsTags)
                .HasForeignKey(q => q.TagId);
        }
    }
}
