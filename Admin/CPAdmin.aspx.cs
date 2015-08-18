using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class Admin_CP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            string _sql = "SELECT * FROM LBTable where LBDel<>True";
            OleDbDataAdapter da = new OleDbDataAdapter(_sql, objConnection);
            DataTable _dt = new DataTable();
            da.Fill(_dt);
            parentIDDropDown.DataSource = _dt;
            parentIDDropDown.DataTextField = "LBName";
            parentIDDropDown.DataValueField = "编号";
            parentIDDropDown.DataBind();
            parentIDDropDown.Items.Insert(0, new ListItem("无效类别", "-1"));
        }
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
                        string strSQL = "SELECT * FROM CPTable WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {
                            doUpdateButton.CommandArgument = e.CommandArgument.ToString();
                            doUpdateButton.Visible = true;
                            doInsertButton.Visible = false;

                            titleTextBox.Text = dt.Rows[0]["CPName"].ToString();
                            Txt_newsPicURL.Text = dt.Rows[0]["CPPicURL"].ToString();
                            infTextBox.Text = dt.Rows[0]["CPInf"].ToString();
                            contentTextBox.Text = dt.Rows[0]["CPDetails"].ToString();
                            try
                            {
                                parentIDDropDown.SelectedValue = dt.Rows[0]["CPLBID"].ToString();
                            }
                            catch
                            {
                                parentIDDropDown.SelectedValue = "-1";
                            }
                            //inMainPageCheckBox.Checked = Convert.ToBoolean(dt.Rows[0]["CPInMainPage"]);

                            NewsListPanel.Visible = false;
                            EditPanel.Visible = true;
                            ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定新闻或通知时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定产品信息时发生错误", "", this);
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
                            JScript.Alert("请输入产品名称", this);
                            return;
                        }
                        
                        string strSQL = "UPDATE CPTable SET CPName=@title, CPInf=@inf, CPDetails=@content, CPPicURL=@newsPic, CPLBID=@lbID, CPInMainPage=@inMain WHERE 编号=" + e.CommandArgument;
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        command.Parameters.Add("@title", OleDbType.VarChar).Value = titleTextBox.Text;
                        command.Parameters.Add("@inf", OleDbType.VarChar).Value = infTextBox.Text;
                        command.Parameters.Add("@content", OleDbType.VarChar).Value = contentTextBox.Text;
                        command.Parameters.Add("@newsPic", OleDbType.VarChar).Value = Txt_newsPicURL.Text;
                        command.Parameters.Add("@lbID", OleDbType.Integer).Value = parentIDDropDown.SelectedValue;
                        command.Parameters.Add("@inMain", OleDbType.Boolean).Value = false;
                        //command.Parameters.Add("@inMain", OleDbType.Boolean).Value = inMainPageCheckBox.Checked;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改新闻或通知,操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("产品信息已修改", "", this);

                            NewsListPanel.Visible = true;
                            EditPanel.Visible = false;
                            ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改新闻或通知时发生错误，返回受影响数据库条数为零。操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("修改产品信息时发生错误", "", this);
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
                        string strSQL = "UPDATE CPTable SET CPDel=true WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除新闻或通知,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("该产品信息已删除", "", this);
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除新闻或通知时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("删除产品信息时发生错误", "", this);
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
                        string strSQL = "SELECT * FROM CPTable WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {

                            ArticleViewTitle.Text = dt.Rows[0]["CPName"].ToString();
                            viewDoEditButton.CommandArgument = e.CommandArgument.ToString();
                            string articleInfo = string.Empty;
                            //articleInfo += ("<div>类别:" + dt.Rows[0]["CPLBID"].ToString() + "</div>");
                            //articleInfo += ("<div>首页展示:" + ( Convert.ToBoolean(dt.Rows[0]["CPInMainPage"]) ? "√" : "×" ) + "</div>");

                            ArticleViewInfo.Text = articleInfo;
                            ArticleView.Text = "<p>" + dt.Rows[0]["CPInf"].ToString() + "</p><p>&nbsp;</p><p>&nbsp;</p>" + dt.Rows[0]["CPDetails"].ToString();
                            if (dt.Rows[0]["CPPicURL"].ToString() != "")
                            {
                                ArticlePic.ImageUrl = dt.Rows[0]["CPPicURL"].ToString();
                                ArticlePic.Visible = true;
                            }
                            else
                            {
                                ArticlePic.Visible = false;
                            }
                            NewsListPanel.Visible = false;
                            EditPanel.Visible = false;
                            ViewPanel.Visible = true;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定新闻或通知时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定产品信息时发生错误", "", this);
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
        Txt_newsPicURL.Text = "";
        contentTextBox.Text = "";
        infTextBox.Text = "";
        //inMainPageCheckBox.Checked = false;

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
            JScript.Alert("请输入产品名称", this);
            return;
        }
        

        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string strSQL = "INSERT INTO CPTable(CPName,CPInf,CPDetails,CPPicURL,CPLBID,CPInMainPage) values (@title,@inf,@content,@newsPic,@lbID,@inMain)";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@title", OleDbType.VarChar).Value = titleTextBox.Text;
            command.Parameters.Add("@inf", OleDbType.VarChar).Value = infTextBox.Text;
            command.Parameters.Add("@content", OleDbType.VarChar).Value = contentTextBox.Text;
            command.Parameters.Add("@newsPic", OleDbType.VarChar).Value = Txt_newsPicURL.Text;
            command.Parameters.Add("@lbID", OleDbType.Integer).Value = parentIDDropDown.SelectedValue;
            //command.Parameters.Add("@inMain", OleDbType.Boolean).Value = inMainPageCheckBox.Checked;
            command.Parameters.Add("@inMain", OleDbType.Boolean).Value = false;
            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加新闻或通知,标题:" + titleTextBox.Text);
                JScript.AlertAndRedirect("产品信息已添加", "", this);

                NewsListPanel.Visible = true;
                EditPanel.Visible = false;
                ViewPanel.Visible = false;
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加新闻或通知时发生错误，返回受影响数据库条数为零。欲添加标题: " + titleTextBox.Text);
                JScript.AlertAndRedirect("添加产品信息时发生错误", "", this);
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