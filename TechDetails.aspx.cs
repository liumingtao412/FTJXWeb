using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TechDetails : System.Web.UI.Page
{
    int TechArticleID = 0;
    public string strArticleContents = string.Empty;
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
                if (Request.QueryString["articleID"].Length != 0)
                {
                    TechArticleID = int.Parse(Request.QueryString["articleID"]);
                    //OleDbConnection objConnection = new OleDbConnection(strConnection);
                    //objConnection.Open();

                    _sql = "SELECT * FROM JSZCTable WHERE 编号=" + TechArticleID.ToString();
                    da = new OleDbDataAdapter(_sql, objConnection);
                    // DataSet ds = new DataSet();
                    da.Fill(ds, "article");
                    DataTable dt = ds.Tables["article"];
                    Page.Title = ArticleTitleLabel.Text = dt.Rows[0]["articleTitle"].ToString();
                    ArticleTimeLabel.Text = dt.Rows[0]["articleTime"].ToString();
                    strArticleContents = dt.Rows[0]["articleContents"].ToString();
                    ArticleClickTimesLabel.Text = dt.Rows[0]["articleClickTimes"].ToString();

                    _sql = string.Format("UPDATE JSZCTable SET articleClickTimes ={0} WHERE 编号={1}", int.Parse(ArticleClickTimesLabel.Text) + 1, TechArticleID);

                    OleDbCommand updateCmd = new OleDbCommand(_sql, objConnection);
                    updateCmd.ExecuteNonQuery();                    

                }
            }
        }

    }
}