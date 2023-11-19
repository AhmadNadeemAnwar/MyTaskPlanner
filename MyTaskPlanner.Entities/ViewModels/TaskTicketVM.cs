using MyTaskPlanner.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskPlanner.Entities.ViewModels
{
    public class TaskTicketVM
    {
        public TaskTickets TaskTicket { get; set; }

        public IEnumerable<TaskTickets> TaskTickets { get; set; }

        public Note Note { get; set; }
    }
}
