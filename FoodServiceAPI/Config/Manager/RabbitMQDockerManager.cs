using Docker.DotNet.Models;
using Docker.DotNet;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System;

namespace FoodServiceAPI.Config.Manager
{
    public static class RabbitMQDockerManager
    {
        private static readonly DockerClient client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();

        public static async Task ValidateDockerContainer()
        {
            try
            {
                // Check if any RabbitMQ container is already running
                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters()
                {
                    All = true,
                });

                if (!containers.Any(d => d.Image == "rabbitmq:management" && d.State == "running"))
                {
                    // If no containers are running, execute the Docker command
                    await RunDockerCommand();
                }
                else
                {
                    Console.WriteLine("A Docker container for RabbitMQ is already running.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while validating the Docker container: {ex.Message}");
            }
        }

        private static async Task RunDockerCommand()
        {
            Console.WriteLine("Starting process to create Docker container for RabbitMQ.");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = "run --rm -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management",
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

                    if (exited)
                    {
                        Console.WriteLine("Docker container for RabbitMQ started successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Process to create Docker container for RabbitMQ timed out.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting Docker container for RabbitMQ: {ex.Message}");
                }
            }
        }
    }
}
