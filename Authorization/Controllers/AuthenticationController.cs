using Authorization.Models;
using System;
using System.Web.Http;

namespace Authorization.Controllers
{
    public class AuthenticationController : ApiController
    {
        [Route("authenticate")]
        public IHttpActionResult Authenticate(AuthenticateViewModel viewModel)
        {
            if (!(viewModel.Username == "test" && viewModel.Password == "test"))
            {
                return Ok(new { success = false, message = "User code or password is incorrect" });
            }

            var loginTime = DateTime.UtcNow;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(viewModel.Username + "$" + viewModel.Password + "$" + loginTime);
            return Ok(new
            {
                success = true,
                Token = Convert.ToBase64String(plainTextBytes),
                Expiration = loginTime.AddMinutes(5),
                IssuedAt = loginTime
            });
        }
    }
}
