using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class DeleteListItem
    {
        public class Command : IRequest<bool>
        {
            public Guid ListId { get; set; }
            public Guid ItemId { get; set; }

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
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var list = _toDoListContext.ToDoLists.Include(l => l.Items).FirstOrDefault(x => x.Id == request.ListId && x.Owner == request.Owner);
                if (list != null)
                {
                    var listItem = list.Items.FirstOrDefault(x => x.Id == request.ItemId);
                    if (listItem != null)
                    {
                        list.Items.Remove(listItem);
                        list.UpdatePositionsAfterDelete(listItem);
                        _toDoListContext.SaveChanges();
                        return Task.FromResult(true);
                    }
                }
                return Task.FromResult(false);
            }
        }
    }
}
