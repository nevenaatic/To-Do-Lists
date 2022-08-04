using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Queries
{
    public static class GetSharedToDoList
    {
        public class Query : IRequest<ToDoList?>
        {
            public Guid Link { get; set; }
        }
        public class RequestHandler : IRequestHandler<Query, ToDoList?>
        {
            public readonly ToDoListContext _toDoListContext;
            public RequestHandler(ToDoListContext toDoListContext)
            {
                _toDoListContext = toDoListContext ?? throw new ArgumentNullException(nameof(toDoListContext)); ;
            }
            public Task<ToDoList?> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var sharedList = _toDoListContext.SharedLists.FirstOrDefault(l => l.GeneratedLink == request.Link);
                if (sharedList != null && sharedList.ValidTime > DateTime.Now)
                {
                    var list = _toDoListContext.ToDoLists.Include(x => x.Items).FirstOrDefault(x => x.Id == sharedList.ListId);
                    return Task.FromResult(list);
                }
                return Task.FromResult(null as ToDoList); 
            }
        }
    }
}
