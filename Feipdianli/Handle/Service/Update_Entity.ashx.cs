using DbComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// Update_Entity 的摘要说明
    /// </summary>
    public class Update_Entity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];
            string ParentID = context.Request["ParentID"];
            string c_name = context.Request["c_name"];
        

            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@id", id);
            sp[1] = new SqlParameter("@ParentID", ParentID);
            sp[2] = new SqlParameter("@c_name", c_name);


            StringBuilder sbSQL = new StringBuilder("update [Entity] SET [EntityName] =@c_name,ParentID=@ParentID where Id = @id");
            SQLHelper.ExecuteNonQuery(CommandType.Text, sbSQL.ToString(), sp);
            
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