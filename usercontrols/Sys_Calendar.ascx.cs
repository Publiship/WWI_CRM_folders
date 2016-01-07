using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_Sys_Calendar : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Calendar.SelectedDate = new DateTime(DateTime.Now.Year, 3, 14);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
