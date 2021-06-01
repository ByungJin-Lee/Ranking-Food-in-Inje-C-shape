using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomePage.Page.Post
{
    public partial class warning : System.Web.UI.Page
    {
        ViewUtility.VLog.User uLog;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = "NULL";
                if (Session["userId"] != null)
                    userId = Session["userId"].ToString();
                uLog = new ViewUtility.VLog.User();
                uLog.commitLog(new ViewUtility.VLog.User.LogBlock
                {
                    userID = userId,
                    connection = true,
                    postKind = "Warning",
                    postItem = "",
                });
            }
        }
    }
}