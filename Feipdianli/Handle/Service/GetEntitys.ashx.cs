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
    /// GetEntitys 的摘要说明
    /// </summary>
    public class GetEntitys : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            StringBuilder sbSQL = new StringBuilder("SELECT E1.[Id],E1.[ParentID] ,E1.[EntityName],E2.EntityName as ParentName FROM [Entity] E1 LEFT JOIN [Entity] E2 ON E1.ParentID = E2.Id ");
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sbSQL.ToString(), "phone");
            context.Response.Write(DbComponent.JSON.DatatableToJson(dt, "Devices")); 
        
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