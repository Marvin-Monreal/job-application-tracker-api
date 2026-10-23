namespace JobApplicationTracker.Application.Features.Authentication.Register
{
	public class RegisterResult
	{
		public string UserId { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public bool RequiresEmailConfirmation { get; set; }
	}
}
