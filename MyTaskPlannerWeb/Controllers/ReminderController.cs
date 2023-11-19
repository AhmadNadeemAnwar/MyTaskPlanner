using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using MyTaskPlanner.DataAccess.Data;
using MyTaskPlanner.Entities.Models;
using MyTaskPlanner.Entities.ViewModels;
using System.Threading.Tasks;

namespace MyTaskPlannerWeb.Controllers
{
	public class ReminderController : Controller
	{
		AppDBContext _appDBContext { get; set; }

		public ReminderController(AppDBContext appDBContext)
		{
			_appDBContext = appDBContext;
		}

		public IActionResult ShowReminderDialog(int taskId)
		{
			if(taskId > 0)
			{
				TempData["TaskId"] = taskId;

				return PartialView("_AddReminder");
			}

			return NoContent();
		}

		[HttpPost]
        public IActionResult Add(Reminder reminder)
        {
			int taskId = Convert.ToInt32(TempData["TaskId"]);
            if (taskId > 0)
            {
				reminder.TaskId = taskId;
				
				_appDBContext.Reminders.Add(reminder);
				_appDBContext.SaveChanges();
            }

            return NoContent();
        }

		[HttpPost]
		public IActionResult SetReminderOffByID(int reminderId) 
		{
			if (reminderId > 0)
			{
				var reminder = _appDBContext.Reminders.FirstOrDefault(p => p.ReminderId == reminderId);
				
				if(reminder != null)
				{
					reminder.IsReminderRepeat = false;
					_appDBContext.Reminders.Update(reminder);
					_appDBContext.SaveChanges();
				}
			}

			return NoContent();
		}
    }
}
