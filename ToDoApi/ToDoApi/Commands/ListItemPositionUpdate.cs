using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Exceptions;
using ToDoApi.Services;
using ToDoInfrastructure;

namespace ToDoApi.Commands
{
    public static class ListItemPositionUpdate
    {
        public class Command : IRequest<bool>
        {
            public Guid ListId { get; set; }
            public Guid ItemId { get; set; }
            public int Position { get; set; }
            public string Owner { get; set; } = string.Empty;
        }

        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IToDoService _toDoService;

            public RequestHandler(IToDoService toDoService)
            {
                _toDoService = toDoService ?? throw new ArgumentNullException(nameof(toDoService));
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Position < 1)
                {
                    throw new PositionUpdateException();
                }
                var list = _toDoService.FindById(request.ListId, request.Owner);
                if (list == null)
                {
                    return Task.FromResult(false);
                }

                if (request.Position > list.Items.Count + 1)
                {
                    throw new PositionUpdateException();
                }

                var listItem = list.Items.FirstOrDefault(i => i.Id == request.ItemId);

                if (listItem == null)
                {
                    return Task.FromResult(false);
                }

                list.UpdateItemPosition(listItem, request.Position);
                _toDoService.SaveChanges();
                return Task.FromResult(true);
            }
        }
    }
}
