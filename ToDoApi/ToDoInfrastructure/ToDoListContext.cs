using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure.Configuration;

namespace ToDoInfrastructure
{
    public class ToDoListContext : DbContext
    {
       public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) { }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoListItem> ToDoListItems { get; set; }
        public DbSet<SharedList> SharedLists { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoListConfiguration());
            modelBuilder.ApplyConfiguration(new ToDoListItemConfiguration());
            modelBuilder.ApplyConfiguration(new SharedListConfiguration());
        }
    }
}
