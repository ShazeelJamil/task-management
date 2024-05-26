using Microsoft.EntityFrameworkCore;
using SharedModels;
namespace Task_Management_Web_APIs.DataContext
{
    public class TaskManagementDBContext : DbContext
    {
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options) : base(options)
        {

        }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
