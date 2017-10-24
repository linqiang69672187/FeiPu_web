using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feipdianli.CommonClass
{
    public class DriveDir
    {
        /// 将物理路径转换成相对路径  
        /// </summary>  
        /// <param name="imagesurl1"></param>  
        /// <returns></returns>  
      public  static string urlToVirtual(string imagesurl1)
        {
            //其实这里的tmpRootDir也等于tmpRootDir</span><span style="font-size:18px;">=Server.MapPath(</span><span style="font-size:18px;">"~/");              

            string tmpRootDir = HttpRuntime.AppDomainAppPath.ToString();//获取程序根目录  
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径  
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            return imagesurl2;
        }
        //相对路径转换成服务器本地物理路径  
      public static string urlTolocal(string imagesurl1)
        {
            string tmpRootDir = HttpRuntime.AppDomainAppPath.ToString();//获取程序根目录  
            string imagesurl2 = tmpRootDir + imagesurl1.Replace(@"/", @"\"); //转换成绝对路径  
            return imagesurl2;
        }
    }


}