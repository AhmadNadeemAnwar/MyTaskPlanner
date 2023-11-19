using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyTaskPlanner.DataAccess.Data;
using MyTaskPlanner.Entities.Models;
using MyTaskPlanner.Entities.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using TaskTickets = MyTaskPlanner.Entities.Models.TaskTickets;

namespace MyTaskPlannerWeb.Controllers
{
    public class HomeController : Controller
    {
        public TaskTicketVM Tasks { get; set; }
        
        AppDBContext _appDBContext;

        public HomeController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            Tasks = new TaskTicketVM();
        }

        public IActionResult Index()
        {
            Tasks.TaskTickets = _appDBContext.Tasks;
            return View(Tasks);
        }

        [HttpPost]
        public IActionResult Index(TaskTicketVM taskTicketVM, string id)
        {
            if(taskTicketVM.TaskTicket != null)
            {
                _appDBContext.Tasks.Add(taskTicketVM.TaskTicket);
                _appDBContext.SaveChanges();
            }

            Tasks.TaskTickets = _appDBContext.Tasks;
            return View(Tasks);
        }

        public IActionResult Upsert()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Upsert(TaskTickets task)
        {
            if (!String.IsNullOrEmpty(task?.TaskName))
            {
                _appDBContext.Tasks.Add(task);
                _appDBContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);

        }

        public IActionResult Delete(int id)
        {
            var taskToBeDeleted = _appDBContext.Tasks.FirstOrDefault(p => p.TaskId == id);

            if (taskToBeDeleted != null)
            {
                _appDBContext.Tasks.Remove(taskToBeDeleted);
                _appDBContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult OpenCalendar(int id)
        {
            if(id > 0)
            {
                return PartialView("_AddReminder");
            }

            return NoContent();
        }

        public IActionResult ShowCalendar()
        {
            return View("TaskCalendar");
        }

        [HttpGet]
        public IActionResult GetCalendarTasks()
        {
            var reminderEvents = new List<Reminder>();

            foreach (var reminder in _appDBContext.Reminders.ToList())
            {
                reminder.Ticket = _appDBContext.Tasks.FirstOrDefault(p => p.TaskId == reminder.TaskId);

                //var reminderEvent = new JObject
                //    {
                //        { "id", reminder.ReminderId },
                //        { "start", reminder.ReminderDate.ToString("yyyy-MM-dd") },
                //        { "startTime", reminder.ReminderTime.ToString() },
                //        { "title", reminder.Ticket?.TaskName }
                //    };

                reminderEvents.Add(reminder);
            }

            //var jsonData = JsonConvert.SerializeObject(reminderEvents);

            return Json(reminderEvents);
        }

    }
}
