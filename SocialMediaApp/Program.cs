using Microsoft.EntityFrameworkCore;
using SocialMedia.API.Extensions;
using SocialMedia.Core;
using SocialMedia.Core.Services;
using SocialMedia.Data.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupSwagger();
builder.Services.AddDbContext<SocialMediaContext>(options =>
    options.UseSqlServer(@"Data Source=DESKTOP-RFQKAUE\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True;TrustServerCertificate=true;")
);
builder.Services.AddScoped<JwtTokenFunctions>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<PostInteractionService>();
builder.Services.SetupAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
