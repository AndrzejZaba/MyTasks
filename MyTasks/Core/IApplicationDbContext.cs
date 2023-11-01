using Microsoft.EntityFrameworkCore;
using MyTasks.Core.Models.Domains;
using Task = MyTasks.Core.Models.Domains.Task;

namespace MyTasks.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Task> Tasks { get; set; }

        DbSet<Category> Categories { get; set; }
        int SaveChanges();
    }
}
