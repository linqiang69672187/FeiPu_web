using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DbComponent
{
    public class login
    {


        #region 用户登陆
        public static DataTable loginin(string username, string pwd)
        {
            DataTable dt = SQLHelper.ExecuteRead(CommandType.Text, "select [type]  from Login where [Username] =@username and [Pwd]=@pwd ", "phone", new SqlParameter("username", username), new SqlParameter("pwd", pwd));
          //
            return dt;
                //int.Parse(SQLHelper.ExecuteScalar(CommandType.Text, "select [type]  from Login where [Username] =@username and [Pwd]=@pwd ", new SqlParameter("username", username), new SqlParameter("pwd", pwd)).ToString());
        }
        #endregion



       
    }
}
