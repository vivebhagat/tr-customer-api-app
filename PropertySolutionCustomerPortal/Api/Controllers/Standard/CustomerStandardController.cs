using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Api.Dto.Auth;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionCustomerPortal.Domain.Entities.Users;

namespace PropertySolutionCustomerPortal.Api.Controllers.Standard
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class CustomerStandardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerStandardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<List<CustomerToRoleMap>> GetCustomerRoles(string userId)
        {
            return await _mediator.Send(new GetCustomerRolesByUserIdQuery { UserId = userId });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RoleSelect([FromForm] string role, [FromForm] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            RoleSelectionRequestDto roleSelectionRequestDto = new() { Role = role, RefreshToken = refreshToken };

            var result = await _mediator.Send(new CustomerRoleCommand { RoleSelectionRequestDto = roleSelectionRequestDto });

            if (result != null)
                return Ok(new { result });
            else
                return Unauthorized();
        }
    }
}
