using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Login
{
    public partial class Id : System.Web.UI.Page
    {
        dbUser dbUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbUser = new dbUser();
        }

        protected void findBtn_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if(name == null || email == null)
            {
                alert.ForeColor = System.Drawing.Color.Red;
                alert.Text = "이름 혹은 이메일을 입력해주세요.";
                return;
            }
            if(dbUser.findData(name, email, false))
            {
                alert.ForeColor = System.Drawing.Color.Green;
                alert.Text = "등록된 이메일로 아이디가 발송되었습니다";
            }
            else
            {
                alert.ForeColor = System.Drawing.Color.Red;
                alert.Text = "유효한 이름 혹은 이메일이 아닙니다.";
            }
        }
    }
}