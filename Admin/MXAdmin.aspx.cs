using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MXAdmin : System.Web.UI.Page
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
                        string strSQL = "SELECT * FROM ProductsShowTable WHERE CPID=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count >= 1)
                        {
                            doUpdateButton.CommandArgument = e.CommandArgument.ToString();
                            doUpdateButton.Visible = true;
                            doInsertButton.Visible = false;

                            productNameTextBox.Text = dt.Rows[0]["ProductName"].ToString();
                            picURLTextBox.Text = dt.Rows[0]["PicURL"].ToString();
                            remarksTextBox.Text = dt.Rows[0]["Remarks"].ToString();
                            linkURLTextBox.Text = dt.Rows[0]["LinkURL"].ToString();
                            posIndexTextBox.Text = dt.Rows[0]["PosIndex"].ToString();
                            altTextBox.Text = dt.Rows[0]["AltValue"].ToString();


                            Image_preview.ImageUrl = formatPicURL(dt.Rows[0]["PicURL"].ToString());
                          

                            NewsListPanel.Visible = false;
                            EditPanel.Visible = true;
                            //ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "选择指定明星产品时发生错误,操作对象ID:" + e.CommandArgument.ToString());
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
                            JScript.Alert("请输入明星产品名称", this);
                            return;
                        }
                        if (picURLTextBox.Text == "")
                        {
                            JScript.Alert("请输入产品图片地址", this);
                            return;
                        }
                        int nindex;
                        if (!int.TryParse(posIndexTextBox.Text, out nindex))
                        {
                            JScript.Alert("显示顺序必须为整数", this);
                            return;
                        }
                        

                        string strSQL = "UPDATE ProductsShowTable SET ProductName=@name, PicURL=@picurl, Remarks=@remarks, LinkURL=@linkURL, PosIndex=@posIndex, AltValue=@altValue WHERE CPID=" + e.CommandArgument;
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        command.Parameters.Add("@name", OleDbType.VarChar).Value = productNameTextBox.Text;
                        command.Parameters.Add("@picurl", OleDbType.VarChar).Value = picURLTextBox.Text;
                        command.Parameters.Add("@remarks", OleDbType.VarChar).Value = remarksTextBox.Text;
                        command.Parameters.Add("@linkURL", OleDbType.VarChar).Value = linkURLTextBox.Text;
                        command.Parameters.Add("@posIndex", OleDbType.Integer).Value = nindex;
                        command.Parameters.Add("@altValue", OleDbType.VarChar).Value = altTextBox.Text.Trim();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改明星产品,操作对象ID:" + e.CommandArgument);
                            JScript.AlertAndRedirect("记录已修改", "", this);

                            NewsListPanel.Visible = true;
                            EditPanel.Visible = false;
                            //ViewPanel.Visible = false;
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "修改明星产品时发生错误，返回受影响数据库条数为零。操作对象ID:" + e.CommandArgument);
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
                        string strSQL = "UPDATE ProductsShowTable SET IsDel=true WHERE CPID=" + e.CommandArgument.ToString();
                        OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除明星产品,操作对象ID:" + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("该明星产品已删除", "", this);
                        }
                        else
                        {
                            MyBasePage.writeLog(Session["userName"].ToString(), "删除明星产品时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
                            JScript.AlertAndRedirect("删除明星产品时发生错误", "", this);
                        }
                        //string strSQL = "DELETE FROM ProductsShowTable WHERE CPID=" + e.CommandArgument.ToString();
                        //OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                        //if (command.ExecuteNonQuery() > 0)
                        //{
                        //    MyBasePage.writeLog(Session["userName"].ToString(), "删除明星产品,操作对象ID:" + e.CommandArgument.ToString());
                        //    JScript.AlertAndRedirect("记录已删除", "", this);
                        //}
                        //else
                        //{
                        //    MyBasePage.writeLog(Session["userName"].ToString(), "删除明星产品时发生错误，返回受影响数据库条数为零。欲操作对象ID: " + e.CommandArgument.ToString());
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
        remarksTextBox.Text = "";
        linkURLTextBox.Text = "";
        posIndexTextBox.Text = "0";
        altTextBox.Text = "";
       

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
            JScript.Alert("请输入明星产品名称", this);
            return;
        }
        if (picURLTextBox.Text == "")
        {
            JScript.Alert("请输入产品图片地址", this);
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

            string strSQL = "INSERT INTO ProductsShowTable(ProductName,PicURL,Remarks,LinkURL,PosIndex,AltValue) values (@name,@picurl,@remarks,@linkURL,@posIndex,@altValue)";
            OleDbCommand command = new OleDbCommand(strSQL, objConnection);
            command.Parameters.Add("@name", OleDbType.VarChar).Value = productNameTextBox.Text;
            command.Parameters.Add("@picurl", OleDbType.VarChar).Value = picURLTextBox.Text;
            command.Parameters.Add("@remarks", OleDbType.VarChar).Value = remarksTextBox.Text;
            command.Parameters.Add("@linkURL", OleDbType.VarChar).Value = linkURLTextBox.Text;
            command.Parameters.Add("@posIndex", OleDbType.Integer).Value = nindex;
            command.Parameters.Add("@altValue", OleDbType.VarChar).Value = altTextBox.Text;
            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加明星产品,标题:" + productNameTextBox.Text);
                JScript.AlertAndRedirect("记录已添加", "", this);

                NewsListPanel.Visible = true;
                EditPanel.Visible = false;
                //ViewPanel.Visible = false;
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "添加明星产品时发生错误，返回受影响数据库条数为零。欲添加标题: " + productNameTextBox.Text);
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