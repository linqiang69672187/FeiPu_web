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
    /// GetDevices 的摘要说明
    /// </summary>
    public class GetDevices : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            GetDevicesByUser(context);

        }

        public void GetDevicesByUser(HttpContext context)
        {
           

           string Username = context.Request["username"];
           string sqltext = "SELECT  de.c_index_code,de.c_cascade_code,de.i_domain_id,de.c_device_ip,de.i_device_port,de.c_user_name,de.c_user_pwd, de.c_phonenumber,de.c_user,de.c_name,de.elementId,de.c_org_name,de.deviceIndexcode,de.i_status,gps.latitude,gps.longitude,gps.sdataTime,et.EntityName,de.id,et.id as eid,c_enable  from  Device_Info as de  LEFT  JOIN  GPS_Info as gps on de.deviceIndexcode = gps.deviceIndexcode LEFT JOIN Entity et on de.entityID = et.Id ";

          if (CheckuserReturnSQL.Returnsql(Username) != "管理员") //网页登录管理员
           {
               sqltext = "SELECT  de.c_index_code,de.c_cascade_code,de.i_domain_id,de.c_device_ip,de.i_device_port,de.c_user_name,de.c_user_pwd, de.c_phonenumber,de.c_user,de.c_name,de.elementId,de.c_org_name,de.deviceIndexcode,de.i_status,gps.latitude,gps.longitude,gps.sdataTime,et.EntityName,de.id,et.id as eid ,c_enable  from  Device_Info as de  LEFT  JOIN  GPS_Info as gps on de.deviceIndexcode = gps.deviceIndexcode LEFT JOIN Entity et on de.entityID = et.Id and c_enable=1 ";

           }




           DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sqltext, "phone");
            context.Response.Write(DbComponent.JSON.DatatableToJson(dt,"Devices")); ;//获取摄像头资源
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