using Microsoft.AspNetCore.Http;

namespace FoodServiceAPI.Core.Util
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
    }
}