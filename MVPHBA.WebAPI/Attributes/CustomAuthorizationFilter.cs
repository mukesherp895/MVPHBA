using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVPHBA.Common;
using MVPHBA.Model.ViewModels;


namespace MVPHBA.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                ResponseVM resp = new ResponseVM
                {
                    Message = "Session Expired",
                    Status = APIRespCodes.Fail
                };
                context.Result = new JsonResult(resp) { StatusCode = StatusCodes.Status200OK };
            }
        }
    }
}
