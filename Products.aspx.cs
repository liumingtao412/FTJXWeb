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
using System.Text.RegularExpressions;

public partial class Products : System.Web.UI.Page
{
    public string strLBName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (Page.Master.FindControl("ProductsClassTreeView") as TreeView).ExpandAll();
            RepeaterPaging();            

        }


    }
    protected void RepeaterPaging()
    {
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
        using (objConnection)
        {
            string _sql = "";
            int LBID = 0;
            OleDbDataAdapter da = null;
            DataTable _dt = null;
            if (Request.QueryString["LBID"] != null)//查看某一类别的所有产品
            {
                if (!int.TryParse(Request.QueryString["LBID"], out LBID))
                {
                    Response.Redirect("Default.aspx", true);
                }
                _sql = "SELECT * FROM LBTable WHERE LBDel=False AND 编号=" + LBID.ToString();
               
                da = new OleDbDataAdapter(_sql, objConnection);
                DataSet ds = new DataSet();         //填充DataSet 
                da.Fill(ds, "lb");
                _dt = ds.Tables["lb"];
                // LBInfLabel.Text = _dt.Rows[0]["LBInf"].ToString();
                //SearchPanel.Visible = false;
                ProductTreePanel.Visible = false;
                PagingPanel.Visible = true;
                strLBName = _dt.Rows[0]["LBName"].ToString();
                //如果没有子类别，则构造直接读出此类别的所有产品的SQL语句
                if (_dt.Rows[0]["LBHasChild"].ToString().ToLower() == "false")
                {
                    string strSQL = "SELECT * FROM CPTable WHERE CPDel=False AND CPLBID=" + LBID.ToString();
                     RepeaterPaging1(strSQL);

                }
                else//如果有子类别，则先读出所有子类别的编号,构成IN的SQL语句，将所有子类别的产品读出
                {
                    _sql = "SELECT 编号 FROM LBTable WHERE  LBDel=False AND LBParentID =" + LBID.ToString();
                    da = new OleDbDataAdapter(_sql, objConnection);
                    da.Fill(_dt);
                    string SubIDs = "(";

                    foreach (DataRow item in _dt.Rows)
                    {
                        SubIDs += item["编号"].ToString()+",";
                    }
                    SubIDs.TrimEnd(',');
                    SubIDs += ")";
                    string strSQL = "SELECT * FROM CPTable WHERE CPDel=False AND CPLBID IN " + SubIDs;
                    RepeaterPaging1(strSQL);

                }

            }
            else if (!string.IsNullOrEmpty(Request.QueryString["SearchText"]))
            {
                string searchText = Request.QueryString["SearchText"];
                searchText = Regex.Replace(searchText, " <.+?> ", " ");
                searchText = Regex.Replace(searchText, " <br> ", " ", RegexOptions.IgnoreCase);                
                SearchTextbox.Text = searchText;
                ProductTreePanel.Visible = false;
                _sql = string.Format("SELECT * FROM CPTable WHERE CPDel=False AND CPName like '%{0}%'", searchText);
                RepeaterPaging1(_sql);

            }
            else//查看所有产品，此时显示提示，请首先选择一个分类，或者使用搜索
            {
                //_sql = "SELECT TOP 12 * FROM CPTable WHERE CPDel=false ORDER BY 编号 DESC";
                //OleDbDataAdapter daForRepeator = new OleDbDataAdapter(_sql, objConnection);
                //DataSet dsForRepeator = new DataSet();
                //daForRepeator.Fill(dsForRepeator, "products");

                //DataTable dtForRepeator = dsForRepeator.Tables["products"];

                //ProductsRepeater.DataSource = dtForRepeator;
                //ProductsRepeater.DataBind();
             

                SearchPanel.Visible = true;
                PagingPanel.Visible = false;

            }
        }


    }
    /// <summary>
    /// 分页读取，使用SQL语句读取数据，并分页，默认12条分页
    /// </summary>
    /// <param name="strSQL">sql语句</param>
    protected void RepeaterPaging1(string strSQL)
    {
       // int nCPLBID = nLBID;
        string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        OleDbConnection objConnection = new OleDbConnection(strConnection);
        objConnection.Open();
      //  string strSQL = "SELECT * FROM CPTable WHERE CPLBID=" + nCPLBID.ToString();
        OleDbDataAdapter daForRepeator = new OleDbDataAdapter(strSQL, objConnection);
        DataSet dsForRepeator = new DataSet();
        daForRepeator.Fill(dsForRepeator, "products");

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dsForRepeator.Tables["products"].DefaultView;
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
        for (int i = 1; i < pds.PageCount+1; ++i)
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

        ProductsRepeater.DataSource = pds;

        ProductsRepeater.DataBind();

        objConnection.Close();
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
    protected void searchButton_Click(object sender, EventArgs e)
    {
        string searchText = Regex.Replace(SearchTextbox.Text.Trim(), " <.+?> ", " ");
        searchText = Regex.Replace(searchText, " <br> ", " ", RegexOptions.IgnoreCase);        
        Response.Redirect("~/Products.aspx?SearchText=" + searchText);
    }
}
