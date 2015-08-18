using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class LogInPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx",false);
    }
    protected void OKButton_Click(object sender, EventArgs e)
    {
        string userName = AdminNameTextBox.Text.Trim();
        string userPassword = PasswordTextBox.Text.Trim();
        string rightPassword = "";
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))
        {
            return;
        }
        
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection connection = new OleDbConnection(strConnection);
        using (connection)
        {
            connection.Open();
            string strSQL = "SELECT YHPassword FROM YHTable WHERE YHName = @YHName";
            OleDbCommand command = new OleDbCommand(strSQL, connection);
            command.Parameters.AddWithValue("@YHName", userName);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                rightPassword = dt.Rows[0]["YHPassword"].ToString();

                if (rightPassword == MD5Provider.Hash(userPassword))
                {
                    strSQL = "SELECT lastlogintime FROM YHTable WHERE YHName=@YHName";
                    command.Parameters.Clear();
                    command.CommandText = strSQL;
                    command.Parameters.AddWithValue("@YHName", userName);
                    //OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.SelectCommand = command;
                    dt.Clear();
                    adapter.Fill(dt);
                    string strLastLoginTime = dt.Rows[0]["lastlogintime"].ToString();

                    strSQL = "UPDATE YHTable SET lastlogintime = @lastlogintime WHERE YHName=@YHName";
                    command.Parameters.Clear();
                    command.CommandText = strSQL;
                    command.Parameters.AddWithValue("@lastlogintime", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@YHName", userName);
                    command.ExecuteNonQuery();

                    Session["AdminName"] = userName;
                    Session["LastLoginTime"] = strLastLoginTime;

                    Response.Redirect("AdminPages/AdminDefault.aspx", false);
                    return;
                }
            }
        }
        JScript.Alert("登陆出错！", this.Page);
       
    }
}
