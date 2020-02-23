using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Contoso.Apps.SportsLeague.Admin.Controllers
{
    public class BaseController : Controller
    {
        public string DisplayName { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    string displayNameClaim = "http://schemas.microsoft.com/identity/claims/displayname";
                    var claim = claimsIdentity.FindFirst(displayNameClaim);
                    if (claim != null)
                    {
                        DisplayName = claim.Value;
                    }
                }
                //DisplayName = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst("http://schemas.microsoft.com/identity/claims/displayname").Value;
            }

            base.OnActionExecuted(context);
        }
    }
}