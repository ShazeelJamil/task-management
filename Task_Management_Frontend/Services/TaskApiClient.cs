using SharedModels;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Task_Management_Frontend.Services
{
    public class TaskApiClient
    {
        private readonly HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TaskModel>>("/tasks");
        }

        public async Task<TaskModel> GetTaskByIdAsync(Guid taskId)
        {
            return await _httpClient.GetFromJsonAsync<TaskModel>($"/taskById?taskId={taskId}");
        }

        public async Task<bool> AddTaskAsync(TaskModel taskModel)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(taskModel.id.ToString()), "id" },
                { new StringContent(taskModel.title), "title" },
                { new StringContent(taskModel.description), "description" },
                { new StringContent(taskModel.status), "status" }
            };

            var response = await _httpClient.PostAsync("/addTask", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskByIdAsync(Guid taskId)
        {
            var response = await _httpClient.DeleteAsync($"/DeleteTaskById?taskId={taskId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(TaskModel taskModel)
        {
            var response = await _httpClient.PutAsJsonAsync("/UpdateTaskById", taskModel);
            return response.IsSuccessStatusCode;
        }

    }
}
