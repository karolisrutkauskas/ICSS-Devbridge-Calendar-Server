using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Resources
{
    public static class EmailCredentials
    {
        public static string Email => "devbridgeapitest@gmail.com";
        public static string Password => ConfigurationManager.AppSettings["appSettings--emailPassword"];
    }
}