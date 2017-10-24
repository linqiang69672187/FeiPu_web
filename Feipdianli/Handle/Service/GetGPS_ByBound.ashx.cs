using DbComponent;
using Feipdianli.CLASS.Static;
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
    /// GetGPS_ByBound 的摘要说明
    /// </summary>
    public class GetGPS_ByBound : IHttpHandler
    {

        public double maxlo, maxla, minlo, minla;
        StringBuilder retJson = new StringBuilder();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            maxlo = Convert.ToDouble(context.Request["maxlo"]);
            maxla = Convert.ToDouble(context.Request["maxla"]);
            minlo = Convert.ToDouble(context.Request["minlo"]);
            minla = Convert.ToDouble(context.Request["minla"]);
            SqlParameter[] sp = new SqlParameter[4];
            sp[0] = new SqlParameter("@maxlo", maxlo);
            sp[1] = new SqlParameter("@maxla", maxla);
            sp[2] = new SqlParameter("@minlo", minlo);
            sp[3] = new SqlParameter("@minla", minla);

            string Username = context.Request["username"];
            string sqltype = CheckuserReturnSQL.Returnsql(Username);
            string sqltext = "SELECT   de.c_phonenumber,de.c_user,de.c_name,de.elementId,de.c_org_name,de.deviceIndexcode,de.i_status,gps.latitude,gps.longitude,gps.sdataTime from  Device_Info as de  LEFT  JOIN  GPS_Info as gps on de.deviceIndexcode = gps.deviceIndexcode where gps.latitude >@minLa and gps.latitude <@maxLa and gps.longitude>@minLo and gps.longitude<@maxLo and c_enable=1 ";

            if (sqltype != "管理员") //管理员?
            {
                sqltext = "SELECT   de.c_phonenumber,de.c_user,de.c_name,de.elementId,de.c_org_name,de.deviceIndexcode,de.i_status,gps.latitude,gps.longitude,gps.sdataTime from  Device_Info as de  LEFT  JOIN  GPS_Info as gps on de.deviceIndexcode = gps.deviceIndexcode where gps.latitude >@minLa and gps.latitude <@maxLa and gps.longitude>@minLo and gps.longitude<@maxLo and de.entityID in (" + sqltype + ") and c_enable=1";

            }





            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sqltext.ToString(), "gps",sp);
             context.Response.Write(DbComponent.JSON.DatatableToJson(dt,"GPS"));
                //获取GPS范围数据，并格式化JSON
          
        }


        /// <summary>Class <c>Point</c> models a point in a two-dimensional
        /// plane.</summary>

      
      

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}