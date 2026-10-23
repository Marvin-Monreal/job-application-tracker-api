using FluentValidation;
using JobApplicationTracker.Api.Contracts.Authentication;
using JobApplicationTracker.Api.Contracts.Common;
using JobApplicationTracker.Application.Features.Authentication.Register;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobApplicationTracker.Api.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly RegisterCommandHandler _registerHandler;

		public AuthController(RegisterCommandHandler registerHandler)
		{
			_registerHandler = registerHandler;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
		{
			var command = new RegisterCommand
			{
				Email = request.Email,
				Password = request.Password,
				ConfirmPassword = request.ConfirmPassword
			};

			try
			{
				var result = await _registerHandler.HandleAsync(
					command,
					cancellationToken);

				var message = result.RequiresEmailConfirmation
					? "Registration successful. Please check your email to confirm your account."
					: "Registration successful.";

				return Ok(ApiResponse<RegisterResult>.Ok(result, message));
			}
			catch (ValidationException ex)
			{
				var errors = ex.Errors
					.Select(error => error.ErrorMessage)
					.Distinct();

				return BadRequest(
					ApiResponse<RegisterResult>.Fail(
						"Validation failed.",
						errors));
			}
			catch (HttpRequestException ex)
			{
				if (ex.StatusCode is HttpStatusCode.BadRequest
					or HttpStatusCode.UnprocessableEntity)
				{
					return BadRequest(
						ApiResponse<RegisterResult>.Fail(
							"Registration could not be completed. Please check your details."));
				}

				return StatusCode(
					StatusCodes.Status502BadGateway,
					ApiResponse<RegisterResult>.Fail(
						"The registration service is temporarily unavailable."));
			}
		}
	}
}
