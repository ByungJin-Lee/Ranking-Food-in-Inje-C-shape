using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ViewUtility
{
    public class dbControl
    {
        //db관련 컨트롤    
        private string dbPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TeamplDB;Integrated Security=True;";
        //
        protected SqlConnection conn;
        protected DataTable dbNow;
        //db관련 인스턴스 생성용 및 주소
        public dbControl()
        {
            //생성자 정의
            conn = new SqlConnection();
            conn.ConnectionString = dbPath;
            dbNow = new DataTable();
        }
        public void ExcuteQ(string sql)
        {
            //Query 실행 no return
            try
            {
                conn.Open();
                SqlCommand sqlC = new SqlCommand(sql, conn);
                sqlC.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("db ExcuteQ : error");
            }
        }
        public DataTable rExcuteQ(string sql)
        {
            //Query 실행 return
            try
            {
                conn.Open();
                SqlDataAdapter sqlD = new SqlDataAdapter(sql, conn);
                DataTable responseData = new DataTable();
                sqlD.Fill(responseData);
                conn.Close();

                return responseData;
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("db rExcuteQ : error");
                conn.Close();
                return null;
            }
        }
        protected int countData(string sqlC)
        {
            //존재하는 데이터 갯수 확인
            try
            {
                return rExcuteQ(sqlC).Rows.Count;
            }
            catch
            {
                return -1;
            }
        }
    }

    //dbControl로부터 상속된 클래스
    public class dbUser : dbControl
    {
        public string tName { get; set; }
        public dbUser()
        {
            tName = "userData";
            //유저관련 테이블 이름
        }
        public DataTable getAll()
        {
            //사용자 전부 가져오기
            string sqlQ = string.Format("SELECT * FROM {0}", tName);
            return this.rExcuteQ(sqlQ);
        }
        public void regUser(UserInfo user)
        {
            //유저 추가
            Random rCode = new Random();
            string userCode = rCode.Next(10000, 100000).ToString();
            //랜덤 코드 생성 (5자리)
            string sqlQ;
            if (user.userId == "AdminQ")
            {
                sqlQ = string.Format("INSERT INTO {0} (userId, userPw, Email, code, certification, gender, userName, class) VALUES ('{1}', N'{2}', '{3}', '{4}', '{5}', '{6}', N'{7}', 1)"
                    , tName, user.userId, user.Password, user.Email, userCode, user.Certification, user.gender, user.userName);
            }
            else
            {
                sqlQ = string.Format("INSERT INTO {0} (userId, userPw, Email, code, certification, gender, userName) VALUES ('{1}', N'{2}', '{3}', '{4}', '{5}', '{6}', N'{7}')"
                    , tName, user.userId, user.Password, user.Email, userCode, user.Certification, user.gender, user.userName);
            }
            this.ExcuteQ(sqlQ);
            VMail mail = new VMail();
            mail.send(user.Email, 0, userCode, user.userId);
            //인증코드 보내기
        }
        public DataRow login(string userId, string userPw)
        {
            string sqlQ = string.Format("SELECT * FROM {0} WHERE userId = '{1}' AND userPw = '{2}'", tName, userId, userPw);
            this.dbNow = rExcuteQ(sqlQ);
            int count = dbNow.Rows.Count;
            if (count == 1)
                return this.dbNow.Rows[0];
            //성공할 경우 유저 데이터를 반환함
            else
                return null;
        }
        public bool checkCode(string userId, string code)
        {
            //코드 체크
            string sqlQ = string.Format("SELECT * FROM {0} WHERE userId = '{1}'", tName, userId);
            this.dbNow = rExcuteQ(sqlQ);
            if (this.dbNow.Rows[0]["code"].ToString() == code)
            {
                //코드를 체크한 후 올바른 경우 이메일 인증 true
                string sql = string.Format("UPDATE {0} "
                    + "SET "
                        + "certification = 'true'"
                    + "WHERE userId = '{1}'", tName, userId);
                rExcuteQ(sql);
                return true;
            }
            else
                return false;
            //코드 인증이 올바르지 않은 경우 false
        }
        public bool checkId(string userId)
        {
            //db에 아이디가 존재하는 경우 true 반환
            string sqlQ = string.Format("SELECT * FROM {0} WHERE userId = '{1}'", tName, userId);
            if (countData(sqlQ) == 1)
                return true;
            else
                return false;
        }
        public bool findData(string option, string email, bool type)
        {
            string sqlQ = string.Format("SELECT * FROM {0} ", tName);
            switch (type)
            {
                case false:
                    sqlQ += string.Format("WHERE userName = N'{0}' AND Email = '{1}'", option, email);
                    //아이디 찾기(이름, 이메일 입력)
                    break;
                case true:
                    //비밀번호 찾기(아이디, 이메일 입력)
                    sqlQ += string.Format("WHERE userId = '{0}' AND Email = '{1}'", option, email);
                    break;
            }
            this.dbNow = rExcuteQ(sqlQ);
            if (dbNow.Rows.Count == 1)
            {
                VMail mail = new VMail();
                switch (type)
                {
                    //아이디의 경우 0, 비밀번호의 경우 1
                    case false:
                        mail.send(email, 1, this.dbNow.Rows[0]["userId"].ToString(), null);
                        break;
                    case true:
                        mail.send(email, 2, this.dbNow.Rows[0]["userPw"].ToString(), null);
                        break;
                }
                return true;
            }
            else
                return false;
            //올바른 인증이 아닐 경우 false 반환
        }
    }

    public class dbPost : dbControl
    {
        public string tName { get; set; }
        public string imagePath { get; set; }
        public delegate void postUpdate();
        public event postUpdate onUpdate;
        public dbPost()
        {
            this.tName = "postData";
            //게시글관련 테이블 이름
        }
        public DataTable getAll()
        {
            //게시글 전부 가져오기
            string sqlQ = string.Format("SELECT * FROM {0}", tName);
            return this.rExcuteQ(sqlQ);
        }
        public DataTable get(string postKind)
        {
            //게시글 일부 가져오기
            short sqlQpost = VRouter.rounting(postKind);
            if (sqlQpost == -1) return null;
            string sqlQ = string.Format("SELECT * FROM {0} WHERE postKind = '{1}'", tName, sqlQpost);
            //퀴리문 작성
            return this.rExcuteQ(sqlQ);
            //쿼리 실행 및 해당 게시물을 반환
        }
        public PostInfo getId(int postId)
        {
            //Id를 이용하여 게시글 데이터 조회
            string sqlQ = string.Format("SELECT * FROM {0} WHERE id = '{1}'", tName, postId);
            DataTable dt = this.rExcuteQ(sqlQ);
            return new PostInfo
            {
                Id = dt.Rows[0]["id"].ToString(),
                postKind = short.Parse(dt.Rows[0]["postKind"].ToString()),
                postFood = dt.Rows[0]["postFood"].ToString(),
                postStar = int.Parse(dt.Rows[0]["postStar"].ToString()),
                foodImage = dt.Rows[0]["imageURL"].ToString(),
                commentCount = int.Parse(dt.Rows[0]["commentCount"].ToString())
            };
        }
        public void add(PostInfo post)
        {
            //게시글 일부 추가
            string sqlQ = string.Format("INSERT INTO {0} (postKind, postFood, postDes, imageURL) VALUES "
                    + "('{1}', N'{2}', N'{3}', N'{4}')"
                    , tName, post.postKind, post.postFood, post.postDes, post.foodImage);
            //퀴리문 작성
            this.ExcuteQ(sqlQ);
            //쿼리문 실행
        }
        public void del(int Id)
        {
            //게시글 삭제
            string sqlQ = string.Format("DELETE FROM {0} WHERE Id = '{1}'", tName, Id);
            //쿼리문 작성
            dbComment comment = new dbComment();
            comment.delAll(Id);
            //foodData에서 삭제
            this.ExcuteQ(sqlQ);
            //쿼리문 실행
        }
        public void updata(int Id, PostInfo post)
        {
            //게시글 수정
            string sqlQ = string.Format("UPDATE {0} "
                + "SET "
                    + "postFood = N'{1}', postDes = N'{2}'"
                + "WHERE Id = '{3}'", tName, post.postFood, post.postDes, Id);
            //쿼리문 작성
            this.ExcuteQ(sqlQ);
            //쿼리문 실행
        }
        public string checkImage(FileUpload fileup)
        {
            //올바른 이미지인지 확인
            string[] imageExtenstion = { ".png", ".jpg", ".jpeg" };
            //확장자 가져오기
            string fileExtention = System.IO.Path.GetExtension(fileup.FileName).ToLower();
            foreach (string exitem in imageExtenstion)
            {
                if (fileExtention == exitem)
                {
                    string fileName = fileup.FileName.Replace(fileExtention, "");
                    short i = 1;
                    do
                    {
                        string sqlQ = string.Format("Select * From {0} WHERE imageURL = N'{1}({2}){3}'"
                            , tName, fileName, i.ToString(), fileExtention);
                        if (countData(sqlQ) == 0) break;
                        i++;
                    } while (true);
                    string file = fileName + "(" + i.ToString() + ")" + fileExtention;
                    fileup.SaveAs(this.imagePath + file);
                    return file;
                    //이미지 이름 반환
                }
            }
            return "";
        }
    }
    public class dbComment : dbControl
    {
        public string tName { get; set; }
        public dbComment()
        {
            tName = "foodData";
            //게시글 - 음식 관련 테이블 이름
        }
        public DataTable get(int parentId)
        {
            //게시글 - 음식 일부 가져오기                        
            string sqlQ = string.Format("SELECT * FROM {0} WHERE parentId = '{1}'", tName, parentId);
            //퀴리문 작성
            return this.rExcuteQ(sqlQ);
            //해당 게시물 - 음식 을 반환
        }
        public FoodInfo getFood(int foodId)
        {
            string sqlQ = "SELECT * FROM " + tName + " WHERE Id = " + foodId;
            DataTable dt = this.rExcuteQ(sqlQ);
            DataRow dtR = dt.Rows[0];
            return new FoodInfo
            {
                parentId = dtR["parentId"].ToString(),
                foodStar = short.Parse(dtR["foodStar"].ToString()),
            };
        }
        public bool add(FoodInfo food)
        {
            if (!check(food.writerId, food.parentId))
                return false;
            //이미 리뷰가 있는지 확인

            //게시글 - 음식 댓글 추가
            string sqlQ = "INSERT INTO " + tName + " (parentId, writer, foodstar, comment, writerId) VALUES "
                    + "('" + food.parentId + "', N'" + food.writer + "', '" + food.foodStar + "', N'" + food.comment + "', N'" + food.writerId + "')";
            //퀴리문 작성
            this.ExcuteQ(sqlQ);
            //쿼리문 실행
            updataPost(food, false);

            return true;
        }
        private void updataPost(FoodInfo food, bool type)
        {
            //포스트 업데이트
            int totalStar;
            int totalComment;
            //post 데이터 가져오기
            dbPost dbpost = new dbPost();
            PostInfo postInfo = dbpost.getId(int.Parse(food.parentId));
            //수정사항(별점, 총 코멘트 수)
            switch (type)
            {
                case false:
                    //추가
                    totalStar = postInfo.postStar + food.foodStar;
                    totalComment = postInfo.commentCount + 1;
                    break;
                case true:
                    //삭제
                    totalStar = postInfo.postStar - food.foodStar;
                    totalComment = postInfo.commentCount - 1;
                    break;
                default:
                    return;
            }
            string sqlQ = "UPDATE postData SET "
                + "postStar = '" + totalStar + "', commentCount = '" + totalComment + "'"
                + "WHERE Id = '" + food.parentId + "'";
            this.ExcuteQ(sqlQ);
            //업데이트
        }
        public void del(int Id)
        {
            string sqlQ = "DELETE FROM " + tName + " WHERE Id = '" + Id + "'";
            FoodInfo food = getFood(Id);
            updataPost(food, true);
            this.ExcuteQ(sqlQ);
        }
        public void delAll(int parentId)
        {
            string sqlQ = "DELETE FROM " + tName + " WHERE parentId = " + parentId;
            this.ExcuteQ(sqlQ);
        }
        public bool check(string writerId, string parentId)
        {
            string sql = "SELECT * FROM foodData WHERE parentId = '" + parentId + "' AND writerId = N'" + writerId + "'";
            if (this.countData(sql) == 0)
                return true;
            else
                return false;
        }
    }

    public class UserInfo
    {
        //유저 정보 구조  
        public int Id { get; set; }
        public string userId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool Certification { get; set; }
        public bool gender { get; set; }
        public string userName { get; set; }
        //관리자 권한 유무
        public bool master { get; set; }
    }
    public class PostInfo
    {
        //게시판 구조
        //식별자
        public string Id { get; set; }
        //게시판 종류
        public short postKind { get; set; }
        //음식
        public string postFood { get; set; }
        //별점
        public int postStar { get; set; }
        //설명
        public string postDes { get; set; }
        //사용자수
        public int commentCount { get; set; }
        //이미지
        public string foodImage { get; set; }
    }
    public class FoodInfo
    {
        //자식 게시판 구조
        //부모 게시판 식별
        public string parentId { get; set; }
        //작성자
        public string writer { get; set; }
        //적성자 아이디
        public string writerId { get; set; }
        //작성 일자
        public string postDate { get; set; }
        //별점
        public short foodStar { get; set; }
        //내용
        public string comment { get; set; }
        //gender
        public bool gender { get; set; }
    }
}