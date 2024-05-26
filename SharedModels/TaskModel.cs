using System.ComponentModel.DataAnnotations;

namespace SharedModels
{
    public class TaskModel
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }

    }
}
