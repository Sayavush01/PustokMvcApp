using Microsoft.AspNetCore.Identity;

namespace MVC_WEB_APP
{
    public class CustomIdentityErrorDescriber: IdentityErrorDescriber
    {
      public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Password must contain at least one non-alphanumeric character."
            };
        }
    }
}
