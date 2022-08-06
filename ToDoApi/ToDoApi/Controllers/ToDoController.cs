using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net;
using ToDoApi.Commands;
using ToDoApi.Exceptions;
using ToDoApi.Extensions;
using ToDoApi.Pagination;
using ToDoApi.Queries;
using ToDoCore;


namespace ToDoApi.Controllers
{
    [Authorize]
    [Route("api/to-do-lists")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IMediator _mediator;
        const string scopes = "read_access";
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(IMediator mediator, ILogger<ToDoController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
            _logger.LogInformation("teststststst");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope("to-do-lists.read");
            var lists = await _mediator.Send(new GetToDoLists.Query { ListOwnerEmail = User.GetEmail(), PaginationFilter = filter });
            _logger.LogDebug("ToDoLists.GetAll() executed!");
            return Ok(lists);
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ToDoList))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListByIdAsync([FromRoute] Guid id)
        {
            var list = await _mediator.Send(new GetListById.Query { Id = id, Owner = User.GetEmail()});
            return list != null ? Ok(list) : NotFound();
        }

        [HttpPost()]
        public async Task<IActionResult> NewToDoListAsync([FromBody] ToDoList list)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope("to-do-lists.write");
            var createdList = await _mediator.Send(new NewToDoList.Command { List = list, Owner = User.GetEmail()});
            if (createdList == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetListById), new { id = createdList.Id }, createdList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteList.Command { Id = id, Owner = User.GetEmail()});
            return result ? Ok() : NotFound();
        }

        [HttpPost("{id}/to-do-items")]
        public async Task<IActionResult> NewToDoItemAsync([FromRoute] Guid id, [FromBody] ToDoListItem item)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope("to-do-lists.write");
            var createdItem = await _mediator.Send(new NewToDoItem.Command { ListId = id, ListItem = item, Owner = User.GetEmail()});
            if (createdItem == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetItemById), new { listId = id, itemId = createdItem.Id }, createdItem);
        }


        [HttpDelete("{listId}/to-do-items/{itemId}")]
        public async Task<IActionResult> DeleteListItem([FromRoute] Guid listId, [FromRoute] Guid itemId)
        {
            var result = await _mediator.Send(new DeleteListItem.Command { ListId = listId, ItemId = itemId, Owner = User.GetEmail()});
            return result ? Ok() : NotFound();
        }

        [HttpPut("{listId}/to-do-items/{itemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid itemId, [FromRoute] Guid listId, [FromBody] ToDoListItem listItem)
        {
            var result = await _mediator.Send(new UpdateListItem.Command { ListId = listId, ListItem = listItem, Owner = User.GetEmail()});
            return result ? Ok() : NotFound();
        }

        [HttpPut("{listId}")]
        public async Task<IActionResult> UpdateList([FromRoute] Guid listId, [FromBody] ToDoList changedList)
        {
            var result = await _mediator.Send(new UpdateList.Command { Id = listId, ToDoList = changedList, Owner = User.GetEmail()});
            return result ? Ok() : NotFound();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromQuery]string? searchText, [FromBody] PaginationFilter filter)
        {
            var lists = await _mediator.Send(new SearchList.Query { SearchParameter = searchText ?? "", Owner = User.GetEmail(), PaginationFilter = filter});
            return lists != null ? Ok(lists) : NotFound();
        }

        [HttpGet("{listId}/to-do-items")]
        public async Task<IActionResult> GetListItems([FromRoute] Guid listId)
        {
            var listItems = await _mediator.Send(new GetListItems.Query { Id = listId, Owner = User.GetEmail()});
            return listItems != null ? Ok(listItems) : NotFound();
        }

        [HttpGet("{listId}/to-do-item/{itemId}")]
        public async Task<IActionResult> GetItemById([FromRoute] Guid listId, [FromRoute] Guid itemId)
        {
            var listItem = await _mediator.Send(new GetItemById.Query { IdList = listId, IdItem = itemId, Owner = User.GetEmail()});
            return listItem != null ? Ok(listItem) : NotFound();
        }

        [HttpPut("{listId}/to-do-list/{position}")]
        public async Task<IActionResult> ListPositionUpdate([FromRoute] Guid listId, [FromRoute] int position)
        {
            try
            {
                var result = await _mediator.Send(new ListPositionUpdate.Command { ListId = listId, Position = position, Owner = User.GetEmail()});
                return result ? Ok() : NotFound();
            }
            catch (PositionUpdateException)
            {
                return BadRequest();
            }
        }
        [HttpPut("{listId}/to-do-items/{itemId}/{position}")]
        public async Task<IActionResult> ListItemPositionUpdate([FromRoute] Guid listId, [FromRoute] Guid itemId, [FromRoute] int position)
        {
            try
            {
                var result = await _mediator.Send(new ListItemPositionUpdate.Command { ListId = listId, ItemId = itemId, Position = position, Owner = User.GetEmail()});
                return result ? Ok() : NotFound();
            }
            catch (PositionUpdateException)
            {
                return BadRequest();
            }
        }

        [HttpPost("share")]
        public async Task<IActionResult> NewSharedList([FromBody] SharedList sharedList)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope("to-do-lists.write");
            var createdList = await _mediator.Send(new NewSharedList.Command { SharedList = sharedList, Owner = User.GetEmail() });
            if (createdList == null)
            {
                return NotFound();
            }
            return Ok(createdList.GeneratedLink);
        }
    }
}

