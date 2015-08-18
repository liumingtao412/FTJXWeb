using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail; 

/// <summary>
/// SendEmail 的摘要说明
/// </summary>
public class SendEmail
{
    static public string strEmailForMessage = string.Empty;
	public SendEmail()
	{
		//		
		//
	}
    static public bool StaticSendEmail(string toEmail,string title,string contents)
    {
        //测试用
        //toEmail = "liumingtao412@sina.com";
        MailMessage message = new MailMessage();

     


        message.From = new MailAddress("email_delegate@126.com","富藤网站留言");
        //message.To.Add(new MailAddress("jerryliu@ftjx.cn"));
        message.To.Add(new MailAddress(toEmail));
        message.Bcc.Add(new MailAddress("liumingtao412@hotmail.com"));
        message.Subject = title;
        message.Priority = MailPriority.High;
        message.IsBodyHtml = true;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Body = contents;
        try
        {
            //发送
           // SmtpClient smtp = new SmtpClient("smtp.qq.com");
            SmtpClient smtp = new SmtpClient("smtp.126.com");
            smtp.Credentials = new System.Net.NetworkCredential("email_delegate@126.com", "5028177");
            smtp.EnableSsl = false;


            smtp.Send(message);

            return true;

        }
        catch (Exception ee)
        {
            string str = ee.ToString();

            return false;
        }
    }


}
