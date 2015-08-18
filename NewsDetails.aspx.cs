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


public partial class NewsDetails : System.Web.UI.Page
{
    int NewsID = 0;
    public string strNewsContents = string.Empty;
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
                if (Request.QueryString["NewsID"].Length != 0)
                {
                    NewsID = int.Parse(Request.QueryString["NewsID"]);
                    //OleDbConnection objConnection = new OleDbConnection(strConnection);
                    //objConnection.Open();

                    _sql = "SELECT * FROM XWTable WHERE 编号=" + NewsID.ToString();
                    da = new OleDbDataAdapter(_sql, objConnection);
                    // DataSet ds = new DataSet();
                    da.Fill(ds, "news");
                    DataTable dt = ds.Tables["news"];
                    Page.Title = NewsTitleLabel.Text = dt.Rows[0]["newsTitle"].ToString();
                    NewsTimeLabel.Text = dt.Rows[0]["newsTime"].ToString();
                    strNewsContents = dt.Rows[0]["newsContents"].ToString();
                    NewsClickTimesLabel.Text = dt.Rows[0]["newsClickTimes"].ToString();

                    _sql = string.Format("UPDATE XWTable SET newsClickTimes ={0} WHERE 编号={1}", int.Parse(NewsClickTimesLabel.Text) + 1, NewsID);

                    OleDbCommand updateCmd = new OleDbCommand(_sql, objConnection);
                    updateCmd.ExecuteNonQuery();
                    //TODO:浏览次数有点问题，最好的解决方案是把数据库中，新建新闻时，浏览次数默认为1，现在是默认为0.

                }
            }
        }
    }
}
