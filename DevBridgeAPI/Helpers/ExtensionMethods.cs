using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
                    .Select(x => '\'' + (x.Exception != null
                                       ? x.Exception.Message
                                       : x.ErrorMessage)
                                       .Replace("'", "\\'") + '\'');
        }

        public static int GetId (this IIdentity identity)
        {
            return int.Parse(((ClaimsIdentity)identity).FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            DayOfWeek fdow = CultureInfo.GetCultureInfo("lt-LT").DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate;
        }

        public static DateTime GetLastDayOfWeek(this DateTime date)
        {
            return date.GetFirstDayOfWeek().AddDays(6);
        }
    }
}