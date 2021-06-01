using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomePage
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check Login? visible loginoutBtn
            if (Session["userId"] == null) return;  
            
            loginBtn.Visible = false;
            logoutBtn.Visible = true;
            //contorl loginBtn
            userPro.Visible = true;
            userPro.Text = Session["userName"].ToString();
            if (Session["master"] != null && bool.Parse(Session["master"].ToString()))
                adminPage.Visible = true;                
        }
    }
}