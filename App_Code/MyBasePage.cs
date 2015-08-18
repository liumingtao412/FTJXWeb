using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// MyBasePage 的摘要说明
/// </summary>
public class MyBasePage:System.Web.UI.Page
{
	public MyBasePage()
	{
		//
	
        this.Load += new EventHandler(MyBasePage_Load);
		//
	}
    void MyBasePage_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null)
        {
            Response.Redirect("~/Default.aspx",false);
        }
    }

    /// <summary>
    /// 获取客户端IP
    /// </summary>
    /// <returns></returns>
    public static string getIP()
    {
        string clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; //适用于反向代理
        if (clientIP == null || clientIP == "")
        {
            clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return clientIP;
    }

    /// <summary>
    /// 保存系统日志到LogTable表
    /// </summary>
    /// <param name="userName">对应userName字段</param>
    /// <param name="operation">对应operation字段</param>
    public static void writeLog(string userName, string operation)
    {
        /*
         string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
                OleDbConnection objConnection = new OleDbConnection(strConnection);
                objConnection.Open();
                using (objConnection)
                {
                     string strSQL = "Insert INTO LogTable(userName,operation,operationTime,operationIP) values (@userName,@operation,@opTime,@opIP)";
                     OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                     command.Parameters.Add("@userName", OleDbType.VarChar).Value = userName;
                     command.Parameters.Add("@operation", OleDbType.VarChar).Value = operation;
                     command.Parameters.Add("@opTime", OleDbType.Date).Value = DateTime.Now;
                     command.Parameters.Add("@opIP", OleDbType.VarChar).Value = getIP();
                     command.ExecuteNonQuery();
                }
        */
    }

    /// <summary>
    /// 退出登录，清除登录信息
    /// </summary>
    /// <param name="page">请传递this</param>
    public static void logout(Page page)
    {
        page.Session["userName"] = null;
        page.Session["userAuthority"] = null;
        page.Session["personName"] = null;
        page.Session["lastLoginTime"] = null;
        page.Session["lastLoginIP"] = null;
    }


    /// <summary>
    /// 登录检查
    /// </summary>
    /// <param name="username">用户输入的用户名</param>
    /// <param name="password">用户输入的密码</param>
    /// <param name="errorText">登录错误时输出的错误信息</param>
    /// <param name="page">请传递this</param>
    /// <returns></returns>
    public static bool login_check(string username, string password, out string errorText, Page page)
    {
        errorText = "";
        if (username != "")
        {
            if (password != "")
            {
                string md5Password = MD5Provider.Hash(password);
                string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
                OleDbConnection objConnection = new OleDbConnection(strConnection);
                objConnection.Open();
                using (objConnection)
                {
                    string strSQL = "SELECT Top 1 * FROM YHTable WHERE YHName = @userName";
                    OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                    command.Parameters.Add("@userName", OleDbType.VarChar).Value = username;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0]["YHPassword"].ToString() == md5Password)
                        {
                            //登录成功，设置session
                            page.Session["userName"] = dt.Rows[0]["YHName"];
                            page.Session["userAuthority"] = dt.Rows[0]["YHAuthority"];
                            page.Session["lastLoginTime"] = dt.Rows[0]["lastlogintime"];
                            page.Session.Timeout = 60;

                            strSQL = "UPDATE YHTable SET lastlogintime=@lasttime WHERE 编号=" + dt.Rows[0]["编号"].ToString();
                            command = new OleDbCommand(strSQL, objConnection);
                            command.Parameters.Add("@lasttime", OleDbType.Date).Value = DateTime.Now;
                            command.ExecuteNonQuery();
                            writeLog(username, "登录成功");
                            return true;
                        }
                        else
                        {
                            errorText = "用户名或密码错误";
                            writeLog(username, "登录错误：用户名或密码错误");
                            return false;
                        }
                    }
                    else
                    {
                        errorText = "用户名或密码错误";
                        writeLog(username, "登录错误：无此用户");
                        return false;
                    }

                }
            }
            else
            {
                errorText = "请输入密码";
                writeLog(username, "登录错误：密码为空");
                return false;
            }
        }

        return false;
    }

    public static string FormatPicURL(string URL)
    {
        if (URL == "")
        {
            URL = "/images/nopic.gif";
        }
        return URL;
    }
    public static string FormatPicURL(object URL)
    {
        return FormatPicURL(URL.ToString());
    }
}
