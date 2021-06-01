using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Login
{
    public partial class code : System.Web.UI.Page
    {
        dbUser dbUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
                Response.Redirect("~/Page/Login/login.aspx");
                //로그인이 안되었을 경우 로그인 화면으로
            dbUser = new dbUser();            
        }

        protected void findBtn_Click(object sender, EventArgs e)
        {
            string inputCode = txtCode.Text.Trim();
            if (String.IsNullOrEmpty(inputCode))
            {
                //입력창이 비었을 경우
                alert.ForeColor = System.Drawing.Color.Red;
                alert.Text = "인증 코드를 입력해주세요.";
                return;
            }
            if (dbUser.checkCode(Session["userId"].ToString(), inputCode))
            {
                //이메일 인증 성공
                alert.ForeColor = System.Drawing.Color.Green;
                Session["mailCertification"] = true;
                alert.Text = "이메일이 인증되었습니다.";
            }
            else
            {
                //이메일 인증 실패 
                alert.ForeColor = System.Drawing.Color.Red;
                alert.Text = "유효한 인증 코드가 아닙니다.";
            }
        }
    }
}