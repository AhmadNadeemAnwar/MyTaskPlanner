using MyTaskPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskPlanner.Entities.Models
{
	public class Reminder
	{
		[Key]
		public int ReminderId { get; set; }

		public int TaskId { get; set; }

		[ForeignKey("TaskId")]
        public TaskTickets Ticket { get; set; }

        [Required]
        public DateTime ReminderDate { get; set; }

        [Required]
        public DateTime ReminderTime { get; set; }

        public bool IsReminderRepeat { get; set; }

		public ReminderRepeatType ReminderRepeatType { get; set; }

		public int ReminderRepeatInterval { get; set; }

		public bool IsSyncReminderWithCalendar {  get; set; }
    }
}
