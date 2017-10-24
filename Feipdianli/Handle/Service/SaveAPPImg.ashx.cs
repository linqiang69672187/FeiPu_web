using Feipdianli.CommonClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// SaveAPPImg 的摘要说明
    /// </summary>
    public class SaveAPPImg : IHttpHandler
    {
       
        public void ProcessRequest(HttpContext context)
        {
            string Username = "";
            string headimage = "";
            try
            {
                context.Response.ContentType = "text/plain";

                Username = context.Request["username"];

                headimage = context.Request["headimage"];
                /*如果POST取不到，用这个试试
                 StreamReader reader = new StreamReader(context.Request.InputStream); //如果POST取不到，用这个试试
                 string postdata = reader.ReadToEnd();
                 */
               string[] img = Regex.Split(headimage, "base64,");
               string dir =  Base64StringToImage(Username, img[1], context);

               context.Response.Write("{\"result\":\"" + dir + "\",\"r\":\"0\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"result\":\"" + ex.ToString() + "\",\"r\":\"1\"}");
                LogHelper.WriteLog(typeof(Exception), ex.ToString() + "___" + Username+"___"+headimage);
            }
        }

        //base64编码的字符串转为图片
        protected string Base64StringToImage(string Username, string headimage,HttpContext context)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(headimage);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
              

               string dir =  DriveDir.urlTolocal("Upload/Headimg/"+Username+".jpg");
               File.Delete(dir);

               bmp.Save(dir, System.Drawing.Imaging.ImageFormat.Jpeg);
          
                ms.Close();
                LogHelper.WriteLog(typeof(Exception), "成功");
                return dir;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex.ToString());
             
                return null;
            }
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