using System.ComponentModel.DataAnnotations;

namespace MVC_WEB_APP.ViewModels.UsersVm
{
    public class LoginVm
    {
        [Required]
        public string UsernameorEmail { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
