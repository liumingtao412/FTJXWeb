using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

public partial class Admin_LY : System.Web.UI.Page
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
                    break;
                case "doUpdate":
                    break;
                case "doDelete":
                    actionNeedAuthority = 1;
                    if (admin_MasterPage.userAuthority >= actionNeedAuthority)
                    {
                        string strSQL = "UPDATE LYTable SET LYDelete=true WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除新闻或通知,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("该新闻或通知已删除", "", this);
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除新闻或通知时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("删除新闻或通知时发生错误", "", this);
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
                        string strSQL = "SELECT * FROM LYTable WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {

                            ArticleViewTitle.Text = dt.Rows[0]["LYTitle"].ToString();
                            viewDoEditButton.CommandArgument = e.CommandArgument.ToString();
                            string articleInfo = string.Empty;
                            articleInfo += ("<div>姓名:" + dt.Rows[0]["LYName"].ToString() + "</div>");
                            articleInfo += ("<div>留言时间:" + dt.Rows[0]["LYTime"].ToString() + "</div>");
                            articleInfo += ("<div>Email:" + dt.Rows[0]["LYEmail"].ToString() + "</div>");
                            articleInfo += ("<div>电话:" + dt.Rows[0]["LYTel"].ToString() + "</div>"); 

                            ArticleViewInfo.Text = articleInfo;
                            ArticleView.Text = dt.Rows[0]["LYContents"].ToString();
                            NewsListPanel.Visible = false;
                            ViewPanel.Visible = true;

                            strSQL = "UPDATE LYTable SET LYRead=true WHERE 编号=" + e.CommandArgument.ToString();
                            command = new OleDbCommand(strSQL, objConnection);
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定新闻或通知时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定新闻或通知时发生错误", "", this);
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

    protected void MarkAllReaded(object sender, EventArgs e)
    {
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            String strSQL = "UPDATE LYTable SET LYRead=true";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.ExecuteNonQuery();
        }
        JScript.AlertAndRedirect("已全部标记为已读", "", this);
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
    protected void Back_List(object sender, EventArgs e)
    {
        NewsListPanel.Visible = true;
        ViewPanel.Visible = false;
    }

}