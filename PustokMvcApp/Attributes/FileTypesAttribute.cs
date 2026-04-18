using System.ComponentModel.DataAnnotations;

namespace PustokMvcApp.Attributes
{
    public class FileTypesAttribute: ValidationAttribute
    {
        public string[] Types { get; set; }
        public FileTypesAttribute(string types)
        {
            Types = types.Split(',').Select(t => t.Trim().ToLower()).ToArray();
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            List<IFormFile> f = new List<IFormFile>();
            if (value is List<IFormFile> files)
            {
                f = files;
            }
            else if (value is IFormFile file)
            {
                f.Add(file);
            }
            foreach (var item in f)
            {
                var ext = Path.GetExtension(item.FileName).ToLower();
                if (!Types.Contains(ext))
                {
                    return new ValidationResult($"Only the following file types are allowed: {string.Join(", ", Types)}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
