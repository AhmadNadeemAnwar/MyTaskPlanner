using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskPlanner.Entities.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public TaskTickets Ticket { get; set; }

        [Required]
        public string NoteName { get; set; }

        [Required]
        public string NoteDescription { get; set; }

        [Required]
        public string NoteCreationDate { get; set; }

        public int NotePriority { get; set; }

        public bool NoteStatus { get; set; }
    }
}
