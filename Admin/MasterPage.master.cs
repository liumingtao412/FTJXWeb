using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;

public partial class admin_MasterPage : System.Web.UI.MasterPage
{
    public static string left_menu_html = string.Empty;
    public static string right_nav_html = string.Empty;
    public static string pageTitle = "管理后台";
    public static int userAuthority = 0;

    bool isAdmin = false;    

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] == null)
        {
            userAuthority = 0;
            JScript.AlertAndRedirect("尚未登录或登录超时", "login.aspx", this.Page);
            Response.Redirect("login.aspx");
        }
        else
        {
            int.TryParse(Session["userAuthority"].ToString(), out userAuthority);
            if (userAuthority == 5)
            {
                isAdmin = true;
            }
        }



        if (!IsPostBack)
        {

            XmlTextReader reader = new XmlTextReader(MapPath("AdminPages.xml"));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument pageSetting = new XmlDocument();
            try
            {
                pageSetting.Load(reader);
            }
            catch
            {
                Response.Write("配置文件错误，请检查AdminPages.xml文件");
                Response.End();
            }
            reader.Close();
            string thispage = Path.GetFileName(HttpContext.Current.Request.Path).ToLower();
            if (thispage == "deafult.aspx") form1.Visible = true;
            left_menu_html = "";
            foreach (XmlNode adminpage in pageSetting["page"].ChildNodes)
            {
                XmlElement xe = (XmlElement)adminpage;
                int intAuth = 100;
                int.TryParse(xe.GetAttribute("authority"), out intAuth);
                if (userAuthority >= intAuth)
                {
                    if (xe.HasAttribute("url"))
                    {
                        left_menu_html = left_menu_html + "<a href=\"" + xe.GetAttribute("url") + "\" class=\"left_menu_a";
                        if (xe.GetAttribute("url").ToLower() == thispage)
                        {
                            form1.Visible = true;
                            left_menu_html = left_menu_html + " left_menu_hl";
                            right_nav_html = "<span class=\"rn_this\">" + xe.GetAttribute("title") + "</span>";
                            pageTitle = xe.GetAttribute("title");
                        }
                        left_menu_html = left_menu_html + "\">" + xe.GetAttribute("title") + "</a>";
                        
                    }
                    else
                    {
                        left_menu_html = left_menu_html + "<span class=\"left_menu_txt\">" + xe.GetAttribute("title") + "</span>";
                    }
                    left_menu_html = left_menu_html + "<div class=\"lm_line\"></div>";
                    if (adminpage.HasChildNodes)
                    {
                        foreach (XmlNode subpage in adminpage.ChildNodes)
                        {
                            xe = (XmlElement)subpage;
                            intAuth = 100;
                            int.TryParse(xe.GetAttribute("authority"), out intAuth);
                            if (userAuthority >= intAuth)
                            {
                                left_menu_html = left_menu_html + "<a href=\"" + xe.GetAttribute("url") + "\" class=\"left_menu_sub";
                                if (xe.GetAttribute("url").ToLower() == thispage)
                                {
                                    form1.Visible = true;
                                    left_menu_html = left_menu_html + " left_menu_hl";
                                    right_nav_html = "<span class=\"rn_this\">" + xe.GetAttribute("title") + "</span>";
                                    pageTitle = xe.GetAttribute("title");
                                }
                                left_menu_html = left_menu_html + "\">" + xe.GetAttribute("title") + "</a>";


                            }
                        }
                        left_menu_html = left_menu_html + "<div class=\"lm_line\" style=\"margin-top:8px;\"></div>";
                    }
                }

            }
            if (form1.Visible == false)
            {
                Response.Redirect("Default.aspx");
            }

        }
        this.Page.Title = "管理后台 - " + pageTitle;
    }
    protected void logoutButton_Click1(object sender, EventArgs e)
    {
        MyBasePage.logout(this.Page);
        JScript.AlertAndRedirect("您已安全退出管理后台", "../Default.aspx", this.Page);
    }

}
