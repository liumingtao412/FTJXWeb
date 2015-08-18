using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

public partial class Admin_XX : System.Web.UI.Page
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
                string strSql = "SELECT TOP 1 * FROM XXTable";
                OleDbDataAdapter daForRepeator = new OleDbDataAdapter(strSql, objConnection);
                DataTable dtForRepeator = new DataTable();
                daForRepeator.Fill(dtForRepeator);
                GSJJTextBox.Text = dtForRepeator.Rows[0]["GSJJ"].ToString();
                LXFSTextBox.Text = dtForRepeator.Rows[0]["LXFS"].ToString();
                ZPXXTextBox.Text = dtForRepeator.Rows[0]["ZPXX"].ToString();
                GSXJTextBox.Text = dtForRepeator.Rows[0]["GSXJ"].ToString();
                GSEMAILTextBox.Text = dtForRepeator.Rows[0]["GSEmail"].ToString();
                isShowBannerCheckBox.Checked = Convert.ToBoolean(dtForRepeator.Rows[0]["IsShowBannerImage"]);
                IsScrollStarProductsCheckBox.Checked = Convert.ToBoolean(dtForRepeator.Rows[0]["IsScrollStarProducts"]);
                ScrollSpeedTextBox.Text = dtForRepeator.Rows[0]["ScrollSpeed"].ToString();
                ScrollPicNumTextBox.Text = dtForRepeator.Rows[0]["ScrollPicNum"].ToString();

                //ZCLXFSTextBox.Text = dtForRepeator.Rows[0]["ZCLXFS"].ToString();
            }
        }
    }
    protected void saveButton_Click(object sender, EventArgs e)
    {
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string strSql = @"UPDATE XXTable SET GSJJ=@GSJJ, LXFS=@LXFS, ZPXX=@ZPXX, GSXJ=@GSXJ, 
            GSEmail=@GSEmail, IsShowBannerImage=@IsShowBannerImage,
            IsScrollStarProducts=@IsScrollStarProducts,
            ScrollSpeed=@ScrollSpeed,
            ScrollPicNum=@ScrollPicNum
            ";
            OleDbCommand command = new OleDbCommand(strSql, objConnection);
            command.Parameters.Add("@GSJJ", OleDbType.VarChar).Value = GSJJTextBox.Text;
            command.Parameters.Add("@LXFS", OleDbType.VarChar).Value = LXFSTextBox.Text;
            command.Parameters.Add("@ZPXX", OleDbType.VarChar).Value = ZPXXTextBox.Text;
            command.Parameters.Add("@GSXJ", OleDbType.VarChar).Value = GSXJTextBox.Text;
            command.Parameters.Add("@GSEmail", OleDbType.VarChar).Value = GSEMAILTextBox.Text;
            command.Parameters.Add("@IsShowBannerImage", OleDbType.Boolean).Value = isShowBannerCheckBox.Checked;
            command.Parameters.Add("@IsScrollStarProducts", OleDbType.Boolean).Value = IsScrollStarProductsCheckBox.Checked;
            command.Parameters.Add("@ScrollSpeed", OleDbType.VarChar).Value = ScrollSpeedTextBox.Text;
            command.Parameters.Add("@ScrollPicNum", OleDbType.VarChar).Value = ScrollPicNumTextBox.Text;
            //command.Parameters.Add("@ZCLXFS", OleDbType.VarChar).Value = ZCLXFSTextBox.Text;
            if (command.ExecuteNonQuery() > 0)
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "修改系统设置");
                JScript.AlertAndRedirect("系统设置已修改","",this);
            }
            else
            {
                MyBasePage.writeLog(Session["userName"].ToString(), "修改系统设置时发生错误，返回受影响数据库条数为零。");
                JScript.AlertAndRedirect("系统设置修改失败", "", this);
            }

        }
    }
    protected void reloadButton_Click(object sender, EventArgs e)
    {
        JScript.RefreshOpener(this);
    }

}