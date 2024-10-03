var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "http://localhost:4200"));

app.MapControllers();
try
{
     using var scope = app.Services.CreateScope();
     var services = scope.ServiceProvider;
     var context = services.GetRequiredService<StoreContext>();
     await context.Database.MigrateAsync();
     await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
     Console.WriteLine(ex);
     throw;
}

app.Run();
