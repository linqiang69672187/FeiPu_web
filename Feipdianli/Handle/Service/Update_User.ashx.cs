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
    /// Update_User 的摘要说明
    /// </summary>
    public class Update_User : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];
            string Mange_EntityIDs = context.Request["Mange_EntityIDs"];
            string pwd = context.Request["pwd"];
            string Imgurl = context.Request["Imgurl"];

            SqlParameter[] sp = new SqlParameter[4];
            sp[0] = new SqlParameter("@id", id);
            sp[1] = new SqlParameter("@Mange_EntityIDs", Mange_EntityIDs);
            sp[2] = new SqlParameter("@pwd", pwd);
            sp[3] = new SqlParameter("@Imgurl", Imgurl);

            StringBuilder sbSQL = new StringBuilder("update [Login] SET [Mange_EntityIDs]=@Mange_EntityIDs,[pwd]=@pwd,[Imgurl]=@Imgurl  where Id = @id");
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