var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

var app = builder.Build();


// Create endpoint for hello world
app.MapGet("/", () => "Hello World!");


// Configure the HTTP request pipeline.

app.Run();
