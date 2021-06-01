using System.Data;

namespace ViewUtility
{
    public class VRouter
    {
        static public short rounting(string postKind)
        {
            short kindValue = -1;
            try
            {
                switch (postKind)
                {
                    //switch를 통한 게시글 종류 확인
                    case "인정관":
                        kindValue = 0;
                        break;
                    case "인덕재":
                        kindValue = 1;
                        break;
                    case "늘빛관":
                        kindValue = 2;
                        break;
                    default:
                        dbControl dbCon = new dbControl();
                        string sql = string.Format("SELECT * FROM {0} WHERE postKind = {1}", "postData", postKind);
                        DataTable dbTable = dbCon.rExcuteQ(sql);
                        if (dbTable.Rows.Count > 0)
                            kindValue = short.Parse(postKind);
                        break;
                }
                return kindValue;
            }
            catch
            {
                return -1;
            }
        }
    }
}
