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

public partial class Template : System.Web.UI.MasterPage
{
    static public string productsClassList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            productsClassList = "";
            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                string _sql = "SELECT * FROM LBTable WHERE LBDel=False ORDER BY PosIndex DESC";
                OleDbDataAdapter LBda = new OleDbDataAdapter(_sql, objConnection);
                DataSet ds = new DataSet();
                LBda.Fill(ds, "lb");
                DataTable categoryTable = ds.Tables["lb"];
                DataRow[] drs = categoryTable.Select("LBParentID=-1");
                if (drs != null && drs.Length > 0)
                {
                     //获取根节点   
                    foreach (DataRow dr in drs)
                    {
                        TreeNode rootNode = new TreeNode();
                        rootNode.Text = (string)dr["LBName"];

                        rootNode.NavigateUrl = String.Format("Products.aspx?LBID={0}", dr["编号"].ToString());

                        rootNode.Expanded = false;//决定默认时，树节点是否展开
                        //第归实例化TreeView   
                        InitTreeView((int)dr["编号"], categoryTable, rootNode);
                        ProductsClassTreeView.Nodes.Add(rootNode);
                    }
                    //foreach (DataRow dr in drs)
                    //{
                    //    string tmpHtml = "";
                    //    tmpHtml = createChildClassList((int)dr["编号"], categoryTable);
                    //    if (tmpHtml == "")
                    //    {
                    //        productsClassList += "<p><a href=\"" + String.Format("Products.aspx?LBID={0}", dr["编号"].ToString()) + "\">" + (string)dr["LBName"] + "</a></p>";
                    //    }
                    //    else
                    //    {
                    //        productsClassList += "<p style=\"color:#666\">" + (string)dr["LBName"] + "</p>";
                    //    }
                    //    productsClassList += tmpHtml;
                    //    productsClassList += "<div class=\"line\"></div>";
                    //}
                }
            }
        }

    }
    private string createChildClassList(int parentID, DataTable dt)
    {
        string rtnString = "";
        DataRow[] drs = dt.Select("LBParentID=" + parentID);
        foreach (DataRow dr in drs)
        {
            rtnString += "<p><a href=\"" + String.Format("Products.aspx?LBID={0}", dr["编号"].ToString()) + "\">" + (string)dr["LBName"] + "</a></p>";
        }
        return rtnString;
    }
      protected void InitTreeView(int parentID, DataTable dt, TreeNode parentNode)
    {
        DataRow[] drs = dt.Select("LBParentID=" + parentID);
        foreach (DataRow dr in drs)
        {
            TreeNode currentNode = new TreeNode();
            currentNode.Text = (string)dr["LBName"];
            //currentNode.ImageUrl = "Images/TreeView_folder.gif";
            currentNode.NavigateUrl = String.Format("Products.aspx?LBID={0}", dr["编号"].ToString());
            //currentNode.Expanded = true;
            InitTreeView((int)dr["编号"], dt, currentNode);
            parentNode.ChildNodes.Add(currentNode);

        }
    }
}
