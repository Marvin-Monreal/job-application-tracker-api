using JobApplicationTracker.Application.Common.Interfaces;
using JobApplicationTracker.Application.Features.Authentication.Register;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace JobApplicationTracker.Infrastructure.Authentication
{
	public class SupabaseAuthService : IAuthService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public SupabaseAuthService(
			HttpClient httpClient,
			IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}

		public async Task<RegisterResult> RegisterAsync(
			string email,
			string password,
			CancellationToken cancellationToken = default)
		{
			var supabaseUrl = _configuration["Supabase:Url"];
			var publishableKey =
				_configuration["Supabase:PublishableKey"];

			if (string.IsNullOrWhiteSpace(supabaseUrl) ||
				string.IsNullOrWhiteSpace(publishableKey))
			{
				throw new InvalidOperationException(
					"Supabase configuration is missing.");
			}

			using var request = new HttpRequestMessage(
				HttpMethod.Post,
				$"{supabaseUrl.TrimEnd('/')}/auth/v1/signup");

			request.Headers.Add("apikey", publishableKey);

			request.Content = JsonContent.Create(new
			{
				email,
				password
			});

			using var response = await _httpClient.SendAsync(
				request,
				cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				// Avoid exposing provider details or credentials to the client.
				throw new HttpRequestException(
					"Supabase registration failed.",
					null,
					response.StatusCode);
			}

			var signup = await response.Content
				.ReadFromJsonAsync<SupabaseSignupResponse>(
					cancellationToken: cancellationToken);

			if (signup is null ||
				string.IsNullOrWhiteSpace(signup.Id))
			{
				throw new InvalidOperationException(
					"Supabase returned an unexpected registration response.");
			}

			return new RegisterResult
			{
				UserId = signup.Id,
				Email = signup.Email ?? email,
				RequiresEmailConfirmation =
					string.IsNullOrWhiteSpace(signup.AccessToken)
			};
		}

		private sealed class SupabaseSignupResponse
		{
			[JsonPropertyName("id")]
			public string Id { get; set; } = string.Empty;

			[JsonPropertyName("email")]
			public string? Email { get; set; }

			[JsonPropertyName("access_token")]
			public string? AccessToken { get; set; }
		}
	}
}
