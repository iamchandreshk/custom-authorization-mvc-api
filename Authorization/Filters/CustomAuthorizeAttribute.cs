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
            if (Authorise(actionContext))
            {
                base.OnAuthorization(actionContext);
            }
        }

        private static bool Authorise(HttpActionContext credentials)
        {
            var credential = Encoding.GetEncoding("iso-8859-1").GetString
               (Convert.FromBase64String(credentials.Request.Headers.Authorization.Parameter));

            var credentialsArray = credential.Split('$');
            var username = credentialsArray[0];
            var password = credentialsArray[1];
            var datetime = Convert.ToDateTime(credentialsArray[2]);
            var result = datetime > DateTime.UtcNow.AddMinutes(-5);

            if (username == "test" && password == "test" && result && credentials.Request.Headers.Authorization.Scheme == "Basic")
            {
                var identity = new GenericIdentity(username);
                SetPrincipal(new GenericPrincipal(identity, null));
                return true;
            }
            return false;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}