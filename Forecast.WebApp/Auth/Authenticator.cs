using System;
using System.Threading.Tasks;
using AspNetCore.Security.Jwt;

namespace Forecast.WebApp.Auth
{
    public class Authenticator : IAuthentication
    {        
        public async Task<bool> IsValidUser(string id, string password)
        {
            throw new NotImplementedException();
        }
    }
}