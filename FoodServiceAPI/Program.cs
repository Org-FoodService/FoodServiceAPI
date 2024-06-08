using Docker.DotNet.Models;
using Docker.DotNet;
using FoodServiceAPI.Config;
using FoodServiceAPI.Config.Ioc;
using FoodServiceAPI.Filters;
using FoodServiceAPI.Middleware;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Net.Http.Headers;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Start the docker container for serilog sink.
await ValidateDockerContainer();

// Get the database connection string from appsettings.json
string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add connection to Database
builder.Services.ConfigureDatabase(mySqlConnection);
builder.Services.UpdateMigrationDatabase();

builder.Services.ConfigureAuthentication(builder);

// Add IOC
builder.Services.ConfigureRepositoryIoc();
builder.Services.ConfigureServiceIoc();
builder.Services.ConfigureCommandIoc();

// Add Logger

builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration));

Log.Information("Starting up");

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AsyncExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodServiceAPI", Version = "v1" });

    // Adiciona a segurança do tipo Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Adiciona o esquema de segurança do tipo Bearer aos endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

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
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.MapControllers();

app.Run();


async Task ValidateDockerContainer()
{
    using (var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient())
    {
        // Check if any container is running
        var containers = await client.Containers.ListContainersAsync(new ContainersListParameters()
        {
            Limit = 1,
        });

        if (!containers.Any(d => d.Image == "datalust/seq"))
        {
            // If no containers are running, run the Docker command
            await RunDockerCommand();
        }
        else
        {
            Console.WriteLine("A docker container for serilog sink seq is already running.");
        }
    }
}

async Task RunDockerCommand()
{
    ProcessStartInfo startInfo = new ProcessStartInfo
    {
        FileName = "docker",
        Arguments = "run --rm -e ACCEPT_EULA=y -p 5341:80 datalust/seq",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using (Process process = new Process())
    {
        try
        {
            process.StartInfo = startInfo;
            
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            bool exited = await Task.Run(() => process.WaitForExit(30000)); // Timeout after 30 seconds

            if (!exited)
            {
                Console.WriteLine("Process to create docker container for serilog sink seq completed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while Docker container started for serilog sink seq with exception {ex.Message}");
        }
    }
}