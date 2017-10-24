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
    /// Add_User 的摘要说明
    /// </summary>
    public class Add_User : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string Mange_EntityIDs = context.Request["Mange_EntityIDs"];
            string username = context.Request["username"];
            string pwd = context.Request["pwd"];
            string Imgurl = context.Request["Imgurl"];
            string type = context.Request["type"];

            SqlParameter[] sp = new SqlParameter[5];
            sp[0] = new SqlParameter("@Mange_EntityIDs", Mange_EntityIDs);
            sp[1] = new SqlParameter("@username", username);
            sp[2] = new SqlParameter("@pwd", pwd);
            sp[3] = new SqlParameter("@Imgurl", Imgurl);
            sp[4] = new SqlParameter("@type", type);


            StringBuilder sbSQL = new StringBuilder("INSERT INTO  [Login]( Mange_EntityIDs,username,pwd,Imgurl,type) VALUES ( @Mange_EntityIDs,@username,@pwd,@Imgurl,@type ) ");
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