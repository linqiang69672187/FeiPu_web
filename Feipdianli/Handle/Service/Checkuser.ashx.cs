using DbComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// Checkuser 的摘要说明
    /// </summary>
    public class Checkuser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request["username"];
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, "select [type]  from Login where [Username] =@username", "phone", new SqlParameter("username", username));

            if (dt.Rows.Count == 0)
            {
                    context.Response.Write("{\"result\":\"验证通过用户名可以注册\",\"r\":\"0\"}");
            }
            else
            {
                context.Response.Write("{\"result\":\"用户名已存在，请更换\",\"r\":\"1\"}");
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