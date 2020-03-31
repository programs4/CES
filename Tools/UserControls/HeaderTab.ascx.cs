using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_UserControls_HeaderTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HrpTab1.NavigateUrl = "/tools/sib-r/?i=" + Config._GetQueryString("i");
        HrpTab2.NavigateUrl = "/tools/evaluations/?i=" + Config._GetQueryString("i");
        HrpTab3.NavigateUrl = "/tools/evaluationsskill/?i=" + Config._GetQueryString("i");
    }
}