using System.Net.Mail;

namespace ViewUtility
{
    public class VMail //메일 서비스
    {
        private MailAddress mailFrom;
        private MailAddress mailTo;
        string mailKey = "xxxxx";
        //mailKey는 cntsmtp0110 계정의 Password
        string smtpHost = "smtp.gmail.com";
        int smtpPort = 587;
        //gmail의 smtp서비스 속성        
        
        public VMail()
        {
            this.mailFrom = new MailAddress("xxxxx@gmail.com", "인제대에서 뭐 물래?");            
        }
        public void send(string _mailTo, int select, string content, string userId) //sMail 생성자
        {
            this.mailTo = new MailAddress(_mailTo);
            
            //클래스의 변수에 값을 기록            
            MailMessage message = new MailMessage(this.mailFrom.Address, this.mailTo.Address);
            message.From = this.mailFrom;
            SmtpClient client = new SmtpClient(this.smtpHost, this.smtpPort);

            message.IsBodyHtml = true;            
            string kind = "";
            switch (select)
            {
                case 0:
                    message.Subject = "이메일 인증 코드";
                    kind = "인증 코드";
                    break;
                case 1:
                    message.Subject = "아이디 찾기";
                    kind = "등록된 아이디";
                    break;
                case 2:
                    message.Subject = "비밀번호 찾기";
                    kind = "등록된 비밀번호";
                    break;
            }
            string bodyText = "<html>"
            + "<body style='width: 100%;'>"
            + @"<div style='background:#404040; text-align:left; color:#ffffff; padding:10px;'>인제대에서 뭐 물래?</div>"
            + @"<div style='background: #e0e0e0;height:500px;font-size: 1.2em;'>"
                    + "<span style='display:block;padding:150px 0px;text-align:center;color: rgba(0, 0, 0, 0.65); font-size: 1.4em; font-weight: bold; padding-bottom: 1em;'>"+kind+"</span>"
                    + "<div style='text-align:center; margin-top:50px;'><span style='padding: 10px; background-color: #000; font-size: 1.2em; color: #fff;'>"+content+"</div></div>"
            + @"</div >"
            + "</body>"
            + "</html>";
            //body 구성 및 가공;
            //body html 가공
            message.Body = bodyText;

            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            //메일의 charset을 utf8로 바꿈;

            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(this.mailFrom.Address, this.mailKey);
            //각종 인증 및 전송방식을 선택

            try
            {
                client.Send(message);
            }
            catch
            {
                //if fail then commit Log
                VLog.User log = new VLog.User();
                string id = (userId != null) ? userId : _mailTo;
                log.commitLog(new VLog.User.LogBlock
                {                    
                    postKind = "Mail",
                    postItem = select.ToString(),
                    connection = false,
                    userID = id
                });
            }
            //message에 속성값을 적용시킨 후 보냄.
        }
    }
}
