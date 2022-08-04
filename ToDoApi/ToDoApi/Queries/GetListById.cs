using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Queries
{
    public static class GetListById
    {
        public class Query : IRequest<ToDoList?>
        {
            public string Owner { get; set; }= string.Empty;
            public Guid Id { get; set; }
        }
        public class RequestHandler : IRequestHandler<Query, ToDoList?>
        {
            private readonly ToDoListContext _toDoListContext;

            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }
            public Task<ToDoList?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.Include(x => x.Items.OrderBy(item => item.Position)).FirstOrDefault(x => x.Id == request.Id && x.Owner == request.Owner);
                return Task.FromResult(list);
            }
        }
    }
}
