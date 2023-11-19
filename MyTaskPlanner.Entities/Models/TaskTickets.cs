using MyTaskPlanner.Utilities;
using System.ComponentModel.DataAnnotations;

namespace MyTaskPlanner.Entities.Models
{
    public class TaskTickets
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }

        public TaskType TaskType { get; set; }
        public List<Note> TaskNotes { get; set; }
    }
}