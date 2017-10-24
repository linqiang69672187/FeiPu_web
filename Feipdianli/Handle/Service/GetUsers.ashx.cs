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
    /// GetUsers 的摘要说明
    /// </summary>
    public class GetUsers : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder sbSQL = new StringBuilder("SELECT Id, [username],[pwd],[Mange_EntityIDs],[type],[Imgurl] FROM [Login] ");
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, sbSQL.ToString(), "user");

            StringBuilder sbSQL2 = new StringBuilder("SELECT Id, [EntityName] FROM [Entity] ");
            DataTable entity = SQLHelper.ExecuteRead(CommandType.Text, sbSQL2.ToString(), "entity");


            DataTable loginEntitys = new DataTable("loginEntitys");

            loginEntitys.Columns.Add("Id", Type.GetType("System.Int16"));
            loginEntitys.Columns.Add("username", Type.GetType("System.String"));
            loginEntitys.Columns.Add("pwd", Type.GetType("System.String"));
            loginEntitys.Columns.Add("Mange_EntityIDs", Type.GetType("System.String"));
            loginEntitys.Columns.Add("type", Type.GetType("System.String"));
            loginEntitys.Columns.Add("Imgurl", Type.GetType("System.String"));
            loginEntitys.Columns.Add("Mange_Entitys", Type.GetType("System.String"));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = loginEntitys.NewRow();

                dr["Id"] = dt.Rows[i]["Id"];
                dr["pwd"] = dt.Rows[i]["pwd"];
                dr["username"] = dt.Rows[i]["username"];
                dr["Mange_EntityIDs"] = dt.Rows[i]["Mange_EntityIDs"];
                dr["type"] = dt.Rows[i]["type"];
                dr["Imgurl"] = dt.Rows[i]["Imgurl"];
                string entitys= "";
                for (int n = 0; n < entity.Rows.Count; n++)
                {
                    if (dt.Rows[i]["Mange_EntityIDs"].ToString().Contains("[" + entity.Rows[n]["Id"] + "]"))
                    {
                        entitys += "[" + entity.Rows[n]["EntityName"] + "]";
                    }
                
                }
                dr["Mange_Entitys"] = entitys;

                entitys = null;


                loginEntitys.Rows.Add(dr);

            }



            context.Response.Write(DbComponent.JSON.DatatableToJson(loginEntitys, "user")); 

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