using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Post
{
    public partial class Food_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
                Response.Redirect("~/Page/Login/login.aspx");
            //로그인 확인
            if (!bool.Parse(Session["mailCertification"].ToString()))
                Response.Redirect("~/Page/Login/code.aspx");
            //이메일 인증 확인
            if (!bool.Parse(Session["master"].ToString()))
                Response.Redirect("~/Page/Post/Warning.aspx");

            try
            {
                int id = int.Parse(Request.QueryString["food"]);                
                renderEdit(id);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("error");
            }

        }

        protected void renderEdit(int id)
        {
            dbComment comment = new dbComment();
            DataTable dt = comment.get(id);
            RComment Rcom = new RComment();
            Rcom.onDelete += Rcom_onDelete;            
            Rcom.renderEdit(commentTable, dt);
        }

        private void Rcom_onDelete(object sender, EventArgs e)
        {            
            HtmlGenericControl parent = ((sender as Button).Parent as HtmlGenericControl).Parent as HtmlGenericControl;
            if (parent == null) return;
            //테이블이 비었을 경우
            int foodId = int.Parse(parent.Attributes["data-foodid"].ToString());
            if (foodId > 0)
            {
                dbComment comment = new dbComment();
                comment.del(foodId);
                System.Diagnostics.Trace.WriteLine("삭제");
            }            
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["food"]);
                renderEdit(id);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("error");
            }
        }
    }
}