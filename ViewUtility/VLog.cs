using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ViewUtility
{
    static public class VLog
    {
        //dbLog
        public class User
        {
            //user관련 log 데이터베이스
            private string userdbP = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TeamplDB;Integrated Security=True;";
            private SqlConnection uLogConn;
            //db인스턴스 생성 및 주소
            public User()
            {
                uLogConn = new SqlConnection();
                uLogConn.ConnectionString = userdbP;                
            }
            public class LogBlock
            {
                //유저용 log 형태
                //식별자
                public string Id { get; set; }
                //유저 ID
                public string userID { get; set; }
                //접속 성공 여부
                public bool connection { get; set; }
                //접속 시간
                public DateTime Time { get; set; }
                //접속 위치
                public string postKind { get; set; }
                //접속 상세 위치
                public string postItem { get; set; }
                //접속 IP
                public string Ip { get; set; }
            }
            public class Condition { 
                //조건
                //Field 명
                public string fieldName { get; set; } 
                //condition
                public string condition { get; set; }
            }
            public DataTable Fill()
            {
                //Grid용 sql 매서드
                if (uLogConn.State == 0) uLogConn.Open();
                //연결 여부 확인하기
                string sql = "SELECT * FROM userLog ORDER BY id DESC";
                SqlDataAdapter sqlData = new SqlDataAdapter(sql, uLogConn);                
                //sql 쿼리 실행
                DataTable responseDate = new DataTable();
                sqlData.Fill(responseDate);
                //데이터 가져오기
                uLogConn.Close();
                return responseDate;
                //데이터 반환
            }            
            public void commitLog(LogBlock log)
            {
                //로그 등록
                if(uLogConn.State == 0) uLogConn.Open();
                //연결 여부 확인하기
                string sql = string.Format("INSERT INTO userLog (userId, connection, postKind, postItem, accessIP, time) VALUES "
                    + "('{0}', '{1}', N'{2}', N'{3}', '{4}', N'{5}')"
                    , log.userID, log.connection.ToString(), log.postKind, log.postItem, HttpContext.Current.Request.UserHostAddress, DateTime.Now.ToString());
                try
                {
                    SqlCommand sqlC = new SqlCommand(sql, uLogConn);
                    sqlC.ExecuteNonQuery();
                    uLogConn.Close();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    uLogConn.Close();
                }
            }                        
        }                        
    }    
}