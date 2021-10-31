using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TokenController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        private IConfiguration config;
        private readonly IAuthorizationService app;
        public TokenController(IConfiguration config, IAuthorizationService app)
        {
            this.config = config;
            this.app = app;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Credentials login)
        {
            _log4net.Info(" Http Post request");
            if (login == null)
            {
                return BadRequest();
            }
            try
            {
                IActionResult response = Unauthorized();
                Credentials user = app.AuthenticateUser(login);

                if (user != null)
                {
                    var tokenString = app.GenerateJSONWebToken(user, config);
                    var token = new Token() { JWTToken = tokenString };
                    return Ok(token);
                }

                return response;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured " + e.Message);
                return StatusCode(500);
            }

        }
    }
}
