using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Queries;

namespace ToDoApi.Controllers
{
    [Authorize]
    [Route("api/share")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShareController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var result = await _mediator.Send(new GetSharedToDoList.Query { Link = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
