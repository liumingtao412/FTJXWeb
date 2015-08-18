using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BannerAdmin : System.Web.UI.Page
{
    private string formatPicURL(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return string.Empty;
        }
        if (url.Substring(0, 1) != "/")
        {
            url = "../" + url;
        }
        return url;
    }

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
                        string strSQL = "SELECT * FROM BannerImageTable WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {
                            doUpdateButton.CommandArgument = e.CommandArgument.ToString();
                            doUpdateButton.Visible = true;
                            doInsertButton.Visible = false;

                            productNameTextBox.Text = dt.Rows[0]["imgText"].ToString();
                            picURLTextBox.Text = dt.Rows[0]["imgUrl"].ToString();                            
                            linkURLTextBox.Text = dt.Rows[0]["imgLink"].ToString();
                            posIndexTextBox.Text = dt.Rows[0]["PosIndex"].ToString();
                          


                            Image_preview.ImageUrl = formatPicURL(dt.Rows[0]["imgUrl"].ToString());


                            NewsListPanel.Visible = false;
                            EditPanel.Visible = true;
                            //ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择banner时发生错误,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("载入指定数据时发生错误", "", this);
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
                        if (productNameTextBox.Text == "")
                        {
                            JScript.Alert("请输入Banner名称", this);
                            return;
                        }
                        if (picURLTextBox.Text == "")
                        {
                            JScript.Alert("请输入Banner图片地址", this);
                            return;
                        }
                        int nindex;
                        if (!int.TryParse(posIndexTextBox.Text, out nindex))
                        {
                            JScript.Alert("显示顺序必须为整数", this);
                            return;
                        }


                        string strSQL = "UPDATE BannerImageTable SET imgText=@name, imgUrl=@picurl, imgLink=@linkURL, PosIndex=@posIndex WHERE 编号=" + e.CommandArgument;
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        command.Parameters.Add("@name", OleDbType.VarChar).Value = productNameTextBox.Text;
                        command.Parameters.Add("@picurl", OleDbType.VarChar).Value = picURLTextBox.Text;
                        command.Parameters.Add("@linkURL", OleDbType.VarChar).Value = linkURLTextBox.Text;
                        command.Parameters.Add("@posIndex", OleDbType.Integer).Value = nindex;

                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改Banner图片,操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("记录已修改", "", this);

                            NewsListPanel.Visible = true;
                            EditPanel.Visible = false;
                            //ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改banner时发生错误，返回受影响数据库条数为零。操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("修改数据库记录时发生错误", "", this);
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
                        string strSQL = "UPDATE BannerImageTable SET IsDel=true WHERE 编号=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除Banner,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("该Banner已删除", "", this);
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除Banner时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("删除Banner时发生错误", "", this);
                        }
                        //string strSQL = "DELETE FROM BannerImageTable WHERE 编号=" + e.CommandArgument.ToString();
                        //OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        //if (command.ExecuteNonQuery() > 0)
                        //{
                        //    MyBasePage.writeLog(Session["userName"].ToString(), "删除banner图片,操作对象ID:" + e.CommandArgument.ToString());
                        //    JScript.AlertAndRedirect("记录已删除", "", this);
                        //}
                        //else
                        //{
                        //    MyBasePage.writeLog(Session["userName"].ToString(), "删除banner时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                        //    JScript.AlertAndRedirect("删除数据库记录时发生错误", "", this);
                        //}
                    }
                    else
                    {
                        JScript.Alert("您无权进行此操作", this);
                    }

                    break;
                case "doView":
                    break;
            }
        }
    }
    protected void Article_New(object sender, EventArgs e)
    {
        doUpdateButton.Visible = false;
        doInsertButton.Visible = true;

        productNameTextBox.Text = "";
        picURLTextBox.Text = "";
        linkURLTextBox.Text = "";
        posIndexTextBox.Text = "0";


        NewsListPanel.Visible = false;
        EditPanel.Visible = true;
        //ViewPanel.Visible = false;
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
        if (productNameTextBox.Text == "")
        {
            JScript.Alert("请输入Banner名称", this);
            return;
        }
        if (picURLTextBox.Text == "")
        {
            JScript.Alert("请输入Banner图片地址", this);
            return;
        }
        int nindex;
        if (!int.TryParse(posIndexTextBox.Text, out nindex))
        {
            JScript.Alert("显示顺序必须为整数", this);
            return;
        }

        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {

            string strSQL = "INSERT INTO BannerImageTable(imgText,imgUrl,imgLink,PosIndex) values (@name,@picurl,@linkURL,@posIndex)";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@name", OleDbType.VarChar).Value = productNameTextBox.Text;
            command.Parameters.Add("@picurl", OleDbType.VarChar).Value = picURLTextBox.Text;
            command.Parameters.Add("@linkURL", OleDbType.VarChar).Value = linkURLTextBox.Text;
            command.Parameters.Add("@posIndex", OleDbType.Integer).Value = nindex;
            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加Banner,标题:" + productNameTextBox.Text);
                JScript.AlertAndRedirect("Banner已添加", "", this);

                NewsListPanel.Visible = true;
                EditPanel.Visible = false;
                //ViewPanel.Visible = false;
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加Banner时发生错误，返回受影响数据库条数为零。欲添加标题: " + productNameTextBox.Text);
                JScript.AlertAndRedirect("添加数据库记录时发生错误", "", this);
            }
        }

    }
    protected void Back_List(object sender, EventArgs e)
    {
        NewsListPanel.Visible = true;
        EditPanel.Visible = false;
        //ViewPanel.Visible = false;
    }

    protected void Btn_RefreshPreview_Click(object sender, EventArgs e)
    {
        Image_preview.ImageUrl = formatPicURL(picURLTextBox.Text);
    }
}