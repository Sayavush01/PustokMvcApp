using MVC_WEB_APP.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace MVC_WEB_APP.ViewModels.UsersVm
{
    public class UserProfileVm
    {
        public UserProfileInfoVm UserInfo { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class UserProfileInfoVm
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
