using MediatR;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class DeleteList
    {
        public class Command : IRequest<bool>
        {
            public string Owner { get; set; } = string.Empty;   
            public Guid Id { get; set; }
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
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.FirstOrDefault(x => x.Id == request.Id && x.Owner == request.Owner);
                if (list != null)
                {
                    _toDoListContext.ToDoLists.Remove(list);
                    _toDoListContext.ToDoLists.Where(l => l.Position > list.Position).ToList().ForEach(l => l.Position--);
                    _toDoListContext.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
        }
    }
}
