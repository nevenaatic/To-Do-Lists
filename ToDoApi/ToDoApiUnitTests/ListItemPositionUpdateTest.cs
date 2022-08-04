using FakeItEasy;
using ToDoApi.Commands;
using ToDoApi.Exceptions;
using ToDoApi.Services;
using ToDoCore;
using Command = ToDoApi.Commands.ListItemPositionUpdate.Command;

namespace ToDoApiUnitTests
{
    public class ListItemPositionUpdateTest
    {
        private IToDoService toDoService;
        private ListItemPositionUpdate.RequestHandler _requestHandler;
        private static ToDoListItem Some => new()
        {
            Id = new Guid("3C708FA1-D383-437E-BE35-06E8E6DC7568"),
            Position = 1
        };
        private static ToDoListItem Another => new()
        {
            Position = 2,
            Id = new Guid("5F83E760-CFA1-4470-B9A0-E4043FC2D9B6")

        };
        private static ToDoListItem YetAnother => new()
        {
            Id = new Guid("FBCFBFCE-1C2C-4F46-BBEF-A7CB9FC256A8"),
            Position = 3
        };

        private static ToDoList SomeList => new()
        {
            Id = new Guid("85127673-F9D1-4973-94FA-333096E1E345"),
            Items = new()
            {
                Some, Another, YetAnother
            },
            Owner = "nevena.atic@hotmail.com"
        };

        [SetUp]
        public void Setup()
        {
            toDoService = A.Fake<IToDoService>();

            _requestHandler = new ListItemPositionUpdate.RequestHandler(toDoService);
        }

        [Test]
        public async Task ListNotFoundAsync()
        {
            A.CallTo(() => toDoService.FindById(A<Guid>.Ignored, A<string>.Ignored)).Returns(null as ToDoList);
            var result = await _requestHandler.Handle(new Command { Position = 5 }, CancellationToken.None);
            Assert.That(result, Is.False);
        }

        [Test]
        public void PositionUpdateFailed()
        {
            Assert.Throws<PositionUpdateException>(() => _requestHandler.Handle(new Command { Position = 0 }, CancellationToken.None));
        }

        [Test]
        public void PositionCountFailed()
        {
            Assert.Throws<PositionUpdateException>(() => _requestHandler.Handle(new Command { Position = 5, ListId = SomeList.Id, ItemId = YetAnother.Id }, CancellationToken.None));
        }
        [Test]
        public async Task PositionUpdated()
        {
            A.CallTo(() => toDoService.FindById(SomeList.Id, SomeList.Owner)).Returns(SomeList);
            var result = await _requestHandler.Handle(new Command { Position = 1, ListId = SomeList.Id, ItemId = YetAnother.Id }, CancellationToken.None);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ListItemNotFound()
        {
            A.CallTo(() => toDoService.FindById(SomeList.Id, SomeList.Owner)).Returns(SomeList);

            var result = await _requestHandler.Handle(new Command { Position = 1, ListId = SomeList.Id, ItemId = new Guid("FBCFBFCE-1C1C-4F46-BBEF-A7CB9FC256A8") }, CancellationToken.None);
           
            Assert.That(result, Is.False);
        }
    }
}
