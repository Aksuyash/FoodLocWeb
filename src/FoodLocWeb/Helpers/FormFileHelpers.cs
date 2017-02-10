
using System;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace FoodLoc.Helpers
{
    public static class FormFileHelpers
    {
        private static int DefaultBufferSize = 80 * 1024;
        /// <summary>
        /// Asynchronously saves the contents of an uploaded file.
        /// </summary>
        /// <param name="formFile">The <see cref="IFormFile"/>.</param>
        /// <param name="filename">The name of the file to create.</param>
        public static string Save(
            IFormFile formFile,
           string rootpath,
            CancellationToken cancellationToken = default(CancellationToken))
        {


            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition);
            string FilePath = parsedContentDisposition.FileName.Trim('"');

            string FileExtension = Path.GetExtension(FilePath);
            FilePath = Guid.NewGuid().ToString() + "." + FileExtension;
            var uploadDir = rootpath;
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            var imageUrl = uploadDir + FilePath; //+ FileExtension;

            using (var stream = new FileStream(imageUrl, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return FilePath;
        }

        public static bool IsImage(IFormFile file)
        {
            //Checks for image type... you could also do filename extension checks and other things
            return ((file != null) && System.Text.RegularExpressions.Regex.IsMatch(file.ContentType, "image/\\S+") && (file.Length > 0));
        }
    }
}
