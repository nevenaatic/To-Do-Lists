using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class UpdateListItem
    {
        public class Command : IRequest<bool>
        {
            public Guid ListId { get; set; }
            public ToDoListItem ListItem { get; set; } = new();
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
                var list = _toDoListContext.ToDoLists.Include(list=> list.Items).FirstOrDefault(x => x.Id == request.ListId && x.Owner == request.Owner);
               if (list != null)
                {
                    var listItem = list.Items.FirstOrDefault(i => i.Id == request.ListItem.Id);
                    if (listItem != null)
                    {
                        listItem.Update(request.ListItem);
                        _toDoListContext.SaveChanges();
                        return Task.FromResult(true);
                    }
                }
                return Task.FromResult(false);
            }
        }
    }
}
