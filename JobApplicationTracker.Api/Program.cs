using FluentValidation;
using JobApplicationTracker.Application.Common.Interfaces;
using JobApplicationTracker.Application.Features.Authentication.Register;
using JobApplicationTracker.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Register controllers.
builder.Services.AddControllers();

// Register Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register FluentValidation.
builder.Services.AddValidatorsFromAssemblyContaining<
	RegisterCommandValidator>();

// Register application services.
builder.Services.AddScoped<RegisterCommandHandler>();

// Register Supabase authentication service.
builder.Services.AddHttpClient<IAuthService, SupabaseAuthService>();

var app = builder.Build();

// Enable Swagger in development.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
