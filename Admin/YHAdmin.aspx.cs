using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class Admin_YH : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //强制显示分页 
        if (GridView1.Rows.Count != 0)
        {
            Control table = GridView1.Controls[0];
            int count = table.Controls.Count;
            table.Controls[count - 1].Visible = true;
            
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        InsertTable.Visible = true;
    }

    protected void DoInsertButton_Click(object sender, EventArgs e)
    {
        /*
         * 1.检查输入是否合法（用户名必须输入，权限为1到5）
         * 2.检查是否有此用户名
         * 3.执行添加
         */

        if (UserNameTextBox.Text == "")
        {
            UserNameTextBoxLabel.Text = "请输入用户名";
            return; 
        }
        else
        {
            UserNameTextBoxLabel.Text = "";
        }
        if (PasswordTextBox.Text == "")
        {
            PasswordTextBoxLabel.Text = "请输入密码";
            return;
        }
        else
        {
            PasswordTextBoxLabel.Text = "";
        }
        int uAuth = 0;
        try
        {
            int.TryParse(AuthorityTextBox.Text, out uAuth);
        }
        catch
        {
        }
        if (uAuth == 0)
        {
            AuthorityTextBoxLabel.Text = "权限必须为大于等于1，小于等于5的数字";
            return;
        }
        else
        {
            AuthorityTextBoxLabel.Text = "";
        }

        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string strSQL = "SELECT TOP 1 * FROM YHTable WHERE YHName=@userName";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@userName", OleDbType.VarChar).Value = UserNameTextBox.Text;
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                UserNameTextBoxLabel.Text = "该用户已存在";
            }
            else
            {
                strSQL = "INSERT INTO YHTable(YHName,YHPassword,YHAuthority) values (@userName,@userPassword,@userAuthority)";
                command = new OleDbCommand(strSQL, objConnection);
                command.Parameters.Add("@userName", OleDbType.VarChar).Value = UserNameTextBox.Text;
                command.Parameters.Add("@userPassword", OleDbType.VarChar).Value = MD5Provider.Hash(PasswordTextBox.Text);
                command.Parameters.Add("@userAuthority", OleDbType.Numeric).Value = uAuth;
                if (command.ExecuteNonQuery() > 0)
                {
                    MyBasePage.writeLog(Session["userName"].ToString(), "添加用户，账户: " + UserNameTextBox.Text + "  权限:" + uAuth.ToString());
                    JScript.AlertAndRedirect("用户已添加", "", this);
                }
                else
                {
                    MyBasePage.writeLog(Session["userName"].ToString(), "添加用户时发生错误，返回受影响数据库条数为零。欲添加账户: " + UserNameTextBox.Text + "  权限:" + uAuth.ToString());
                    JScript.AlertAndRedirect("添加帐号时发生错误", "", this);
                }


            }
        }
    }

    protected void ClearInsertTable_Click(object sender, EventArgs e)
    {
        UserNameTextBox.Text = "";
        PasswordTextBox.Text = "";
        AuthorityTextBox.Text = "1";
        GridView1.Visible = true;
        InsertTable.Visible = false;
    }
    protected void ModifyButton_Command(object sender, CommandEventArgs e)
    {
        GridView1.Visible = false;
        int rowIndex = int.Parse(e.CommandArgument.ToString());

        mUserNameLabel.Text = GridView1.Rows[rowIndex].Cells[1].Text;
        mAuthorityTextBox.Text = GridView1.Rows[rowIndex].Cells[2].Text;
        ModifyTable.Visible = true;

    }


    protected void DoModifyButton_Click(object sender, EventArgs e)
    {
        /*
         * 1.检查输入是否合法（用户名必须输入，权限为1到5）
         * 2.检查是否有此用户名
         * 3.执行添加
         */

        int uAuth = 0;
        try
        {
            int.TryParse(mAuthorityTextBox.Text, out uAuth);
        }
        catch
        {
        }
        if (uAuth == 0)
        {
            mAuthorityTextBoxLabel.Text = "权限必须为大于等于1，小于等于5的数字";
            return;
        }
        else
        {
            mAuthorityTextBoxLabel.Text = "";
        }

        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string strSQL = string.Empty;
            if (mPasswordTextBox.Text == "")
            {
                strSQL = "UPDATE YHTable SET YHAuthority=@userAuthority WHERE YHName=\"" + mUserNameLabel.Text + "\"";
            }
            else
            {
                strSQL = "UPDATE YHTable SET YHPassword=@userPassword, YHAuthority=@userAuthority WHERE YHName=\"" + mUserNameLabel.Text + "\"";
            }
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            //command.Parameters.Add("@userName", OleDbType.VarChar).Value = mUserNameLabel.Text;
            if (mPasswordTextBox.Text != "") command.Parameters.Add("@userPassword", OleDbType.VarChar).Value = MD5Provider.Hash(mPasswordTextBox.Text);
            command.Parameters.Add("@userAuthority", OleDbType.Numeric).Value = uAuth;
            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "修改用户，账户: " + mUserNameLabel.Text + "  权限:" + uAuth.ToString());
                JScript.AlertAndRedirect("用户信息已修改", "", this);
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "修改用户时发生错误，返回受影响数据库条数为零。账户: " + mUserNameLabel.Text + "  欲修改为:权限:" + uAuth.ToString() );
                JScript.AlertAndRedirect("修改用户信息时发生错误", "", this);
            }
        }
    }

    protected void ClearModifyTable_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        ModifyTable.Visible = false;
    }

}