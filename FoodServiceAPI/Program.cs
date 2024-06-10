using FoodServiceAPI.Config;
using FoodServiceAPI.Config.Ioc;
using FoodServiceAPI.Filters;
using Serilog;
using FoodServiceAPI.Config.Manager;
using Destructurama;

var builder = WebApplication.CreateBuilder(args);

// Start the docker container for serilog sink.
await SerilogSeqDockerManager.ValidateDockerContainer();

// Get the database connection string from appsettings.json
string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add connection to Database
builder.Services.ConfigureDatabase(mySqlConnection!);
builder.Services.UpdateMigrationDatabase();

builder.Services.ConfigureAuthentication(builder);

// Add IOC
builder.Services.ConfigureRepositoryIoc();
builder.Services.ConfigureServiceIoc();
builder.Services.ConfigureCommandIoc();

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    await scope.AddAdminRole();
    await scope.AddEmployeeRole();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
