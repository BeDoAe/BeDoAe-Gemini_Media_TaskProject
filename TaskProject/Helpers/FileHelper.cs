namespace TaskProject.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/Files/" + uniqueFileName;
        }

    }
}
