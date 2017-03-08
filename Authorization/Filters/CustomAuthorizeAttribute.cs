using System;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace Authorization.Filters
{
    public class CustomAuthorizeAttribute : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            string credentials = encoding.GetString
                (Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));

            var credentialsArray = credentials.Split(':');
            var username = credentialsArray[0];
            var password = credentialsArray[1];
            if (username == "test" && password == "test" && actionContext.Request.Headers.Authorization.Scheme == "Basic")
            {
                var identity = new GenericIdentity(username);
                SetPrincipal(new GenericPrincipal(identity, null));
                base.OnAuthorization(actionContext);
            }
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    return true;// if my current user is authorised
        //}
    }
}