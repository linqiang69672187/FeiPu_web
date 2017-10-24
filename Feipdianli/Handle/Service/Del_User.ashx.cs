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
    /// Del_User 的摘要说明
    /// </summary>
    public class Del_User : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];


            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", id);


            StringBuilder sbSQL = new StringBuilder("delete [Login]  where Id = @id");
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