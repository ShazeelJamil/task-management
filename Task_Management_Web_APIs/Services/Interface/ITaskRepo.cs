

using SharedModels;

namespace Task_Management_Web_APIs.Services.Interface
{
    public interface ITaskRepo
    {
        public List<TaskModel> GetAllProducts();
        Task<TaskModel> GetProduct(Guid id);
        Task<int> AddTask(TaskModel model);
        Task<bool> DeleteProduct(Guid task_id);
        Task<bool> Update(TaskModel model);
    }
}
