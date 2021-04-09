using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HW4._7._21QASite.Data
{
    class QuestionsTagsContextFactory : IDesignTimeDbContextFactory<QuestionsTagsContext>
    {
        public QuestionsTagsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}HW4.7.21QASite.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new QuestionsTagsContext(config.GetConnectionString("ConStr"));
        }
    }
}
