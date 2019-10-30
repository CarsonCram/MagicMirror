using Microsoft.EntityFrameworkCore;
using MagicMirror.Models;

namespace MagicMirror.Data
{
    public class GoalContext : DbContext
    {
        public GoalContext(DbContextOptions<GoalContext> options)
            : base(options) { }

        public DbSet<Goal> Goal { get; set; }
    }
}