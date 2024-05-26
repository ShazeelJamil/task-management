using Microsoft.EntityFrameworkCore;
using SharedModels;
using Task_Management_Web_APIs.DataContext;
using Task_Management_Web_APIs.Services.Interface;

namespace Task_Management_Web_APIs.Services
{
    public class TaskRepoService : ITaskRepo
    {
        public TaskManagementDBContext _dbContext;

        public TaskRepoService(TaskManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TaskModel> GetAllProducts()
        {
            var tasks = _dbContext.Tasks.ToList();
            return tasks.Count > 0 ? tasks : new List<TaskModel>(); //If any Task  return them else Empty Task List
        }

        public async Task<TaskModel> GetProduct(Guid id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task != null) // If task found
            {
                return task;
            }
            return null ; // If no task found
        }

        public async Task<int> AddTask(TaskModel model)
        {
            await _dbContext.Tasks.AddAsync(model);
            return await _dbContext.SaveChangesAsync(); // return the number of rows effected 1 indicates inserted elso not inserted
        }

        public async Task<bool> DeleteProduct(Guid task_id)
        {
            var task = await _dbContext.Tasks.FindAsync(task_id);
            if (task == null)
            {
                return false; //if no task found
            }
            _dbContext.Tasks.Remove(task);
            var status = await _dbContext.SaveChangesAsync();
            return status > 0; // return true if deleted
        }

        public async Task<bool> Update(TaskModel model)
        {
            var task = await _dbContext.Tasks.FindAsync(model.id);
            if (task == null)
            {
                return false; // if no task found
            }

            task.title = model.title;
            task.description = model.description;
            task.status = model.status;

            _dbContext.Tasks.Update(task);
            var status = await _dbContext.SaveChangesAsync();
            return status > 0; // return true if updated

        }



    }
}
