using FoodServiceAPI.Config;
using FoodServiceAPI.Config.Ioc;
using FoodServiceAPI.Filters;
using Serilog;
using FoodServiceAPI.Config.Manager;
<<<<<<< yg/develop-exclude-sensitive-log
using Destructurama;
=======
using FoodServiceAPI.Data.SqlServer.Config;
>>>>>>> develop

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

<<<<<<< yg/develop-exclude-sensitive-log
// Add Logger
builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Destructure.UsingAttributes());

Log.Information("Starting up");

=======
>>>>>>> develop
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

using (var scope = app.Services.CreateScope())
{
    await scope.AddAdminRole();
    await scope.AddEmployeeRole();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
