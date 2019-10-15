using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MagicMirror.Data;
using System;
using System.Linq;

namespace MagicMirror.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GoalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GoalContext>>()))
            {
               // Look for and goals.
                if (context.Goal.Any())
                    {
                        return; //Db has been seeded
                    }

                context.Goal.AddRange(
                    new Goal
                    {
                        Title = "You can add tasks by pressing create new"
                    },
                    new Goal
                    {
                        Title = "And delete them with the delete button",
                    },
                    new Goal
                    {
                        Title = "Hope this helps!",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
