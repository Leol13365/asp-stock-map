using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class SessionAuth : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        if (HttpContext.Current.Session["Account"] == null)
        {
            this.HandleUnauthorizedRequest(filterContext);
        }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        base.HandleUnauthorizedRequest(filterContext);
        filterContext.Result = new RedirectResult("~/Auth/Login");
    }
}