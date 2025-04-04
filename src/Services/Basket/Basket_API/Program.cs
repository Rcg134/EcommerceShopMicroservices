using Discount.Grpc;

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

//Add Marten
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();


// Add MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Add Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();



builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
    //options.InstanceName = "Basket_";
});

//GRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});


// if multiple cache is needed
//builder.Services.AddScoped<Func<string, ICustomCacheService>>(serviceProvider => key =>
//{
//    return key switch
//    {
//        "Redis" => serviceProvider.GetRequiredService<RedisCacheService>(),
//        "InMemory" => serviceProvider.GetRequiredService<InMemoryCacheService>(),
//        _ => throw new KeyNotFoundException($"Cache service with key '{key}' is not registered.")
//    };
//});

//---------Cross Cutting services
//Health Checks
builder.Services.AddHealthChecks()
     .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
     .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

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
