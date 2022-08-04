using MediatR;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Queries
{
    public static class GetItemById
    {
        public class Query : IRequest<ToDoListItem?>
        {
            public Guid IdList { get; set; }
            public Guid IdItem { get; set; }
            public string Owner { get; set; } = string.Empty;
        }
        public class RequestHandler : IRequestHandler<Query, ToDoListItem?>
        {
            private readonly ToDoListContext _toDoListContext;

            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }

            public Task<ToDoListItem?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.FirstOrDefault(x => x.Id == request.IdList && x.Owner == request.Owner);

                if (list != null)
                {
                    var listItem = list.Items.FirstOrDefault(x => x.Id == request.IdItem);
                    return Task.FromResult(listItem);
                }
                return Task.FromResult(null as ToDoListItem);
            }
        }
    }
}
