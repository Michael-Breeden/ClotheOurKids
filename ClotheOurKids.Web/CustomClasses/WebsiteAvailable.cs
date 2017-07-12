using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ClotheOurKids.Web.CustomClasses
{
    public class WebsiteAvailable
    {
        private static readonly Lazy<bool> _isOffline = new Lazy<bool>(() =>
        {
            bool result;
            return bool.TryParse(
                ConfigurationManager.AppSettings["available.offline"], out result) && result;
        });

        private static readonly Lazy<string> _message = new Lazy<string>(() => ConfigurationManager.AppSettings["available.message"]);

        public static bool IsOffline
        {
            get { return _isOffline.Value; }
        }

        public static bool IsOnline
        {
            get { return !_isOffline.Value; }
        }

        public static string Message
        {
            get { return _message.Value; }
        }

        public static bool HasMessage
        {
            get { return !string.IsNullOrWhiteSpace(Message); }
        }
    }
}