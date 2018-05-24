using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JustBlog
{
    public static class Extensions
    {
        /// <summary>
        /// Изменяет время к времени указаному в WebConfig
        /// </summary>
        /// <param name="utcDT"></param>
        /// <returns></returns>
        public static string ToConfigLocalTime(this DateTime utcDT)
        {
            var istTZ = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return String.Format(
                "{0} ({1})",
                TimeZoneInfo.ConvertTimeFromUtc(utcDT, istTZ).ToShortDateString(),
                ConfigurationManager.AppSettings["TimezoneAbbr"]
                );
        }
    }
}