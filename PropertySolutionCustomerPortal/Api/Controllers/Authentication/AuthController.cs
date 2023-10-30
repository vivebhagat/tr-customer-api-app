using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Application.Users.CustomerComponent.Command;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;

namespace PropertySolutionCustomerPortal.Api.Controllers.Authentication
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<BaseApplicationUser> _signInManager;
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, SignInManager<BaseApplicationUser> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }

        [HttpGet("[action]")]
        public IActionResult ExternalLogin(dynamic data)
        {
            var d = data;
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SendVerificationEmail(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("Invalid email verification request");

            bool result = await _mediator.Send(new SendVerificationEmailCommand { UserId = userId });

            if (!result)
                return BadRequest("Invalid email verification request");
            return Ok(result);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> EmailConfirmation([FromForm] string emailConfirmation)
        {
            EmailConfirmationCommand @object = JsonConvert.DeserializeObject<EmailConfirmationCommand>(emailConfirmation);

            if (string.IsNullOrWhiteSpace(@object.EmailConfirmation.Email) || string.IsNullOrWhiteSpace(@object.EmailConfirmation.Token))
                return BadRequest("Invalid email confirmation request");

            bool result = await _mediator.Send(new EmailConfirmationCommand { EmailConfirmation = @object.EmailConfirmation });

            if (!result)
                return BadRequest("Invalid email confirmation request");
            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("User not found. Please enter valid email address.");

            bool result = await _mediator.Send(new ForgotPasswordCommand { Email = email });

            if (!result)
                return BadRequest("User not found. Please enter valid email address.");

            return Ok(result);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromForm] string resetPassword)
        {
            ResetPasswordCommand @object = JsonConvert.DeserializeObject<ResetPasswordCommand>(resetPassword);

            if (string.IsNullOrWhiteSpace(@object.ResetPassword.Email) || string.IsNullOrWhiteSpace(@object.ResetPassword.Email) || string.IsNullOrWhiteSpace(@object.ResetPassword.Token))
                return BadRequest("Invalid passowrd reset request");

            bool result = await _mediator.Send(new ResetPasswordCommand { ResetPassword = @object.ResetPassword });

            if (!result)
                return BadRequest("Invalid passowrd reset request");
            return Ok(result);
        }
    }
}