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
    /// Update_Device 的摘要说明
    /// </summary>
    public class Update_Device : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];
            string c_user = context.Request["c_user"];
            string c_phonenumber = context.Request["c_phonenumber"];
            string eid = context.Request["eid"];

            SqlParameter[] sp = new SqlParameter[4];
            sp[0] = new SqlParameter("@id", id);
            sp[1] = new SqlParameter("@c_user", c_user);
            sp[2] = new SqlParameter("@c_phonenumber", c_phonenumber);
            sp[3] = new SqlParameter("@eid", eid);

            StringBuilder sbSQL = new StringBuilder("update [Device_Info] SET c_user =@c_user,c_phonenumber=@c_phonenumber,entityID=@eid where id = @id");
            SQLHelper.ExecuteNonQuery(CommandType.Text, sbSQL.ToString(),sp);
            

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