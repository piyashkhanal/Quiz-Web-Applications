using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

namespace QuizApi.Data
{
    public class QuizContext:DbContext

    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Participant> Participants { get; set; }

    }
}
