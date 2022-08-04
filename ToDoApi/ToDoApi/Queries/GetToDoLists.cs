using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Pagination;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Queries
{
    public static class GetToDoLists
    {
        public class Query : IRequest<Response>
        {
            public string ListOwnerEmail { get; set; } = string.Empty;
            public PaginationFilter PaginationFilter { get; set; } = new();
        }
        public class Response
        {
            public IEnumerable<ToDoList>? ToDoLists { get; set; }
            public int ListSize { get; set; }
        }
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ToDoListContext _toDoListContext;

            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext));
            }

            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var result = _toDoListContext.ToDoLists.Include(l => l.Items)
                    .Where(x => x.Owner == request.ListOwnerEmail)
                    .OrderByDescending(l => l.Position)
                    .Skip((request.PaginationFilter.PageNumber - 1) * request.PaginationFilter.PageSize)
                    .Take(request.PaginationFilter.PageSize)
                    .AsEnumerable();
                return Task.FromResult(new Response { ToDoLists = result, ListSize = _toDoListContext.ToDoLists.Count() });
            }
        }
    }
}
