using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ValidateImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckCode vCode = new CheckCode();
        string code = vCode.CreateValidateCode(5);
        Session["ValidateCode"] = code;
        byte[] imageBytes = vCode.CreateValidateGraphic(code);
        Response.BinaryWrite(imageBytes);

    }
}