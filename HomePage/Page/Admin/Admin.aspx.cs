using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Log
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ViewUtility.VLog.User userLog;        
        DataTable userTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["master"] == null || !bool.Parse(Session["master"].ToString()))
                Response.Redirect("~/Page/Post/Warning.aspx");

            if (!IsPostBack)
            {
                adminId.Text = Session["userId"].ToString();
                timeLabel.Text = DateTime.Now.ToString();
                dbUser uU = new dbUser();
                DataTable dt = uU.getAll();
                memberCount.Text = dt.Rows.Count.ToString();
                sessionCount.Text = Application["SessionC"].ToString();
            }

            renderData();                       
        }

        void renderData()
        {
            userLog = new ViewUtility.VLog.User();
            userTable = userLog.Fill();
            userLogGrid.DataSource = userTable;            
            userLogGrid.DataBind();
        }
    }
}