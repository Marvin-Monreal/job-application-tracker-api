namespace JobApplicationTracker.Api.Contracts.Common
{
	public class ApiResponse<T>
	{
		public bool Success { get; private set; }

		public string Message { get; private set; } = string.Empty;

		public T? Data { get; private set; }

		public List<string> Errors { get; private set; } = [];

		public static ApiResponse<T> Ok(
			T? data,
			string message = "Request successful.")
		{
			return new ApiResponse<T>
			{
				Success = true,
				Message = message,
				Data = data
			};
		}

		public static ApiResponse<T> Fail(
			string message,
			IEnumerable<string>? errors = null)
		{
			var response = new ApiResponse<T>
			{
				Success = false,
				Message = message
			};

			if (errors is not null)
			{
				response.Errors.AddRange(errors);
			}

			return response;
		}

		public ApiResponse<T> AddError(string error)
		{
			if (!string.IsNullOrWhiteSpace(error))
			{
				Errors.Add(error);
			}

			Success = false;
			Data = default;

			return this;
		}
	}
}
