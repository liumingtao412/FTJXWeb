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

public partial class News : System.Web.UI.Page
{
    public string strContactInf = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //设置产品列表树 默认不展开
            (Page.Master.FindControl("ProductsClassTreeView") as TreeView).ExpandDepth = 0;

            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                #region 读取公司信息表

                string _sql = "SELECT * FROM XXTable";
                OleDbDataAdapter da = new OleDbDataAdapter(_sql, objConnection);
                DataSet ds = new DataSet();         //填充DataSet 
                da.Fill(ds, "nb");
                DataTable _dt = ds.Tables["nb"];
                strContactInf = _dt.Rows[0]["LXFS"].ToString(); 
                #endregion
                #region 分页读取新闻

                //_sql = "SELECT * FROM XWTable WHERE newsDelete=false  ORDER BY isTop, newsTime DESC ";
                //OleDbDataAdapter daForRepeator = new OleDbDataAdapter(_sql, objConnection);
                //DataSet dsForRepeator = new DataSet();
                //daForRepeator.Fill(dsForRepeator, "news");

                //DataTable dtForRepeator = dsForRepeator.Tables["news"];

                //NewsRepeater.DataSource = dtForRepeator;
                //NewsRepeater.DataBind(); 
                RepeaterPaging();
                #endregion
            }
        }
    }
    protected void NewsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void FirstLinkButton_Click(object sender, EventArgs e)
    {
        CurrentPageLabel.Text = "1";
        RepeaterPaging();
    }
    protected void NextLinkButton_Click(object sender, EventArgs e)
    {
        CurrentPageLabel.Text = (Convert.ToInt32(CurrentPageLabel.Text) + 1).ToString();
        RepeaterPaging();
    }
    protected void PreLinkButton_Click(object sender, EventArgs e)
    {
        CurrentPageLabel.Text = (Convert.ToInt32(CurrentPageLabel.Text) - 1).ToString();
        RepeaterPaging();
    }
    protected void LastLinkButton_Click(object sender, EventArgs e)
    {
        CurrentPageLabel.Text = TotalPageLabel.Text;
        RepeaterPaging();
    }

    protected void DropDownListPageNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPageLabel.Text = DropDownListPageNum.SelectedValue;
        RepeaterPaging();
    }
    /// <summary>
    /// 分页读取新闻，默认12条分页，按  是否置顶  和 时间  排序，读取前120条
    /// </summary>    
    protected void RepeaterPaging()
    {
       
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        string strSQL = "SELECT TOP 120 * FROM XWTable WHERE newsDelete=false ORDER BY isTop, newsTime DESC ";
        OleDbDataAdapter daForRepeator = new OleDbDataAdapter(strSQL, objConnection);
        DataSet dsForRepeator = new DataSet();
        daForRepeator.Fill(dsForRepeator, "news");

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dsForRepeator.Tables["news"].DefaultView;
        pds.AllowPaging = true;
        pds.PageSize = 12;
        pds.CurrentPageIndex = Convert.ToInt32(this.CurrentPageLabel.Text) - 1;


        TotalPageLabel.Text = pds.PageCount.ToString();
        CurrentPageLabel.Text = (pds.CurrentPageIndex + 1).ToString();
        FirstLinkButton.Enabled = true;
        LastLinkButton.Enabled = true;
        NextLinkButton.Enabled = true;
        PreLinkButton.Enabled = true;
        DropDownListPageNum.Items.Clear();
        for (int i = 1; i < pds.PageCount + 1; ++i)
        {
            ListItem li = new ListItem(i.ToString(), i.ToString());
            DropDownListPageNum.Items.Add(li);
        }
        DropDownListPageNum.SelectedValue = CurrentPageLabel.Text;
        if (pds.CurrentPageIndex < 1)
        {
            FirstLinkButton.Enabled = false;
            PreLinkButton.Enabled = false;
        }
        if (pds.CurrentPageIndex == pds.PageCount - 1)
        {
            LastLinkButton.Enabled = false;
            NextLinkButton.Enabled = false;
        }

        NewsRepeater.DataSource = pds;

        NewsRepeater.DataBind();

        objConnection.Close();
    }
}
