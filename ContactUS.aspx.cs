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


public partial class ContactUS : System.Web.UI.Page
{
    public string strContactInf = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (Page.Master.FindControl("ProductsClassTreeView") as TreeView).ExpandDepth = 0;

            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                string _sql = "SELECT * FROM XXTable";
                OleDbDataAdapter da = new OleDbDataAdapter(_sql, objConnection);
                DataSet ds = new DataSet();         //填充DataSet 
                da.Fill(ds, "nb");
                DataTable _dt = ds.Tables["nb"];
                strContactInf = _dt.Rows[0]["LXFS"].ToString();
                SendEmail.strEmailForMessage = _dt.Rows[0]["GSEmail"].ToString().Trim();//获取设定的发送的目标邮箱



                if (Request.QueryString["class"] == "contactinf" || Request.QueryString["class"] == "emap")
                {
                    stitle.Text = "联系方式";
                    borderImg.ImageUrl = "images/b/contactus.jpg";
                    EMapImage.Visible = false;
                    MessagePanel.Visible = false;
                    ContectInfLabel.Text = _dt.Rows[0]["LXFS"].ToString() + @"<br /> <br /> <br />";
                    ContectInfLabel.Visible = true;

                    EMapImage.ImageUrl = "~/Pics/公司地图3.gif";//百度地图
                    EMapImage.Visible = true;
                }
                else
                {
                    stitle.Text = "在线留言";
                    borderImg.ImageUrl = "images/b/feedback.jpg";

                    EMapImage.Visible = false;
                    ContectInfLabel.Visible = false;
                    MessagePanel.Visible = true;

                  
                    if (Request.QueryString["class"]=="AskForProduct")
                    {
                        TitleTextBox.Text = "咨询产品-" + Request.QueryString["productName"];
                        ContentsTextBox.Text = "【请在此描述您的详细问题或需求。为了方便我们及时回答您的咨询，请留下您的电话号码，谢谢！】";
                    }
                }
            }
        }
    }

    /// <summary>
    /// 确定留言按钮，同时发送邮件通知指定邮箱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OKButton_Click(object sender, EventArgs e)
    {
        ValicodeWrongLabel.Visible = false;
        if (IsValid)//判断页面控件是否都通过验证
        {
            if (Session["ValidateCode"] == null || UserCodeTextbox.Text != Session["ValidateCode"].ToString())
            {
                JScript.Alert("验证码输入错误！请重新输入！",  this.Page);
                ValicodeWrongLabel.Visible = true;
                return;
            }              

            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                string strLYTitle = TitleTextBox.Text.Trim();
                string strLYName = NameTextBox.Text.Trim();
                string strLYEmail = EmailTextBox.Text.Trim();
                string strLYTel = TelTextBox.Text.Trim();
                string strLYContents = ContentsTextBox.Text.Trim();

                if (strLYTitle == "")
                {
                    strLYTitle = strLYName + " 的留言";
                }



                string strSQL = "INSERT INTO LYTable(LYTitle,LYName,LYEmail,LYTel,LYContents) VALUES(@lyTitle,@lyName,@lyEmail,@lyTel,@lyContent)";
                OleDbCommand command = new OleDbCommand(strSQL, objConnection);
                command.Parameters.Add("@lyTitle", OleDbType.VarChar).Value = strLYTitle;
                command.Parameters.Add("@lyName", OleDbType.VarChar).Value = strLYName;
                command.Parameters.Add("@lyEmail", OleDbType.VarChar).Value = strLYEmail;
                command.Parameters.Add("@lyTel", OleDbType.VarChar).Value = strLYTel;
                command.Parameters.Add("@lyContent", OleDbType.VarChar).Value = strLYContents;
                if (command.ExecuteNonQuery() == 1)
                {
                    string emailcontents = string.Format(@"一条新留言<br />标题: {0}<br />留言人: {1}<br />Email: {2}<br />电话: {3}<br /><br />内容为: <br />{4}", strLYTitle, strLYName, strLYEmail, strLYTel, strLYContents);
                    if (SendEmail.strEmailForMessage == string.Empty)
                    {
                        SendEmail.strEmailForMessage = "wenxianyang@ftjx.cn";
                    }
                    if (SendEmail.StaticSendEmail(SendEmail.strEmailForMessage, strLYTitle, emailcontents))
                    {
                       // Response.Write("<script language='javascript'>alert('留言发表成功!!'); window.location.href='Default.aspx'</script>");
                        JScript.AlertAndRedirect("你的留言发表成功！我们会尽快联系您，谢谢！", "Default.aspx", this.Page);


                    }
                    else
                    {
                        JScript.AlertAndRedirect("留言失败，请稍候再试！谢谢！", "Default.aspx", this.Page);
                    }
                }
                else
                {
                    JScript.AlertAndRedirect("留言失败，数据库错误！", "Default.aspx", this.Page);
                }



            }
        }
    }



}
