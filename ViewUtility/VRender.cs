using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ViewUtility
{
    public class VRender
    {
        public string imagePath { get; set; }
        public string defaultURL { get; set; }
        //연결 기본 주소
        public double itemDelay { get; set; }
        //애니메이션 딜레이 초기값
        public double delayGap { get; set; }
        //애니메이션 딜레이 차이                      
    }
    public class Rpost : VRender
    {
        public event EventHandler onUpdata;
        public event EventHandler onDelete;
        public PostInfo topItem { get; set; }
        public Rpost()
        {
            this.defaultURL = "~/Page/Post/Food.aspx?food=";
            this.itemDelay = 0.5;
            this.delayGap = 0.25;
            this.imagePath = "";
            topItem = null;
        }
        public void render(Table Parent, DataTable data)
        {
            //데이터 화면에 뿌리기
            Parent.Controls.Clear();
            //자식 제거
            Image fImage;
            HyperLink fName;
            Label fStar;
            Label displayEmpty;
            TableRow row;
            TableCell cell;
            //구성요소 생성
            double delayInit = this.itemDelay;
            //애니메이션 딜레이 초기값                  

            if (data.Rows.Count != 0)
            {
                //데이터 갯수가 0이 아닐 경우
                foreach (DataRow item in data.Rows)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    //인스턴스 생성
                    fImage = new Image();
                    fName = new HyperLink();
                    fStar = new Label();
                    //데이터가 있는 경우 각 열 생성                                                            
                    fImage.ImageUrl = this.imagePath + item["imageURL"].ToString();
                    cell.CssClass = "image";
                    cell.Controls.Add(fImage);
                    row.Cells.Add(cell);
                    //이미지 설정                    
                    fName.NavigateUrl = this.defaultURL + item["Id"].ToString();
                    fName.Text = item["postFood"].ToString();
                    cell = new TableCell();
                    cell.CssClass = "name";
                    cell.Controls.Add(fName);
                    row.Cells.Add(cell);
                    //이름 설정
                    //calc Star
                    int coCount = int.Parse(item["commentCount"].ToString());
                    int postS = int.Parse(item["postStar"].ToString());
                    if (coCount == 0)
                        fStar.Text = "0/5(0)";
                    else
                        fStar.Text = string.Format("{0:F1}/5({1})", ( (double)postS / coCount), coCount);
                    cell = new TableCell();
                    cell.CssClass = "star";
                    cell.Controls.Add(fStar);
                    row.Cells.Add(cell);
                    row.CssClass = "line";
                    //별점 라벨 설정
                    string delay = delayInit + "s";
                    row.Style.Add("-webkit-animation-delay", delay);
                    row.Style.Add("-moz-animation-delay", delay);
                    row.Style.Add("-o-animation-delay", delay);
                    row.Style.Add("animation-delay", delay);
                    //애니메이션 딜레이 관련 css
                    delayInit += this.delayGap;
                    //딜레이 차이
                    Parent.Controls.Add(row);
                    //테이블에 row 추가

                    //top Item
                    if(topItem == null || topItem.postStar < postS)
                    {
                        topItem = new PostInfo
                        {
                            postStar = postS,
                            postFood = fName.Text,
                            commentCount = coCount,
                            foodImage = this.imagePath + item["imageURL"].ToString(),
                            Id = item["Id"].ToString()
                        };
                    }
                }
            }
            else
            {
                //0일 경우
                row = new TableRow();
                cell = new TableCell();
                //줄, 열 인스턴스 생성
                displayEmpty = new Label();
                displayEmpty.Text = "No Post";
                displayEmpty.CssClass = "empty";
                //비어있을 경우 no data 라벨 출력
                cell.Controls.Add(displayEmpty);
                row.Cells.Add(cell);
                Parent.Controls.Add(row);
                //추가
            }
        }
        public void renderEdit(Table Parent, DataTable data)
        {
            //수정용 페이지 생성
            Parent.Controls.Clear();
            //자식 제거
            Image fImage;
            TextBox fName;
            Button fBtn;
            int count = 0;
            Label displayEmpty;
            TableRow row;
            TableCell cell;
            //구성요소 생성
            if (data.Rows.Count != 0)
            {
                //데이터 갯수가 0이 아닐 경우
                foreach (DataRow item in data.Rows)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    //인스턴스 생성                                        
                    fImage = new Image();
                    fName = new TextBox();
                    fBtn = new Button();
                    //데이터가 있는 경우 각 열 생성                                        
                    fImage.ImageUrl = this.imagePath + item["imageURL"].ToString();
                    cell.CssClass = "image";
                    cell.Controls.Add(fImage);
                    row.Cells.Add(cell);
                    //이미지 설정                    
                    fName.ID = "itemN" + (count++);
                    fName.Text = item["postFood"].ToString();
                    cell = new TableCell();
                    cell.CssClass = "name";
                    cell.Controls.Add(fName);
                    row.Cells.Add(cell);
                    //이름 설정
                    fBtn.Text = "수정";
                    fBtn.Click += this.onUpdata;
                    cell = new TableCell();
                    cell.CssClass = "star";
                    cell.Controls.Add(fBtn);
                    fBtn = new Button();
                    fBtn.Text = "삭제";
                    fBtn.Click += this.onDelete;
                    cell.Controls.Add(fBtn);
                    //dataset 지정
                    row.Attributes.Add("data-postId", item["Id"].ToString());
                    row.CssClass = "line";
                    row.Cells.Add(cell);
                    //버튼 설정
                    Parent.Controls.Add(row);
                    //테이블에 row 추가
                }
            }
            else
            {
                //0일 경우
                row = new TableRow();
                cell = new TableCell();
                //줄, 열 인스턴스 생성
                displayEmpty = new Label();
                displayEmpty.Text = "No Post";
                displayEmpty.Height = 110;
                displayEmpty.CssClass = "empty";
                //비어있을 경우 no data 라벨 출력
                cell.Controls.Add(displayEmpty);
                row.Cells.Add(cell);
                Parent.Controls.Add(row);
                //추가
            }
        }
    }
    public class RComment : VRender
    {        
        public event EventHandler onDelete;
        public RComment()
        {
            this.defaultURL = "~/Page/Post/Food.aspx?food=";
            this.itemDelay = 0.5;
            this.delayGap = 0.25;
            this.imagePath = "";
        }
        public void render(HtmlGenericControl parent, DataTable data)
        {
            Label userName, comment;            

            if (data.Rows.Count > 0)
            {
                parent.Controls.Clear();

                foreach (DataRow item in data.Rows)
                {
                    var itemDiv = new HtmlGenericControl("div");
                    var starBox = new HtmlGenericControl("div");
                    var progress = new HtmlGenericControl("div");
                    var outer_progress = new HtmlGenericControl("div");

                    userName = new Label();
                    comment = new Label();                    
                    //인스턴스 생성                    
                    userName.Text = item["writer"].ToString();
                    userName.Attributes.Add("title", userName.Text);
                    itemDiv.Controls.Add(userName);                    
                    //userName                    
                    comment.Text = item["comment"].ToString();
                    itemDiv.Controls.Add(comment);                    
                    //comment                    
                    int value = int.Parse(item["foodStar"].ToString());
                    starBox.Attributes.Add("title", value + "점");

                    progress.Style.Add("width", 20 * value + "%");
                    outer_progress.Controls.Add(progress);

                    starBox.Controls.Add(outer_progress);
                    itemDiv.Controls.Add(starBox);
                    //star
                    parent.Controls.Add(itemDiv);
                }
            }
            else
            {
                Label displayEmpty;                                
                //줄, 열 인스턴스 생성
                displayEmpty = new Label();
                displayEmpty.Text = "No Comment";
                displayEmpty.Height = 110;
                displayEmpty.CssClass = "empty";
                //비어있을 경우 no data 라벨 출력                
                parent.Controls.Add(displayEmpty);
                //추가
            }
        }
        public void renderEdit(HtmlGenericControl parent, DataTable data)
        {
            Label userName, comment;            
            Button delBtn;

            if (data.Rows.Count > 0)
            {
                parent.Controls.Clear();
                foreach (DataRow item in data.Rows)
                {
                    var itemDiv = new HtmlGenericControl("div");
                    var starBox = new HtmlGenericControl("div");
                    var delSpace = new HtmlGenericControl("div");
                    delBtn = new Button();

                    userName = new Label();
                    comment = new Label();
                    //인스턴스 생성                    
                    userName.Text = item["writer"].ToString();
                    userName.Attributes.Add("title", userName.Text);
                    itemDiv.Controls.Add(userName);
                    //userName                    
                    comment.Text = item["comment"].ToString();
                    itemDiv.Controls.Add(comment);                    
                    //comment                                        
                    //del                    
                    delBtn.Text = "Del";
                    delBtn.Click += this.onDelete;
                    delSpace.Controls.Add(delBtn);
                    itemDiv.Controls.Add(delSpace);
                    itemDiv.Attributes.Add("data-foodId", item["Id"].ToString());
                    //star
                    parent.Controls.Add(itemDiv);
                }
            }
            else
            {
                Label displayEmpty;
                //줄, 열 인스턴스 생성
                displayEmpty = new Label();
                displayEmpty.Text = "No Comment";
                displayEmpty.Height = 110;
                displayEmpty.CssClass = "empty";
                //비어있을 경우 no data 라벨 출력                
                parent.Controls.Add(displayEmpty);
                //추가
            }
        }
    }

}