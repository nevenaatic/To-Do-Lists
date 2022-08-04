using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Pagination;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Queries
{
    public static class SearchList
    {

        public class Query : IRequest<Response>
        {
            public string SearchParameter { get; set; } = string.Empty;
            public string Owner { get; set; } = string.Empty;
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
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext)); ;
            }
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var lists = _toDoListContext.ToDoLists
                    .Where(l => l.Title.Contains(request.SearchParameter) && l.Owner == request.Owner)
                    .Include(x => x.Items)
                    .OrderByDescending(x => x.Position);
                var result = lists.Skip((request.PaginationFilter.PageNumber - 1) * request.PaginationFilter.PageSize)
                    .Take(request.PaginationFilter.PageSize);
             
                return Task.FromResult(new Response { ToDoLists = result, ListSize = lists.Count() });

            }
        }
    }
}
