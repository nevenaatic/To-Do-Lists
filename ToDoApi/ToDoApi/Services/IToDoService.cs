using ToDoCore;

namespace ToDoApi.Services
{
    public interface IToDoService
    {
        ToDoList? FindById(Guid id, string owner);
        void SaveChanges();
    }
}
