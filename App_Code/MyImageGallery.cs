using System;
using FreeTextBoxControls;

namespace MyImageControl
{


    /// <summary>
    /// MyImageGallery 的摘要说明
    /// </summary>
    public class MyImageGallery : ImageGallery
    {
        public MyImageGallery()
        {
            //
           
            //
        }
        public override void RaisePostBackEvent(string eventArgument)
        {
            char[] chArray1 = new char[] { ':' };
            string[] textArray1 = eventArgument.Split(chArray1);
            if (textArray1[0] != null && textArray1[0] == "UploadImage")
            {
                this.EnsureChildControls();
                if (this.inputFile.PostedFile != null && this.inputFile.PostedFile.FileName != null && !(IsAcceptedFileTypes(this.inputFile.PostedFile.FileName)))
                {
                    this.returnMessage = "不允许上传该类型的文件";
                    return;
                }
            }
            base.RaisePostBackEvent(eventArgument);
        }

        private bool IsAcceptedFileTypes(string fileName)
        {
            for (int i = 0; i < this.AcceptedFileTypes.Length; i++)
            {
                if (fileName.ToLower().EndsWith("." + this.AcceptedFileTypes[i]))
                {
                    return true;
                }
            }
            return false;
        }
        

        

    }

}