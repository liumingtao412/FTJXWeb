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
        if (!IsPostBack)
        {


        }
        if (Request.QueryString["wantmd5"] != null)
        {
            wantmd5.Visible = true;
            wantmd5.Text = "MD5:" + MD5Provider.Hash(Request.QueryString["wantmd5"].ToString());
        }
    }

    protected void Btn_Login_Click(object sender, EventArgs e)
    {
        string Out_Error;
        if (MyBasePage.login_check(Txt_UserName.Text, Txt_Password.Text, out Out_Error, this))
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            JScript.Alert("用户名或密码错误", this);
        }

    }
}