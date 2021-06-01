using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;
using System.Drawing;

namespace HomePage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        VLog.User uLog;        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userId"] != null && !IsPostBack)
            {
                uLog = new VLog.User();
                uLog.commitLog(new VLog.User.LogBlock
                {
                    userID = Session["userId"].ToString(),
                    connection = true,
                    postKind = "Home",
                    postItem = "",
                });
            }            
        }          
    }
}