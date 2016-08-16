using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using ShareNet.Infrastructure.Utilities;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Utilities
{
    public class HtmlUtility
    {
        public static string CleanHtml(string rawHtml, TrustedHtmlLevel level)
{
    if (string.IsNullOrEmpty(rawHtml))
    {
        return rawHtml;
    }
    HtmlDocument document = new HtmlDocument {
        OptionAutoCloseOnEnd = true,
        OptionWriteEmptyNodes = true
    };
    TrustedHtml trustedHtml = DIContainer.Resolve<TrustedHtml>();
    switch (level)
    {
        case TrustedHtmlLevel.Basic:
            trustedHtml = trustedHtml.Basic();
            break;

        case TrustedHtmlLevel.HtmlEditor:
            trustedHtml = trustedHtml.HtmlEditor();
            break;
    }
    document.LoadHtml(rawHtml);
    HtmlNodeCollection source = document.DocumentNode.SelectNodes("//*");
    if (source != null)
    {
       
        Dictionary<string, string> enforcedAttributes;
        string host = string.Empty;
        if (HttpContext.Current != null)
        {
            host = WebUtility.HostPath(HttpContext.Current.Request.Url);
        }
        source.ToList<HtmlNode>().ForEach(delegate (HtmlNode n) {
            Action<HtmlAttribute> action = null;
          
            if (trustedHtml.IsSafeTag(n.Name))
            {
                if (action == null)
                {
                    action = delegate (HtmlAttribute attr) {
                        if (!trustedHtml.IsSafeAttribute(n.Name, attr.Name, attr.Value))
                        {
                            attr.Remove();
                        }
                        else if (attr.Value.StartsWith("javascirpt:", StringComparison.InvariantCultureIgnoreCase))
                        {
                            attr.Value = "javascirpt:;";
                        }
                    };
                }
                n.Attributes.ToList<HtmlAttribute>().ForEach(action);
                enforcedAttributes = trustedHtml.GetEnforcedAttributes(n.Name);
                if (enforcedAttributes != null)
                {
                    foreach (KeyValuePair<string, string> pair in enforcedAttributes)
                    {
                        if (!(from a in n.Attributes select a.Name).Contains<string>(pair.Key))
                        {
                            n.Attributes.Add(pair.Key, pair.Value);
                        }
                        else
                        {
                            n.Attributes[pair.Key].Value = pair.Value;
                        }
                    }
                }
                if ((n.Name == "a") && n.Attributes.Contains("href"))
                {
                    string str = n.Attributes["href"].Value;
                    if (str.StartsWith("http://") && !str.ToLowerInvariant().StartsWith(host.ToLower()))
                    {
                        if (!(from a in n.Attributes select a.Name).Contains<string>("rel"))
                        {
                            n.Attributes.Add("rel", "nofollow");
                        }
                        else if (n.Attributes["rel"].Value != "fancybox")
                        {
                            n.Attributes["rel"].Value = "nofollow";
                        }
                    }
                }
            }
            else if (trustedHtml.EncodeHtml)
            {
                n.HtmlEncode = true;
            }
            else
            {
                n.RemoveTag();
            }
        });
    }
    return document.DocumentNode.WriteTo();
}





    public static string StripHtml(string rawString, bool removeHtmlEntities, bool enableMultiLine)
        {
            string input = rawString;
            if (enableMultiLine)
            {
                input = Regex.Replace(Regex.Replace(input, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase), @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            input = input.Replace("\"", "''");
            if (removeHtmlEntities)
            {
                input = Regex.Replace(input, "&[^;]*;", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            return Regex.Replace(input, "<[^>]+>", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

 


 

    }
}
