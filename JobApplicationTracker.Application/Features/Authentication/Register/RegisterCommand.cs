namespace JobApplicationTracker.Application.Features.Authentication.Register
{
	public class RegisterCommand
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
	}
}
