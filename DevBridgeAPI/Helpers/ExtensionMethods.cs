using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace DevBridgeAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<string> GetErrorsAndExceptions(this ModelStateDictionary modelState)
        {
            return modelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => '`' + (x.Exception != null
                                      ? x.Exception.Message
                                      : x.ErrorMessage)
                                      .Replace("`", "\\`") + '`');
        }
    }
}