using Microsoft.EntityFrameworkCore;
using MyTaskPlanner.Entities.Models;
using MyTaskPlanner.Utilities;
using TaskTickets = MyTaskPlanner.Entities.Models.TaskTickets;

namespace MyTaskPlanner.DataAccess.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<TaskTickets> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}