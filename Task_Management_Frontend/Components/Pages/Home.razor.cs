using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedModels;
using Task_Management_Frontend.Services;

namespace Task_Management_Frontend.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private TaskApiClient _taskApiClient { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        public TaskModel Selected_Model { get; set; } = new TaskModel();

        private bool isLoading = true;

        public List<TaskModel> TaskList = new List<TaskModel>();

        protected override async Task<Task> OnInitializedAsync()
        {
            // Start loading data in the background for better UX
            _ = LoadTasksInBackground();
            return base.OnInitializedAsync();
        }
        private async Task LoadTasksInBackground()
        {
            TaskList = await _taskApiClient.GetAllTasksAsync();
            isLoading = false;
            StateHasChanged();
        }
        private async Task DeleteTask(Guid taskId)
        {
            isLoading = true;
            var success = await _taskApiClient.DeleteTaskByIdAsync(taskId);
            if (success)
            {

                TaskList = TaskList.Where(t => t.id != taskId).ToList(); // deleteing the task from view
                JSRuntime.InvokeVoidAsync("showAlert", "Task Deleted Successfully!");

            }
            else
            {
                JSRuntime.InvokeVoidAsync("showAlert", "Error deleting task. Please try again.");
            }
            isLoading = false;
        }

        private async Task OpenDetailsAsync(TaskModel selectedmodel)
        {
            Selected_Model = selectedmodel;//to display the selected Task in the Modal.PopUp
            await JSRuntime.InvokeVoidAsync("showModal");
        }

        private void OpenInEditMode(Guid taskId)
        {
            Navigation.NavigateTo($"/addTask/{taskId}");
        }

    }
}
