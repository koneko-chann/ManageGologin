using System.ComponentModel.DataAnnotations;

namespace ManageGologin.Attribute
{
    public class PathAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string path = value as string;

            if (path == null)
            {
                return ValidationResult.Success;
            }

            // Kiểm tra xem đường dẫn có hợp lệ hay không
            bool isValidPath = false;
            try
            {
                string fullPath = Path.GetFullPath(path);
                isValidPath = Path.IsPathRooted(fullPath);
            }
            catch
            {
                isValidPath = false;
            }

            return isValidPath ? ValidationResult.Success : new ValidationResult("Not valid path");
        }
    }

}
