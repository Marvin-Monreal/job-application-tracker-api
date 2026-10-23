using FluentValidation;
using JobApplicationTracker.Application.Common.Interfaces;

namespace JobApplicationTracker.Application.Features.Authentication.Register
{
	public class RegisterCommandHandler
	{
		private readonly IValidator<RegisterCommand> _validator;
		private readonly IAuthService _authService;

		public RegisterCommandHandler(
			IValidator<RegisterCommand> validator,
			IAuthService authService)
		{
			_validator = validator;
			_authService = authService;
		}

		public async Task<RegisterResult> HandleAsync(
			RegisterCommand command,
			CancellationToken cancellationToken = default)
		{
			await _validator.ValidateAndThrowAsync(
				command,
				cancellationToken);

			return await _authService.RegisterAsync(
				command.Email,
				command.Password,
				cancellationToken);
		}
	}
}
