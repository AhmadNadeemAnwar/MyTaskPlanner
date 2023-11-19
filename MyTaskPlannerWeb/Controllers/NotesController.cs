using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MyTaskPlanner.DataAccess.Data;
using MyTaskPlanner.Entities.Models;
using MyTaskPlanner.Entities.ViewModels;
using System.Windows;

namespace MyTaskPlannerWeb.Controllers
{
    public class NotesController : Controller
    {
        AppDBContext _appDBContext { get; set; }
        static NoteVM NoteVM { get; set; }

        public NotesController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            NoteVM = new NoteVM()
            {
                Note = new Note
                {
                    NoteCreationDate = DateTime.Now.ToString()
                }
            };
        }

        public IActionResult ShowDialog(string taskId)
        {
            TempData["TaskId"] = taskId;

            return PartialView("_AddNoteView");
        }


        public IActionResult Index(int id)
        {
            return PartialView(_appDBContext.Notes.ToList());
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (!string.IsNullOrEmpty(note.NoteName))
            {
                NoteVM.Note.NoteName = note.NoteName;
                NoteVM.Note.NoteDescription = note.NoteDescription;
                NoteVM.Note.NotePriority = note.NotePriority;
                NoteVM.Note.NoteCreationDate = DateTime.Now.ToString();
                NoteVM.Note.TaskId = Convert.ToInt32(TempData["TaskId"]);

                _appDBContext.Notes.Add(NoteVM.Note);
                _appDBContext.SaveChanges();
            }

            return NoContent();
        }

        public IActionResult DisplayNotesBoard(int taskId)
        {
            return PartialView("_NotesDetailBoard", _appDBContext.Notes?.Where(x=>x.TaskId == taskId)?.ToList());
        }

        [HttpPost]
        public IActionResult MarkNoteState(int id, string noteFilterIds)
        {
            var noteToBeUpdated = _appDBContext.Notes.FirstOrDefault(p=>p.NoteId == id);
            if (noteToBeUpdated != null)
            {
                noteToBeUpdated.NoteStatus = true;
                _appDBContext.Notes.Update(noteToBeUpdated);
                _appDBContext.SaveChanges();
            }

            if(noteFilterIds != null)
            {
                if (noteFilterIds == "note-read") return GetNotes(1);
                else if (noteFilterIds == "note-unread") return GetNotes(2);
                else return GetNotes(0);
            }
            return GetNotes(0);
        }

        [HttpGet]
        public IActionResult GetNotes(int id)
        {
            List<Note> notes = new List<Note>();

            if(id == 0)
            {
                notes = _appDBContext.Notes.ToList();
            }
            else if(id == 1)
            {
                notes = _appDBContext.Notes.Where(p => p.NoteStatus == true).ToList();
            }
            else if(id == 2)
            {
                notes = _appDBContext.Notes.Where(p => p.NoteStatus == false).ToList();
            }

            return Json(notes);
        }

        public IActionResult CopyNoteToClipboard(int id)
        {
            return NoContent();
        }
    }
}
