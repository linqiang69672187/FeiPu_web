using DbComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Feipdianli.Handle.Service
{
    /// <summary>
    /// GetaMarksbyBound 的摘要说明
    /// </summary>
    public class GetaMarksbyBound : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string bounds = context.Request.Form["bounds"];
            string[] arry = bounds.Split(new char[1] { ',' });
            double Llongitu = Convert.ToDouble(arry[0]);
            double Llati = Convert.ToDouble(arry[1]);
            double Rlongitu = Convert.ToDouble(arry[2]);
            double Rlati = Convert.ToDouble(arry[3]);

     
            //"SELECT gps.[ID],[IsOnline],gps.Lo,gps.La,de.Contacts,de.Tel,et.Name,de.[DevType],de.[Cartype],de.DevId,de.[PlateNumber] FROM [Gps] as gps left join Device de on gps.PDAID = de.DevId left join  Entity et  on et.ID = de.EntityId where  de.[DevType] <> '' and  gps.La <" + Rlongitu + " and gps.La >" + Llongitu + " and gps.Lo <" + Rlati + "  and gps.Lo >" + Llati

            string sqltext;

            sqltext = "SELECT [c_name],[latitude],[longitude],c_index_code,[i_status] FROM [Device_Info] de left join [GPS_Info] gps on de.[deviceIndexcode] =  gps.[deviceIndexcode] where latitude is not null and c_name not like '%纽扣摄像机%'";
        
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sqltext.ToString(), "entity");



            context.Response.Write(JSON.DatatableToJson(dt, ""));

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