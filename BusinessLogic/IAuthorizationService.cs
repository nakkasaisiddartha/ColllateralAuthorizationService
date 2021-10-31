using AuthorizationMicroservice.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Services
{
    public interface IAuthorizationService
    {
        public string GenerateJSONWebToken(Credentials userInfo, IConfiguration _config);
        public dynamic AuthenticateUser(Credentials login);
    }
}
