using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.assets.css.login
{
    public partial class register : System.Web.UI.Page
    {
        dbUser dbUser;
        bool ableId;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbUser = new dbUser();
            if (IsPostBack)
            {
                ableId = bool.Parse(ViewState["ableId"].ToString());
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["ableId"] = ableId;
        }

        protected void idCheckBtn_Click(object sender, EventArgs e)
        {
            dbUser = new dbUser();
            string id = txtId.Text.Trim();
            char[] idArr = id.ToCharArray();
            bool kor = false;
            foreach(char i in idArr)
            {
                if(char.GetUnicodeCategory(i) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    kor = true;
                }
            }
            if (!dbUser.checkId(id) && id.Length <= 15 && !kor )
            {
                idCheckBtn.ForeColor = System.Drawing.Color.Green;
                idCheckBtn.BackColor = System.Drawing.Color.LightGreen;
                idCheckBtn.Text = "사용 가능";
                ableId = true;
            }
            else
            {
                if (kor || id.Length > 15)
                    alert.Text = "Id는 15자 이하, 한글 미포함입니다.";
                else
                    alert.Text = "중복된 ID입니다.";
                idCheckBtn.ForeColor = System.Drawing.Color.LightPink;
                idCheckBtn.BackColor = System.Drawing.Color.Red;
                idCheckBtn.Text = "사용 불가";
                ableId = false;                
            }
        }        

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string Id = txtId.Text.Trim();
            string email = txtEmail.Text.Trim();
            string pw = txtPassword.Text.Trim();
            string pwc = txtPwCheck.Text.Trim();
            string emailREGX = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
            if(!String.IsNullOrWhiteSpace(name) && name.Length <= 8)
            {
                if (!String.IsNullOrWhiteSpace(pw) && pw == pwc && pw.Length <= 15)
                {
                    if (Regex.IsMatch(email, emailREGX))
                    {
                        if (emailCheck.Checked)
                        {
                            if (male.Checked)
                            {
                                regUser(name, Id, pw, email, true);
                            }
                            else if (female.Checked)
                            {
                                regUser(name, Id, pw, email, false);
                            }
                            else
                            {
                                alert.Text = "성별을 체크해주세요";
                            }
                        }
                        else
                            alert.Text = "이메일 수신을 동의해주세요";
                    }
                    else
                        alert.Text = "유효한 이메일을 입력해주세요";
                }
                else
                    alert.Text = "유효한 비밀번호를 입력해주세요(15자 이하)";
            }
            else
                alert.Text = "유효한 이름을 입력해주세요(8자 이하)";                
        }

        void regUser(string name, string id, string pw, string em, bool gender)
        {            
            //유저 등록
            dbUser.regUser(new UserInfo
            {
                userName = name,
                userId = id,
                Password = pw,
                Certification = false,
                gender = gender,
                Email = em,
            });
            //session 등록
            Session["userId"] = id;
            Session["userName"] = name;
            Session["mailCertification"] = false;
            Session["master"] = false;
            Response.Redirect("~/Page/Login/code.aspx");
        }

    }
}