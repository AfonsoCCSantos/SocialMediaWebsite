using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Authorization;
using SocialMedia.Data.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SocialMediaContext>(options =>
    options.UseSqlServer(@"Data Source=DESKTOP-RFQKAUE\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True;TrustServerCertificate=true;")
);
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddSingleton<JwtTokenCreator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
