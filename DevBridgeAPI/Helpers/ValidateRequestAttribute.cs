using DevBridgeAPI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DevBridgeAPI.Helpers
{
    public class ValidateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsValue(null))
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest, Errors.EmptyRequestBody());
            }

            if (!actionContext.ModelState.IsValid)
            {
                var errors = actionContext.ModelState.GetErrorsAndExceptions()
                    .Select(x => Errors.InvalidModelState(x));
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest, errors);
            }
        }
    }
}