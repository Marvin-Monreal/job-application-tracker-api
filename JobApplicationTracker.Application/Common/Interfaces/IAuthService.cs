using JobApplicationTracker.Application.Features.Authentication.Register;

namespace JobApplicationTracker.Application.Common.Interfaces
{
	public interface IAuthService
	{
		Task<RegisterResult> RegisterAsync(
			string email,
			string password,
			CancellationToken cancellationToken = default);
	}
}
