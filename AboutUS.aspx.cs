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


public partial class AboutUS : System.Web.UI.Page
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
                AboutUsInfLabel.Text = _dt.Rows[0]["GSXJ"].ToString();
                JoinUsLabel.Text = _dt.Rows[0]["ZPXX"].ToString();
                if (Request.QueryString["class"] == "joinus")
                {
                    stitle.Text = "招聘信息";
                    JoinUsLabel.Visible = true;
                    AboutUsInfLabel.Visible = false;
                    bannerImage.ImageUrl = "~/images/b/jobs.jpg";
                }
                else
                {
                    stitle.Text = "公司简介";
                    AboutUsInfLabel.Visible = true;
                    JoinUsLabel.Visible = false;
                    bannerImage.ImageUrl = "~/images/b/aboutus.jpg";

                }
            }
        }
    }
  
}
