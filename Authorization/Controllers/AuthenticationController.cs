using Authorization.Models;
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
            return Ok(new { success = true });
        }
    }
}
