using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using ViewUtility;

namespace HomePage.Page.Login
{
    public partial class WebForm1 : System.Web.UI.Page
    {
             
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            dbUser dbUser = new dbUser();
            string id = txtId.Text.Trim();
            string pw = txtPassword.Text.Trim();
            //공백제거
            DataRow user = dbUser.login(id, pw);
            //db 접속
            if (user != null)
            {
                //user data가 있을 경우
                Session["userId"] = user["userId"];
                Session["master"] = user["class"];               
                Session["mailCertification"] = user["certification"];
                Session["userName"] = user["userName"];
                if (bool.Parse(Session["master"].ToString()))
                {
                    System.Diagnostics.Trace.WriteLine("관리자 로그인");
                }
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                loginFail.Text = "아이디 혹은 비밀번호가 올바르지 않습니다.";
            }
        }
    }
}