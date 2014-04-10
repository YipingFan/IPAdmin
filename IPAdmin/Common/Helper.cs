using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace IPAdmin.Common
{
    public static class Helper
    {
        public static string GetCurrentUserName()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.User != null)
                {
                    return HttpContext.Current.User.Identity.Name;
                }
            }

            return null;
        }
    }
}