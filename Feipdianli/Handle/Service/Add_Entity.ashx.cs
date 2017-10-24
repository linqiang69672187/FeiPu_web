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
    /// Add_Entity 的摘要说明
    /// </summary>
    public class Add_Entity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            
            string ParentID = context.Request["ParentID"];
            string c_name = context.Request["c_name"];


            SqlParameter[] sp = new SqlParameter[2];
            sp[0] = new SqlParameter("@ParentID", ParentID);
            sp[1] = new SqlParameter("@c_name", c_name);


            StringBuilder sbSQL = new StringBuilder("INSERT INTO  [Entity]( [EntityName],[ParentID]) VALUES ( @c_name,@ParentID) ");
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