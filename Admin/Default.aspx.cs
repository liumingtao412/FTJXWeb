using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



        }
    }

    protected void ModifyMyPassword_Click(object sender, EventArgs e)
    {
        if (OldPsd_TextBox.Text == "" || NewPsd_TextBox.Text == "")
        {
            JScript.Alert("输入不完整，请检查后再提交。", this);
            return;
        }
        if (NewPsd_TextBox.Text != NewPsdRe_TextBox.Text)
        {
            JScript.Alert("两次密码输入不同，请重新输入。", this);
            return;
        }
        if (NewPsd_TextBox.Text.Length < 6)
        {
            JScript.Alert("密码至少应为6个字符。", this);
            return;
        }
        string md5Password = MD5Provider.Hash(OldPsd_TextBox.Text);
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string strSQL = "SELECT Top 1 * FROM YHTable WHERE YHName = @userName";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@userName", OleDbType.VarChar).Value = Session["userName"].ToString();
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["YHPassword"].ToString() == md5Password)
                {
                    strSQL = "UPDATE YHTable SET YHPassword=@psw WHERE 编号=" + dt.Rows[0]["编号"].ToString();
                    command = new OleDbCommand(strSQL, objConnection);
                    command.Parameters.Add("@psw", OleDbType.VarChar).Value = MD5Provider.Hash(NewPsd_TextBox.Text);
                    command.ExecuteNonQuery();
                    MyBasePage.writeLog(Session["userName"].ToString(), "修改密码成功");
                    JScript.Alert("密码修改成功", this);
                }
                else
                {
                    MyBasePage.writeLog(Session["userName"].ToString(), "修改密码错误：旧密码输入错误");
                    JScript.Alert("密码修改失败：旧密码输入错误", this);
                }

            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "修改密码错误：无法在数据库中检索到用户原始信息");
                MyBasePage.logout(this);
                JScript.AlertAndRedirect("系统出现错误，请重新登录后再试", "../Deafult.aspx", this);
            }
        }
    }
}