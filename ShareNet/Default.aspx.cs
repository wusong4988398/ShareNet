using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShareNet.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iisVersion = 6;
            int.TryParse(ConfigurationManager.AppSettings["IISVersion"], out iisVersion);
            if (iisVersion >= 7)
                Response.Redirect(Request.ApplicationPath);
            else
                Response.Redirect("~/Home.aspx");

        }

    }
}