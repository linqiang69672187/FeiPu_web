using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// GetMangeEntitys 的摘要说明
    /// </summary>
    public class GetMangeEntitys : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("[{ 'id': '0', 'text': 'enhancement' }");
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