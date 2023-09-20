using System.Text.RegularExpressions;
using UserService.Exceptions;

namespace UserService.Helpers
{
    public class PasswordValidator
    {
        public static void CheckPasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                throw new InternalException("Password should contains of 8 chars at least");
            }

            if (!(Regex.IsMatch(password, "[a-z, A-Z, 0-9, !,@,#,$,%,^,&,*,(,),_,=,+,{,}]")))
            {
                throw new InternalException("Password should be Alphanumeric with special chars");
            }
        }
    }
}
