using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialHub.Server.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        //??= is a null coalescing assignment operator
        //If _mediator is null, then it will be assigned to HttpContext.RequestServices.GetService<IMediator>()
        //If _mediator is not null, then it will be assigned to _mediator
        //No assignment is performed if the left-hand side is not nullish
        protected IMediator Mediator => _mediator ??= 
            HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccessful && result.Value != null)
            {
                return Ok(result.Value);
            }
            if (result.IsSuccessful && result.Value == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
        
    }

}
