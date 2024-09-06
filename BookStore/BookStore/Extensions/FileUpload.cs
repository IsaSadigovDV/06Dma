using BookStore.Models;

namespace BookStore.Extensions
{
    public static class FileUpload
    {
        public static async Task<string> SaveFileAsync(this IFormFile file,string root, string path)
        {
            string filename=Guid.NewGuid().ToString() + file.FileName;

            var fullpath =   Path.Combine(root, path, filename);


            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }
    }
}
