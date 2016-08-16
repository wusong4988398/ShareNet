﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShareNet.Common.Mvc.ModelBinder
{
    /// <summary>
    /// ModelBinder从Cookie中取值
    /// </summary>
    public class CookieValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            HttpCookieCollection cookies = controllerContext.HttpContext.Request.Cookies;
            Dictionary<string, string> backingStore = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < cookies.Count; i++)
            {
                HttpCookie cookie = cookies[i];
                if (!String.IsNullOrEmpty(cookie.Name))
                {
                    backingStore[cookie.Name] = cookie.Value;
                }

            }
            return new DictionaryValueProvider<string>(backingStore, CultureInfo.InvariantCulture);
        }
    }
}
