using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // Token issuer
        public const string AUDIENCE = "MyAuthClient"; // Token audience
        const string KEY = "mysupersecret_secretkey!123";   // Encryption key
        public const int LIFETIME = 5; // Token lifetime in minutes 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
