using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodServiceAPI.Shared.Util
{
    /// <summary>
    /// Utility class for handling image-related operations.
    /// </summary>
    public static class ImageUtils
    {
        /// <summary>
        /// Extracts the bytes of an image asynchronously from the provided form file.
        /// </summary>
        /// <param name="imageFile">The image file received from the form.</param>
        /// <returns>A task representing the asynchronous operation, returning the bytes of the image.</returns>
        public static async Task<byte[]> ExtractImageBytesAsync(IFormFile imageFile)
        {
            // Check if the image file is provided
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid or missing image file.");
            }

            using MemoryStream memoryStream = new();
            // Copy the bytes of the image file to a MemoryStream
            await imageFile.CopyToAsync(memoryStream);

            // Return the bytes of the image
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Reads the bytes of an image from the specified file path.
        /// </summary>
        /// <param name="imagePath">The path to the image file.</param>
        /// <returns>A byte array containing the image data.</returns>
        public static byte[]? GetImageBytes(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                Console.WriteLine("Image path is null or empty.");
                return null;
            }

            string fullPath = Path.Combine("wwwroot/images", imagePath);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Image file not found at path: {fullPath}");
                return null;
            }

            try
            {
                var result = File.ReadAllBytes(fullPath);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read image file at path: {fullPath}. Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the base64 string of an image from a specified path inside wwwroot/images.
        /// </summary>
        /// <param name="imagePath">The relative path to the image file.</param>
        /// <returns>A base64 string representing the image, or null if the image is not found or an error occurs.</returns>
        public static string? GetImageBase64(string imagePath)
        {
            // Ensure the image path is relative to wwwroot/images
            string fullPath = Path.Combine("wwwroot/images", imagePath);

            byte[]? imageBytes = GetImageBytes(fullPath);

            if (imageBytes == null)
            {
                return null;
            }

            // Convert image bytes to a base64 string
            return Convert.ToBase64String(imageBytes);
        }

        /// <summary>
        /// Saves an image file to the wwwroot/images directory with a specified name.
        /// </summary>
        /// <param name="imageFile">The image file to save.</param>
        /// <param name="fileName">The name to save the file as.</param>
        /// <returns>A task representing the asynchronous operation, returning a boolean indicating success or failure.</returns>
        public static async Task<bool> SaveImageAsync(IFormFile imageFile, string fileName)
        {
            // Check if the image file is provided
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid or missing image file.");
            }

            // Ensure the wwwroot/images directory exists
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Construct the full file path
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                // Save the image to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save image file at path: {filePath}. Error: {ex.Message}");
                return false;
            }
        }
    }
}
