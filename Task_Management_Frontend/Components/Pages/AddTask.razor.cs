using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedModels;
using Task_Management_Frontend.Services;

namespace Task_Management_Frontend.Components.Pages
{
    public partial class AddTask
    {
        [Inject]
        private TaskApiClient _taskApiClient { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; }

        [Parameter]
        public Guid? TaskId { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private TaskModel newTask = new TaskModel();
        private bool isLoading = false;

        public enum TaskStatus
        {
            Complete,
            Pending,
            InProgress
        }

        protected override async Task OnInitializedAsync()
        {
            if (TaskId.HasValue) // if update a task then load it from DB via API
            {
                isLoading = true;
                newTask = await _taskApiClient.GetTaskByIdAsync(TaskId.Value);
                isLoading = false;
            }
            newTask.status = "InProgress"; // to sync with the default selected values
        }

        private async Task HandleValidSubmit()
        {
            isLoading = true;
            var success = TaskId.HasValue //if update a task then call the update api else add the task
                ? await _taskApiClient.UpdateTaskAsync(newTask)
                : await _taskApiClient.AddTaskAsync(newTask);
            isLoading = false;
            StateHasChanged();

            if (success)
            {
                if (TaskId.HasValue)
                {
                    JSRuntime.InvokeVoidAsync("showAlert", "Task Updated Successfully!");
                }
                else 
                {
                    await JSRuntime.InvokeVoidAsync("showAlert", "Task Added Successfully!");
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("showAlert", "Error saving task. Please try again.");
            }
        }

        private async Task OpenDetailsAsync(TaskModel selectedmodel) // to open a selected model in the popup
        {
            await JSRuntime.InvokeVoidAsync("showModal");
        }
    }
}
