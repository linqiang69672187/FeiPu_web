using DbComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// GetPlatSessionID 的摘要说明
    /// </summary>
    public class GetPlatSessionID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           // context.Response.Write("Hello World");
            try
            {
                string username ;
                string PlatLoginPWD ;
                string PlatIP;

                StringBuilder sbSQL = new StringBuilder("SELECT [id],[PlatLoginName],[PlatLoginPWD],[PlatIP] FROM [Setting] ");
                DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sbSQL.ToString(), "phone");

            
               username = dt.Rows[0]["PlatLoginName"].ToString().TrimEnd();
               PlatLoginPWD = dt.Rows[0]["PlatLoginPWD"].ToString().TrimEnd();
               PlatIP = dt.Rows[0]["PlatIP"].ToString().TrimEnd();
            





                Getresouce.VmsSdkWebServicePortTypeClient gt = new Getresouce.VmsSdkWebServicePortTypeClient("VmsSdkWebServiceHttpSoap11Endpoint");
                string xml = gt.sdkLogin(username, SHA256Encrypt(PlatLoginPWD), PlatIP, "", "");
                ServiceResult sr = ServiceResult.Parse(xml);

                context.Response.Write("{\"result\":\"" + sr.Rows[0]["tgt"] + "\",\"r\":\"0\"}");
            }
            catch (Exception ex)
            {

                context.Response.Write("{\"result\":\"" + ex.ToString()+ "\",\"r\":\"1\"}");
            }
           
        }
        public static string SHA256Encrypt(string str)//256加密
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            System.Security.Cryptography.SHA256 Sha256 = new System.Security.Cryptography.SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);
            return BitConverter.ToString(by).Replace("-", "").ToLower();
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