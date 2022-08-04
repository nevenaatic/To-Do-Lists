using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class NewSharedList
    {
        public class Command : IRequest<SharedList?>
        {
            public SharedList SharedList { get; set; } = new();
            public string Owner { get; set; } = string.Empty;
        }
        public class RequestHandler : IRequestHandler<Command, SharedList?>
        {
            private readonly ToDoListContext _toDoListContext;

            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }
            public Task<SharedList?> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.Include(x => x.Items).FirstOrDefault(l => l.Id == request.SharedList.ListId && l.Owner == request.Owner);
                if (list != null)
                {
                    request.SharedList.ValidTime = DateTime.Now.AddHours(2);
                    _toDoListContext.SharedLists.Add(request.SharedList);
                    _toDoListContext.SaveChanges();
                    return Task.FromResult(request.SharedList ?? null as SharedList);
                }
                return Task.FromResult(null as SharedList); 
            }
        }
    }
}
