using FlashcardsAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

//cors1
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//cors2
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
        });
});

builder.Services.AddDbContext<FlashcardsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Host=localhost;Database=Flashcards;Username=milena-codecool;Password=huehuehue"));
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});

builder.Services.AddSingleton<DataService>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//cors3
app.UseCors(MyAllowSpecificOrigins);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
