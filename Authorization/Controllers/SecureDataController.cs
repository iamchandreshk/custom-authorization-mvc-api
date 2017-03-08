using Authorization.Filters;
using System.Web.Http;

namespace Authorization.Controllers
{
    //[Authorize]
    [CustomAuthorize]
    [Route("secure_data")]
    public class SecureDataController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get()
        {
            var user = System.Web.HttpContext.Current.User;
            return Ok(new { secureData = "Hi!, " + user.Identity.Name });
        }
    }
}