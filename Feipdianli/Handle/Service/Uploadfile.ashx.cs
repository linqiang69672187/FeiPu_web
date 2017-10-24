using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// Uploadfile 的摘要说明
    /// </summary>
    public class Uploadfile : IHttpHandler
    {


        //文件大小 1024 kb  
        private long size = 1024;
        //文件类型  
        private string type = ".jpg|.jpeg|.png|.gif|.bmp";
        //保存名称  
        string name = "";
        //保存路径  
        private string path = @"Upload/UserImg";
        //保存大小  
        private string shape = "100*100";  
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpFileCollection files = context.Request.Files;

            uploadMethod(files, context);  


        }

        /// <summary>  
        /// 上传图片  
        /// </summary>  
        /// <param name="hc"></param>  
        public void uploadMethod(HttpFileCollection hc, HttpContext context)
        {
            HttpPostedFile _file = hc[0];
            //文件大小  
            long _size = _file.ContentLength;
            if (_size <= 0)
            {
                context.Response.Write("文件错误。");
                context.Response.End();
                return;
            }
            if (size * 1024 < _size)
            {
                context.Response.Write("文件过大,最大限制为" + size + "KB。");
                context.Response.End();
                return;
            }
            //文件名  
            string _name = _file.FileName;
            Image image = new Bitmap(_file.InputStream);
            image = Resize(image, 330, 330, false);
            string fileName = string.Format("{0}_{1}", GetFileName(_name), DateTime.Now.ToString("yyyyMMddhhmmssfff"));
          
            //文件格式  
            string _tp = System.IO.Path.GetExtension(_name).ToLower();
            if (type.IndexOf(_tp) == -1)
            {
                context.Response.Write("文件格式错误。");
                context.Response.End();
                return;
            }
            //保存路径  
           
            try
            {

                string _path = HttpContext.Current.Server.MapPath(path) + @"/" +fileName+ _tp;
                SaveMap(image, _path, true);
                context.Response.Write("{\"imgurl\":\"" + fileName + _tp + "\"}");
            }
            catch (Exception)
            {
                context.Response.Write("哎呦，出错了。");
                context.Response.End();
            }
        
        
        }
        private string GetFileName(string strFilePath)
        {
            if (strFilePath == null)
                return string.Empty;
            string fileName = strFilePath.Split('\\').ToList().Last();
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        }


        /// <summary> 
        /// 缩放图片 
        /// </summary> 
        /// <param name="image">Image 对象</param> 
        /// <param name="width">图片新的宽度</param> 
        /// <param name="height">图片新高度</param> 
        /// <param name="scale">是否按比例缩放图片</param> 
        /// <returns>Image 对象</returns> 
        public Image Resize(Image image, int width, int height, bool scaleable)
        {
            // 定义图片的新尺寸 
            int iWidth, iHeight;

            // 如果是按比例缩放图片(即scaleable = true)，生成的图片的尺寸以不超过指定尺寸为准，否则以绝对尺寸为准 
            if (scaleable)
            {
                if (image.Width > image.Height)
                {
                    iWidth = width;
                    iHeight = image.Height * iWidth / image.Width;
                }
                else
                {
                    iHeight = height;
                    iWidth = image.Width * iHeight / image.Height;
                }
            }
            else
            {
                iWidth = width;
                iHeight = height;
            }

            Rectangle r = new Rectangle(0, 0, iWidth, iHeight);
            Image img = new Bitmap(iWidth, iHeight);
            using (Graphics g = Graphics.FromImage(img))
            {
                // 定义缩放图片为高质量 
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed; 
                g.DrawImage(image, r);
            }
            return img;
        }

        public string SaveMap(Image image, string filePath, bool compressible)
        {
            if (compressible)
            {
                ImageCodecInfo ici = ImageCodecInfo.GetImageEncoders().Where(p => p.MimeType.ToLower() == "image/jpeg").FirstOrDefault();
                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Convert.ToInt32(40)); //这里最后一个参数,需要注意,即使给的参数是整数,也必须要转成Int32,否则会被默认为byte类型!
                image.Save(filePath, ici, ep);
            }
            else
            {
                image.Save(filePath);
            }
            return filePath;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}