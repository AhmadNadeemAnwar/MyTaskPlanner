using MyTaskPlanner.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskPlanner.Entities.ViewModels
{
    public class NoteVM
    {
        public Note Note { get; set; }

        public IEnumerable<Note> Notes { get; set; }
    }
}
