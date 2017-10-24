using DbComponent;
using Feipdianli.CommonClass;
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
    /// Update_PWD 的摘要说明
    /// </summary>
    public class Update_PWD : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
          try { 
          
            string username = context.Request["username"];

            string pwd = context.Request["pwd"];
            string newpwd = context.Request["newpwd"];

            if (username == null) { username = context.Request.Cookies["userrole"].Values["username"]; }
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@username", username);
            sp[1] = new SqlParameter("@pwd", pwd);
            sp[2] = new SqlParameter("@newpwd", newpwd);


            DataTable dt = DbComponent.login.loginin(username, pwd);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"result\":\"原密码错误\",\"r\":\"1\"}");
                return;
            }

            StringBuilder sbSQL = new StringBuilder("update [Login] SET [pwd]=@newpwd where username = @username");
            SQLHelper.ExecuteNonQuery(CommandType.Text, sbSQL.ToString(), sp);

            context.Response.Write("{\"result\":\"修改成功\",\"r\":\"0\"}");
               
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"result\":\"" + ex.ToString()+ "\",\"r\":\"0\"}");
                LogHelper.WriteLog(typeof(Exception), ex.ToString());

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