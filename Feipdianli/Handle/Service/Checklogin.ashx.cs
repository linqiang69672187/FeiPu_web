using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// Checklogin 的摘要说明
    /// </summary>
    public class Checklogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request["username"];
            string pwd = context.Request["pwd"];
         
            DataTable dt =  DbComponent.login.loginin(username, pwd);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"result\":\"账号或密码不存在\",\"r\":\"1\"}");

            }
            else
            {
                context.Response.Write("{\"result\":\"登录成功\",\"r\":\"0\",\"type\":\"" + dt.Rows[0]["type"] + "\"}");
                HttpCookie myCookie = new HttpCookie("userrole");
                myCookie.Values["username"] = username;
              
                myCookie.Expires.AddDays(20);
                context.Response.Cookies.Add(myCookie);
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