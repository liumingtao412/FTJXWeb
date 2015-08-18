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

public partial class ProductsDetails : System.Web.UI.Page
{
    public string strContactInf = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
        if (Request.QueryString["CPID"] != null)
        {
            int CPID =0;
            if (!int.TryParse(Request.QueryString["CPID"], out CPID))
            {
                Response.Redirect("Default.aspx", true);
            }
            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                string _sql = "SELECT * FROM CPTable LEFT JOIN LBTable ON CPTable.CPLBID=LBTable.编号 WHERE CPDel = False AND CPTable.编号=" + CPID.ToString();
                OleDbDataAdapter CPda = new OleDbDataAdapter(_sql, objConnection);
                DataSet CPds = new DataSet();         //填充DataSet 
                CPda.Fill(CPds, "CP");
                DataTable _CPdt = CPds.Tables["CP"];
                CPDetailsLabel.Text = _CPdt.Rows[0]["CPDetails"].ToString();
                CPLBLabel.Text = _CPdt.Rows[0]["LBName"].ToString();
                CPNameLabel.Text = _CPdt.Rows[0]["CPName"].ToString();
            }
        }



    }

    protected void AskForButton_Click(object sender, EventArgs e)
    {
        string url = string.Format(@"~/ContactUS.aspx?class=AskForProduct&productName={0}",CPNameLabel.Text);
        Response.Redirect(url, true);
    }
}
