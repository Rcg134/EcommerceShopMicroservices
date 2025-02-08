using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container. // add the carter service
builder.Services.AddCarter(configurator: c =>
{
    // Specify the assembly containing your modules
    var modulesAssembly = typeof(Program).Assembly;
    var modules = modulesAssembly.GetTypes()
        .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract)
        .ToArray();
    c.WithModules(modules);
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter(); // Scan all the ICarterModule in the project and map the necessary route

app.Run();
