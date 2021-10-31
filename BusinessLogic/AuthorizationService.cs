using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Services
{
    public class AuthorizationService:IAuthorizationService
    {
        private readonly ICredentialRepository obj;
        public AuthorizationService(ICredentialRepository _obj)
        {
            obj = _obj;
        }

        public string GenerateJSONWebToken(Credentials userInfo, IConfiguration _config)
        {
            if (userInfo == null)
                return null;
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public dynamic AuthenticateUser(Credentials login)
        {
            if (login == null)
            {
                return null;
            }
            try
            {
                Credentials user = null;

                Dictionary<string, string> ValidUsersDictionary = obj.GetCredentials();

                if (ValidUsersDictionary == null)
                    return null;
                else
                {
                    if (ValidUsersDictionary.Any(u => u.Key == login.LoginId && u.Value == login.Password))
                    {
                        user = new Credentials {LoginId= login.LoginId, Password = login.Password };

                    }
                }

                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
