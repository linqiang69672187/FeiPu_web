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
    /// GetEntitys_Jstree 的摘要说明
    /// </summary>
    public class GetEntitys_Jstree : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder sbSQL = new StringBuilder("SELECT [Id] as id,[EntityName] as text,ParentID as parent FROM [Entity]");
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sbSQL.ToString(), "phone");
            context.Response.Write(DbComponent.JSON.DatatableToJstree(dt, "Entity")); 


           
            //{ "id": "ajson1", "parent": "#", "text": "Simple root node" },
            //{ "id": "ajson2", "parent": "#", "text": "Root node 2" },
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