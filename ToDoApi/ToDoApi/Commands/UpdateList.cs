using MediatR;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class UpdateList
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public ToDoList ToDoList { get; set; } = new();
            public string Owner { get; set; } = string.Empty;
        }
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly ToDoListContext _toDoListContext;
            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var list = _toDoListContext.ToDoLists.FirstOrDefault(x => x.Id == request.Id && x.Owner == request.Owner);
                if (list != null)
                {
                    list.Update(request.ToDoList);
                    _toDoListContext.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
        }
    }
}
