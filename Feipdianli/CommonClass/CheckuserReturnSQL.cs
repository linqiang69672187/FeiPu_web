using DbComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Feipdianli.CommonClass
{
    public class CheckuserReturnSQL
    {
       
        public static string Returnsql(string Username)
        {
            string[] ids;
            string entityidstring="";
            string sqltext="";
            //其判断是不是管理员              

            if (Username != null)
           {
               SqlParameter[] sp = new SqlParameter[1];
               sp[0] = new SqlParameter("@Username", Username);

               StringBuilder sbSQLenti = new StringBuilder("SELECT [Mange_EntityIDs],[type] FROM [Login] where [username] = @Username");
               DataTable dtx = SQLHelper.ExecuteRead(CommandType.Text, sbSQLenti.ToString(), "phone", sp);
               ids = dtx.Rows[0]["Mange_EntityIDs"].ToString().TrimEnd().Split(new char[2] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
               for (int i = 0; i < ids.Length; i++)
               {
                   entityidstring += (i > 0) ? "," + ids[i] : ids[i];

               }
               //APP登录普通用户
               sqltext = entityidstring;

               if (dtx.Rows[0]["type"].ToString() == "管理员") //APP登录管理员
               {
                   sqltext = "管理员";

               }
           }

           if (Username == null) //网页登录管理员
           {
               sqltext = "管理员";

           }



           return sqltext;
        }
    }
}