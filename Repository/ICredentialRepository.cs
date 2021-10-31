using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public interface ICredentialRepository
    {
        public Dictionary<string, string> GetCredentials();
    }
}
