using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MagicMirror.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace MagicMirror.Models
{
    public class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            GoalContext context = app.ApplicationServices
                .GetRequiredService<GoalContext>();
            //context.Database.Migrate();
            if (!context.Goal.Any())
            {
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
