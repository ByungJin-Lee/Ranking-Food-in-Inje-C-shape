using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Post
{
    public partial class Kind : System.Web.UI.Page
    {
        string postKind;
        dbPost dbPost;
        DataTable posts;
        ViewUtility.VLog.User uLog;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
                Response.Redirect("~/Page/Login/login.aspx");
                //로그인 확인
            if (!bool.Parse(Session["mailCertification"].ToString()))
                Response.Redirect("~/Page/Login/code.aspx");
                //이메일 인증 확인     
                
            if(!IsPostBack)
            {
                dbPost = new dbPost();
                postKind = Request.QueryString["c"].ToString();
                //url 받기                
                if (VRouter.rounting(postKind) == -1)
                {
                    //올바르지 않은 경로의 경우 함수 종료 및 경고 페이지로 이동
                    Response.Redirect("~/Page/Post/Warning.aspx");
                    return;
                }
                
                if (bool.Parse(Session["master"].ToString()))
                {
                    //관리자 인증(수정 권한 확인) 및 경로 설정
                    adminControl.Visible = true;
                    updata.NavigateUrl += postKind;
                }                
                posts = dbPost.get(postKind);
                if(posts != null)
                {
                    Rpost render = new Rpost();
                    render.imagePath = @"~/ImageData/";
                    render.render(foodItems, posts);

                    if(render.topItem != null)
                    {
                        oneImage.ImageUrl = render.topItem.foodImage;
                        oneTitle.Text = render.topItem.postFood;
                        if (render.topItem.commentCount > 0)
                            oneStar.Text = string.Format("별점: {0:F1}/5", ((double)render.topItem.postStar / render.topItem.commentCount));
                        else
                            oneStar.Text = "0/5(0)";
                        oneComment.Text = string.Format("등록된 리뷰 수: {0}", render.topItem.commentCount);
                        goFood.NavigateUrl += render.topItem.Id;
                    }
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine("error");
                }
                //로그 작성 기능
                uLog = new VLog.User();
                uLog.commitLog(new VLog.User.LogBlock
                {
                    userID = Session["userName"].ToString(),
                    connection = true,
                    postKind = postKind,
                    postItem = "",
                });
            }            
        }        
    }
}