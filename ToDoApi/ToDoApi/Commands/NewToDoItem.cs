using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class NewToDoItem
    {
        public class Command : IRequest<ToDoListItem?>
        {
            public Guid ListId { get; set; }
            public ToDoListItem ListItem { get; set; } = new();
            public string Owner { get; set; } = string.Empty;
        }
        public class RequestHandler : IRequestHandler<Command, ToDoListItem?>
        {
            private readonly ToDoListContext _toDoListContext;

            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }
            public Task<ToDoListItem?> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.Include(x => x.Items).FirstOrDefault(l => l.Id == request.ListId && l.Owner == request.Owner);
                if (list != null)
                {
                    request.ListItem.Position = list.Items.Count() + 1;
                    list.Items.Add(request.ListItem);
                    _toDoListContext.SaveChanges();
                    return Task.FromResult(request.ListItem ?? null as ToDoListItem);  //MUST CHANGE!
                }
                return Task.FromResult(null as ToDoListItem);
            }
        }
    }
}
