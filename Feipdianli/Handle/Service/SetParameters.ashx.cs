using DbComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// SetParameters 的摘要说明
    /// </summary>
    public class SetParameters : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];


            switch (type)
            {
                case "read":
                    Readconfig(context);
                    break;

                case "update":
                    Updateconfig(context);
                    break;
                default:
                  
                    break; 

            }


        }
        public void Readconfig(HttpContext context)
        {
            StringBuilder sbSQL = new StringBuilder("SELECT [id],[PlatLoginName],[PlatLoginPWD],[PlatIP] FROM [Setting] ");
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sbSQL.ToString(), "phone");
            context.Response.Write(DbComponent.JSON.DatatableToJson(dt, "Devices")); 


        }
        public void Updateconfig(HttpContext context)
        {
            


            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@PlatLoginName", context.Request["PlatLoginName"]);
            sp[1] = new SqlParameter("@PlatLoginPWD", context.Request["PlatLoginPWD"]);
            sp[2] = new SqlParameter("@PlatIP", context.Request["PlatIP"]);
           

            StringBuilder sbSQL = new StringBuilder("update [Setting] SET [PlatLoginName] =@PlatLoginName,PlatLoginPWD=@PlatLoginPWD,PlatIP=@PlatIP");
            SQLHelper.ExecuteNonQuery(CommandType.Text, sbSQL.ToString(), sp);

            context.Response.Write("{\"result\":\"修改成功\"}");

        }

        public static bool UpdateAppSettings(string key, string value)
        {
            var _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!_config.HasFile)
            {
                throw new ArgumentException("程序配置文件缺失！");
            }
            KeyValueConfigurationElement _key = _config.AppSettings.Settings[key];
            if (_key == null)
                _config.AppSettings.Settings.Add(key, value);
            else
                _config.AppSettings.Settings[key].Value = value;
            _config.Save(ConfigurationSaveMode.Modified);
            return true;
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