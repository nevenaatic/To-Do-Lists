using MediatR;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class NewToDoList
    {
        public class Command : IRequest<ToDoList>
        {
            public ToDoList List { get; set; } = new();
            public string Owner { get; set; } = string.Empty;
        }

        public class RequestHandler : IRequestHandler<Command, ToDoList>
        {
            private readonly ToDoListContext _toDoListContext;

            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }
            public Task<ToDoList> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                request.List.Owner = request.Owner;
                request.List.Position = _toDoListContext.ToDoLists.ToList().Count() + 1;
                var res = _toDoListContext.ToDoLists.Add(request.List);
                _toDoListContext.SaveChanges();
                return Task.FromResult(res.Entity);
            }
        }

    }
}
