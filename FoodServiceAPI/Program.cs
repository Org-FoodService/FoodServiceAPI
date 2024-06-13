using Destructurama;
using FoodServiceAPI.Data.SqlServer.Config;
using FoodServiceAPI.Config;
using FoodServiceAPI.Config.Ioc;
using FoodServiceAPI.Config.Manager;
using FoodServiceAPI.Filters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Start the docker container for serilog sink.
await SerilogSeqDockerManager.ValidateDockerContainer();

// Get the database connection string from appsettings.json
string? sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add connection to Database
builder.Services.ConfigureDatabase(sqlConnection!);
builder.Services.UpdateMigrationDatabase();

builder.Services.ConfigureAuthentication(builder);

// Add IOC
builder.Services.ConfigureRepositoryIoc();
builder.Services.ConfigureServiceIoc();
builder.Services.ConfigureCommandIoc();
builder.Services.ConfigureWrapperIoc();

// Add HealthCheck
builder.Services.AddHealthChecks();

// Add Logger
builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Destructure.UsingAttributes());

Log.Information("Starting up");

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AsyncExceptionFilter>();
    options.Filters.Add<RequestResponseLogFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Add Logger
builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration));

Log.Information("Starting up");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    await scope.AddAdminRole();
    await scope.AddEmployeeRole();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapGet("/", async context =>
    {
        var data = new { message = "Welcome to the FoodService API" };
        await context.Response.WriteAsJsonAsync(data);
    });

    _ = endpoints.MapHealthChecks("/health");
});

app.Run();
