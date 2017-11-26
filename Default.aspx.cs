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
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class _Default : System.Web.UI.Page
{
    #region 和BannerImage设置相关代码
    //表示是否要显示banner图片，默认不显示
    public bool IsShowBannerImage = false;
    private class BannerImageInfor
    {
        string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
        string _imageLink;

        public string ImageLink
        {
            get { return _imageLink; }
            set { _imageLink = value; }
        }
        string _imageText;

        public string ImageText
        {
            get { return _imageText; }
            set { _imageText = value; }
        }



    }
    private List<BannerImageInfor> BannerImageInfors = new List<BannerImageInfor>();
    /// <summary>
    /// 测试时用，同时当数据库中bannerImage为空时，作为默认值返回
    /// </summary>
    /// <returns></returns>
    public string GetBannerImageInforTest()
    {
        List<string> bannerImageUrls = new List<string>();
        bannerImageUrls.Add("images/f/1.jpg");
        bannerImageUrls.Add("images/f/2.jpg");
        bannerImageUrls.Add("images/f/3.jpg");
        bannerImageUrls.Add("images/f/4.jpg");

        return @"[{""ImageUrl"":""images/f/1.jpg"",""ImageLink"":"""",""ImageText"":""""},{""ImageUrl"":""images/f/2.jpg"",""ImageLink"":"""",""ImageText"":""""},{""ImageUrl"":""images/f/3.jpg"",""ImageLink"":"""",""ImageText"":""""},{""ImageUrl"":""images/f/4.jpg"",""ImageLink"":"""",""ImageText"":""""}]";
    }
    /// <summary>
    /// 供前端调用， 将获取的BannerImage信息转换为JSON字符串
    /// </summary>
    /// <returns></returns>
    public string GetBannerImageInfor()
    {

        if (BannerImageInfors.Count == 0)//如果数据库中获取的信息为空，则返回测试时候的默认值
        {
            return GetBannerImageInforTest();
        }
        string json = JsonConvert.SerializeObject(BannerImageInfors, Formatting.None);
        return json;
    } 
    #endregion

    public bool IsScrollStarProducts = true;
    public int ScrollSpeed = 2;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (Request.QueryString["action"] == "quitadmin")
            {
                if (Session["AdminName"] != null)
                {

                    Session["AdminName"] = null;
                }
            }

            (Page.Master.FindControl("ProductsClassTreeView") as TreeView).ExpandDepth = 0;


            string strConnection = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
            OleDbConnection objConnection = new OleDbConnection(strConnection);
            objConnection.Open();
            using (objConnection)
            {
                #region 读取公司信息表
                string _sql = "SELECT TOP 1 LXFS,GSJJ,IsShowBannerImage,IsScrollStarProducts,ScrollSpeed,ScrollPicNum FROM XXTable";
                OleDbDataAdapter da = new OleDbDataAdapter(_sql, objConnection);
                DataSet ds = new DataSet();         //填充DataSet 
                da.Fill(ds, "nb");
                DataTable _dt = ds.Tables["nb"];
                ContactInf.Text = _dt.Rows[0]["LXFS"].ToString();
                AboutUSLiteral.Text = _dt.Rows[0]["GSJJ"].ToString();
                //下面读取设置信息
                IsShowBannerImage = bool.Parse(_dt.Rows[0]["IsShowBannerImage"].ToString());
                IsScrollStarProducts = Convert.ToBoolean(_dt.Rows[0]["IsScrollStarProducts"]);
                ScrollSpeed = Convert.ToInt32(_dt.Rows[0]["ScrollSpeed"]);
                int ScrollPicNum = Convert.ToInt32(_dt.Rows[0]["ScrollPicNum"]);
                #endregion

                #region 读取最新的4条新闻
                _sql = "SELECT TOP 4 * FROM XWTable WHERE newsDelete=false AND isPublish=true ORDER BY isTop, newsTime DESC";
                OleDbDataAdapter daForRepeator = new OleDbDataAdapter(_sql, objConnection);
                DataTable dtForRepeator = new DataTable();
                daForRepeator.Fill(dtForRepeator);
                NewsRepeater.DataSource = dtForRepeator;
                NewsRepeater.DataBind();
                #endregion

                #region 读取明星产品
                if (IsScrollStarProducts)
                {
                    _sql = string.Format( "SELECT TOP {0} * FROM ProductsShowTable WHERE IsDel=false ORDER BY PosIndex DESC", ScrollPicNum);
                }
                else
                {
                    _sql = "SELECT TOP 4 * FROM ProductsShowTable WHERE IsDel=false ORDER BY PosIndex DESC";
                }               
                daForRepeator = new OleDbDataAdapter(_sql, objConnection);
                DataTable dtForStarProRepeator = new DataTable();
                daForRepeator.Fill(dtForStarProRepeator);
                StarProductsRepeater.DataSource = dtForStarProRepeator;
                StarProductsRepeater.DataBind();
                
                #endregion

                #region 读取BannerImage信息
                //读取页面BannerImg的信息
                _sql = "SELECT TOP 6 * FROM BannerImageTable WHERE IsDel=false ORDER BY PosIndex DESC";
                daForRepeator = new OleDbDataAdapter(_sql, objConnection);
                dtForStarProRepeator = new DataTable();
                daForRepeator.Fill(dtForStarProRepeator);
                foreach (DataRow item in dtForStarProRepeator.Rows)
                {
                    BannerImageInfor newImageInfor = new BannerImageInfor();
                    newImageInfor.ImageUrl = item["imgUrl"].ToString();
                    newImageInfor.ImageLink = item["imgLink"].ToString();
                    newImageInfor.ImageText = item["imgText"].ToString();
                    BannerImageInfors.Add(newImageInfor);

                } 
                #endregion



            }
        }
    }
}
