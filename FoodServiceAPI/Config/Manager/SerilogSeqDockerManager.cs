using Docker.DotNet.Models;
using Docker.DotNet;
using System.Diagnostics;

namespace FoodServiceAPI.Config.Manager
{
    public static class SerilogSeqDockerManager
    {
        private static readonly DockerClient client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();

        public static async Task ValidateDockerContainer()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while validating the Docker container: {ex.Message}");
            }
        }

        private static async Task RunDockerCommand()
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
    }
}
