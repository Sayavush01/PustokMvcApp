namespace PustokMvcApp.Extensions
{
    public static class FileManager
    {
        public static string SaveFile(this IFormFile file, string folderPath, string rootPath)
        {
           
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(rootPath, folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName; // Return the filename to be saved in the database
        }
    }
}
