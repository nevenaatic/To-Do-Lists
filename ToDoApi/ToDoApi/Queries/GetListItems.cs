using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Queries
{
    public static class GetListItems
    {
        public class Query : IRequest<List<ToDoListItem>?>
        {
            public string Owner { get; set; } = string.Empty;
            public Guid Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, List<ToDoListItem>?>
        {
            public readonly ToDoListContext _toDoListContext;
            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext)); ;
            }

            public Task<List<ToDoListItem>?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.Include(x => x.Items).FirstOrDefault(x => x.Id == request.Id && x.Owner == request.Owner);
                if (list != null)
                {
                    return Task.FromResult(list.Items.OrderBy(x => x.Position).ToList() ?? null as List<ToDoListItem>); //MUST CHANGE!
                }
                return Task.FromResult(null as List<ToDoListItem>);
            }
        }
    }
}
