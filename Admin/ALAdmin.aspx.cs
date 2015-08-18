using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class admin_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Article_Command(object sender, CommandEventArgs e)
    {
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            int actionNeedAuthority = 1;
            switch (e.CommandName)
            {
                case "doEdit":
                    actionNeedAuthority = 1;
                    if (admin_MasterPage.userAuthority >= actionNeedAuthority)
                    {
                        string strSQL = "SELECT * FROM ALTable WHERE ID=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {
                            doUpdateButton.CommandArgument = e.CommandArgument.ToString();
                            doUpdateButton.Visible = true;
                            doInsertButton.Visible = false;

                            titleTextBox.Text = dt.Rows[0]["ALName"].ToString();
                            contentTextBox.Text = dt.Rows[0]["ALContent"].ToString();
                            NewsListPanel.Visible = false;
                            EditPanel.Visible = true;
                            ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定新闻或通知时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定文章时发生错误", "", this);
                        }
                    }
                    else
                    {
                        JScript.Alert("您无权进行此操作", this);
                    }

                    break;
                case "doUpdate":
                    actionNeedAuthority = 1;
                    if (admin_MasterPage.userAuthority >= actionNeedAuthority)
                    {
                        if (titleTextBox.Text == "")
                        {
                            JScript.Alert("请输入文章标题", this);
                            return;
                        }
                        if (contentTextBox.Text == "")
                        {
                            JScript.Alert("请输入文章内容", this);
                            return;
                        }

                        string strSQL = "UPDATE ALTable SET ALName=@title, ALContent=@content WHERE ID=" + e.CommandArgument;
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        command.Parameters.Add("@title", OleDbType.VarChar).Value = titleTextBox.Text;
                        command.Parameters.Add("@content", OleDbType.VarChar).Value = contentTextBox.Text;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改新闻或通知,操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("文章已修改", "", this);

                            NewsListPanel.Visible = true;
                            EditPanel.Visible = false;
                            ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改新闻或通知时发生错误，返回受影响数据库条数为零。操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("修改文章时发生错误", "", this);
                        }
                    }
                    else
                    {
                        JScript.Alert("您无权进行此操作", this);
                    }
                    break;
                case "doDelete":
                    actionNeedAuthority = 1;
                    if (admin_MasterPage.userAuthority >= actionNeedAuthority)
                    {
                        string strSQL = "DELETE FROM ALTable WHERE ID=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除新闻或通知,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("该文章已被删除", "", this);
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除新闻或通知时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("删除文章时发生错误", "", this);
                        }
                    }
                    else
                    {
                        JScript.Alert("您无权进行此操作", this);
                    }

                    break;
                case "doView":
                    actionNeedAuthority = 1;
                    if (admin_MasterPage.userAuthority >= actionNeedAuthority)
                    {
                        string strSQL = "SELECT * FROM ALTable WHERE ID=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {

                            ArticleViewTitle.Text = dt.Rows[0]["ALName"].ToString();
                            viewDoEditButton.CommandArgument = e.CommandArgument.ToString();
                            string articleInfo = string.Empty;

                            ArticleViewInfo.Text = articleInfo;
                            ArticleView.Text = dt.Rows[0]["ALContent"].ToString();
                            NewsListPanel.Visible = false;
                            EditPanel.Visible = false;
                            ViewPanel.Visible = true;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定新闻或通知时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定文章时发生错误", "", this);
                        }

                    }
                    else
                    {
                        JScript.Alert("您无权进行此操作", this);
                    }
                    break;
            }
        }
    }
    protected void Article_New(object sender, EventArgs e)
    {
        doUpdateButton.Visible = false;
        doInsertButton.Visible = true;

        titleTextBox.Text = "";
        contentTextBox.Text = "";

        NewsListPanel.Visible = false;
        EditPanel.Visible = true;
        ViewPanel.Visible = false;
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
    protected void Article_Insert(object sender, EventArgs e)
    {
        if (titleTextBox.Text == "")
        {
            JScript.Alert("请输入文章标题", this);
            return;
        }
        if (contentTextBox.Text == "")
        {
            JScript.Alert("请输入文章内容", this);
            return;
        }

        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string strSQL = "INSERT INTO ALTable(ALName,ALContent) values (@title,@content)";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@title", OleDbType.VarChar).Value = titleTextBox.Text;
            command.Parameters.Add("@content", OleDbType.VarChar).Value = contentTextBox.Text;

            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加新闻或通知,标题:" + titleTextBox.Text);
                JScript.AlertAndRedirect("文章已添加", "", this);

                NewsListPanel.Visible = true;
                EditPanel.Visible = false;
                ViewPanel.Visible = false;
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加新闻或通知时发生错误，返回受影响数据库条数为零。欲添加标题: " + titleTextBox.Text);
                JScript.AlertAndRedirect("添加文章时发生错误", "", this);
            }
        }

    }
    protected void Back_List(object sender, EventArgs e)
    {
        NewsListPanel.Visible = true;
        EditPanel.Visible = false;
        ViewPanel.Visible = false;
    }

}