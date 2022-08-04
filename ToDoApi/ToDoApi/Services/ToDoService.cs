using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoListContext _context;

        public ToDoService(ToDoListContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ToDoList? FindById(Guid id, string owner) => _context.ToDoLists.Include(x => x.Items).FirstOrDefault(x => x.Id == id && x.Owner == owner);

        public void SaveChanges() => _context.SaveChanges();
    }
}
