var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container. // Add the carter service
builder.Services.AddCarter(configurator: c =>
{
    // Specify the assembly containing your modules
    var modulesAssembly = assembly;
    var modules = modulesAssembly.GetTypes()
        .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsAbstract)
        .ToArray();
    c.WithModules(modules);
});

//Fluent Validator
builder.Services.AddValidatorsFromAssembly(assembly);

// Add Marten
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

// Add Marten Initial Data seeding if development
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}


// Add MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Add Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Health Checks
builder.Services.AddHealthChecks()
     .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapCarter(); // Scan all the ICarterModule in the project and map the necessary route

// use exception handler after register
app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
