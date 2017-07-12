using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web.CustomClasses
{
    public class OfflineMessageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                filterContext.Result = new ContentResult { Content = string.Empty };
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Offline"
                };
                var response = filterContext.HttpContext.Response;
                response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                response.TrySkipIisCustomErrors = true;
            }
        }
    }
}