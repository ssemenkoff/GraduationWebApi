using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core_Server.Helpers {
    public class AuthenticationHelper
    {
        public const string ISSUER = "GraduationAuthServer";
        public const string AUDIENCE = "GraduationApp";
        const string KEY = "SomeUselessTextToEncode+321";
        public const int LIFETIME = 45;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}