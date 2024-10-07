using Microsoft.AspNetCore.Http;


namespace Api006.Service.Extensions
{
    public static class FileUploadExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            if(file == null)
            {
                return false;
            }
            return file.ContentType.Contains("image");
        }

        public static bool IsSizeOk(this IFormFile file, int mb)
        {
            if (file == null)
            {
                return false;
            }
            return file.Length / 1024 / 1024 <= mb;
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string root, string path)
        {
            if (file == null)
            {
                return "File can not be null";
            }
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string fullpath = Path.Combine(root, path, filename);

            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }
    }
}
