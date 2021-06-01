using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewUtility;

namespace HomePage.Page.Post
{
    public partial class Kind_updata : System.Web.UI.Page
    {
        dbPost dbPost;
        DataTable posts;        
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (!bool.Parse(Session["master"].ToString()))
                    Response.Redirect("~/Home.aspx");
                //관리자가 아닌 경우 Home으로 보냄                                                                                               
            }
            Draw();
        }

        protected void addFood_Click(object sender, EventArgs e)
        {
            dbPost = new dbPost();
            dbPost.imagePath = Server.MapPath(@"~/ImageData/");
            string foodN = foodName.Text.Trim();
            foodName.Text = "";
            short pKind = VRouter.rounting(Request.QueryString["c"].ToString());

            string img = dbPost.checkImage(imageUpload);

            if(foodN != null && pKind != -1)
            {
                dbPost.add(new PostInfo
                {
                    //postKind, postFood, postDes, imageURL
                    postDes = "",
                    postKind = pKind,
                    postFood = foodN,
                    foodImage = img,
                });
                Draw();
            }            
        }
        private void Render_onUpdata(object sender, EventArgs e)
        {
            TableRow parent = ((sender as Button).Parent as TableCell).Parent as TableRow;
            if (parent == null) return;
            //테이블이 비었을 경우
            int postId = int.Parse(parent.Attributes["data-postId"].ToString());
            string txtData = (parent.Cells[1].Controls[0] as TextBox).Text.Trim();
            if (txtData != "")
            {
                dbPost dbPost = new dbPost();
                dbPost.updata(postId, new PostInfo
                {
                    postFood = txtData   
                });                
            }            
        }
        private void Draw()
        {
            dbPost = new dbPost();
            posts = dbPost.get(Request.QueryString["c"]);
            if (posts == null) return;
            //데이터가 없을 경우 함수 탈출   
            Rpost render = new Rpost();
            render.imagePath = @"~/ImageData/";
            render.onUpdata += Render_onUpdata;
            render.onDelete += Render_onDelete;
            render.renderEdit(foodItems, posts);
        }

        private void Render_onDelete(object sender, EventArgs e)
        {
            TableRow parent = ((sender as Button).Parent as TableCell).Parent as TableRow;
            if (parent == null) return;
            //테이블이 비었을 경우
            int postId = int.Parse(parent.Attributes["data-postId"].ToString());            
            if (postId > 0)
            {
                dbPost dbPost = new dbPost();
                dbPost.del(postId);
            }            
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Draw();
        }
    }
}