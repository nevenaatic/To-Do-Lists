using MediatR;
using ToDoApi.Exceptions;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class ListPositionUpdate
    {
        public class Command : IRequest<bool>
        {
            public Guid ListId { get; set; }
            public int Position { get; set; }
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
                if (request.Position < 1 && request.Position > _toDoListContext.ToDoLists.Count() + 1)
                {
                    throw new PositionUpdateException();
                }
                var list = _toDoListContext.ToDoLists.FirstOrDefault(l => l.Id == request.ListId && l.Owner == request.Owner);
                if (list != null)
                {
                    if (list.Position > request.Position)
                    {
                        _toDoListContext.ToDoLists.Where(x => x.Position >= request.Position && x.Position < list.Position)
                            .ToList()
                            .ForEach(x => x.Position++);
                    }
                    else
                    {
                        _toDoListContext.ToDoLists.Where(x => x.Position <= request.Position && x.Position < list.Position)
                            .ToList()
                            .ForEach(x => x.Position--);
                    }
                    list.Position = request.Position;
                    _toDoListContext.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
        }
    }
}
