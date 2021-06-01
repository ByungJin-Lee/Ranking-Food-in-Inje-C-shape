using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Post
{
    public partial class Food : System.Web.UI.Page
    {
        int postId;
        dbComment comment;
        dbPost post;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
                Response.Redirect("~/Page/Login/login.aspx");
            //로그인 확인
            if (!bool.Parse(Session["mailCertification"].ToString()))
                Response.Redirect("~/Page/Login/code.aspx");
            //이메일 인증 확인
            if (bool.Parse(Session["master"].ToString()))
                adminControl.Visible = true;
            //First Time then we will render
            if (!IsPostBack)
            {
                string id = Request.QueryString["food"];
                updata.NavigateUrl += id;
                if (id == null) return;
                postId = int.Parse(id);                                
                profile_Render(postId);
                render_commentTable(postId);
            }
        }

        protected void profile_Render(int id)
        {
            post = new dbPost();
            PostInfo foodPost = post.getId(id);
            double starValue;
            //if there is no data then we do not calc Value.
            if (foodPost.commentCount == 0)
            {
                proStar.Text = "0/5";
                starValue = 0;
            }
            else
            {
                starValue = (double)foodPost.postStar / foodPost.commentCount;
                proStar.Text = string.Format("{0:F1}/5", starValue);
            }
            proImg.ImageUrl = @"~/imageData/" + foodPost.foodImage;
            proName.Text = foodPost.postFood;            
            proCount.Text = foodPost.commentCount+ "";
            proGa.Style.Add("width", 20 * starValue + "%");
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputComment.Text))
                return;
            //만약 코맨트가 없거나 공백만 있는 경우 함수 종료

            short star = calcStar();            
            comment = new dbComment();
            bool su = comment.add(new FoodInfo
            {
                parentId = Request.QueryString["food"],
                writer = Session["userName"].ToString(),
                comment = inputComment.Text.Trim(),
                foodStar = star,
                writerId = Session["userId"].ToString(),
            });

            inputComment.Text = "";
            int postValue = int.Parse(Request.QueryString["food"]);

            if (!su)
            {
                System.Diagnostics.Trace.WriteLine("이미 존재하는 데이터");
                inputComment.Text = "이미 리뷰를 작성하셨습니다.";
                render_commentTable(postValue);
                return;
            }

            //re Render
            
            profile_Render(postValue);
            render_commentTable(postValue);
        }

        protected void render_commentTable(int id)
        {
            comment = new dbComment();
            RComment Rcom = new RComment();
            DataTable dt = comment.get(id);            
            Rcom.render(commentListBox, dt);
        }

        protected short calcStar()
        {
            //calculator StarValue
            short starValue = 5;
            //default starValue is 5
            if (star1.Checked)
                starValue = 1;
            else if (star2.Checked)
                starValue = 2;
            else if (star3.Checked)
                starValue = 3;
            else if (star4.Checked)
                starValue = 4;

            return starValue;
        }
    }
}