using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class Admin_LB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                string _sql = "SELECT * FROM LBTable where LBDel<>True";
                OleDbDataAdapter da = new OleDbDataAdapter(_sql, objConnection);
                DataTable _dt = new DataTable();
                da.Fill(_dt);
                parentIDDropDown.DataSource = _dt;
                parentIDDropDown.DataTextField = "LBName";
                parentIDDropDown.DataValueField = "编号";
                parentIDDropDown.DataBind();
                parentIDDropDown.Items.Insert(0, new ListItem("没有父类别", "-1"));
            }
           
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
                        string strSQL = "SELECT * FROM LBTable WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {
                            doUpdateButton.CommandArgument = e.CommandArgument.ToString();
                            doUpdateButton.Visible = true;
                            doInsertButton.Visible = false;

                            titleTextBox.Text = dt.Rows[0]["LBName"].ToString();
                            contentTextBox.Text = dt.Rows[0]["LBInf"].ToString();
                            IndexTextBox.Text = dt.Rows[0]["PosIndex"].ToString();
                            try
                            {
                                parentIDDropDown.SelectedValue = dt.Rows[0]["LBParentID"].ToString();
                            }
                            catch
                            {
                                parentIDDropDown.SelectedValue = "-1";
                            }
                            hasChildCheckBox.Checked = Convert.ToBoolean(dt.Rows[0]["LBHasChild"]);
                            NewsListPanel.Visible = false;
                            EditPanel.Visible = true;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定新闻或通知时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定内容时发生错误", "", this);
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
                            JScript.Alert("请输入标题", this);
                            return;
                        }
                        if (parentIDDropDown.SelectedValue == e.CommandArgument.ToString())
                        {
                            parentIDDropDown.SelectedValue = "-1";
                            JScript.Alert("不可指定自身为父级", this);
                            return;   
                        }
                        int index;
                        if (!int.TryParse(IndexTextBox.Text,out index))
                        {
                            index = 100;
                            JScript.Alert("显示顺序必须为整数", this);
                            return;
                        }

                        string strSQL = "UPDATE LBTable SET LBName=@title, LBInf=@content, LBHasChild=@hasChild, LBParentID=@parentID, PosIndex=@posIndex WHERE 编号=" + e.CommandArgument;
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        command.Parameters.Add("@title", OleDbType.VarChar).Value = titleTextBox.Text;
                        command.Parameters.Add("@content", OleDbType.VarChar).Value = contentTextBox.Text;
                        command.Parameters.Add("@hasChild", OleDbType.Boolean).Value = hasChildCheckBox.Checked;
                        command.Parameters.Add("@parentID", OleDbType.Integer).Value = parentIDDropDown.SelectedValue;
                        command.Parameters.Add("@posIndex", OleDbType.Integer).Value = index;
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改产品类别,操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("类别信息已修改", "", this);

                            NewsListPanel.Visible = true;
                            EditPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改产品类别时发生错误，返回受影响数据库条数为零。操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("修改类别信息时发生错误", "", this);
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
                        string strSQL = "UPDATE LBTable SET LBDel=true WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除产品类别,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("类别已被删除", "", this);
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除产品类别时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("删除类别时发生错误", "", this);
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
        IndexTextBox.Text = "0";
        hasChildCheckBox.Checked = false;

        NewsListPanel.Visible = false;
        EditPanel.Visible = true;
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
            JScript.Alert("请输入标题", this);
            return;
        }
       
        int index;
        if (!int.TryParse(IndexTextBox.Text, out index))
        {
            index = 100;
            JScript.Alert("显示顺序必须为整数", this);
            return;
        }

        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {

            string strSQL = "INSERT INTO LBTable(LBName,LBInf,LBHasChild,LBParentID,PosIndex) values (@title,@content,@hasChild,@parentID,@posIndex)";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@title", OleDbType.VarChar).Value = titleTextBox.Text;
            command.Parameters.Add("@content", OleDbType.VarChar).Value = contentTextBox.Text;
            command.Parameters.Add("@hasChild", OleDbType.Boolean).Value = hasChildCheckBox.Checked;
            command.Parameters.Add("@parentID", OleDbType.Integer).Value = parentIDDropDown.SelectedValue;
            command.Parameters.Add("@posIndex", OleDbType.Integer).Value = index;
            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加产品类别,标题:" + titleTextBox.Text);
                JScript.AlertAndRedirect("类别已添加", "", this);

                NewsListPanel.Visible = true;
                EditPanel.Visible = false;
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加产品类别时发生错误，返回受影响数据库条数为零。欲添加标题: " + titleTextBox.Text);
                JScript.AlertAndRedirect("添加类别时发生错误", "", this);
            }
        }

    }
    protected void Back_List(object sender, EventArgs e)
    {
        NewsListPanel.Visible = true;
        EditPanel.Visible = false;
    }

}