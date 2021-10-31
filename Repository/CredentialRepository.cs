using AuthorizationMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public class CredentialRepository:ICredentialRepository
    {
        private Dictionary<string, string> ValidUsersDictionary = new Dictionary<string, string>()
        {
               {"10001","cts#1"},
               {"10002","cts#2"},
               {"10003","cts#3"},
               {"10004","cts#4"},
               {"10005","cts#5" }
        };
        public Dictionary<string, string> GetCredentials()
        {
            return ValidUsersDictionary;
        }
    }
}
