using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Apps.HTML
{
    public partial class TestWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTextMail_Click(object sender, EventArgs e)
        {
            Common.CommonUtility Utility = new Common.CommonUtility();
            App.Base.Common.CommonMailManager.SendMailManager(Utility);
        }
    }
}