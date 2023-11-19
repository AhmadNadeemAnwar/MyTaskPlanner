using System.ComponentModel;

namespace MyTaskPlanner.Utilities
{
    public enum TaskType
    {
        [Description("None")]
        None = 0,

        [Description("Planned")]
        Planned,

        [Description("Pending")]
        Pending,

        [Description("Completed")]
        Completed
    }

    public enum NoteStatus
    {
        [Description("None")]
        None = 0,

        [Description("Read")]
        Read,

        [Description("Unread")]
        Unread
    }

    public enum ReminderRepeatType
    {
        [Description("None")]
        None = 0,

		[Description("Daily")]
		Daily,

		[Description("Monthly")]
		Monthly,

		[Description("Yearly")]
		Yearly,
	}
}