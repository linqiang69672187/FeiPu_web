using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComponent
{
    public class GPS
    {
        public static DataTable GetGPSData_ByBound(SqlParameter[] sp)
        {

           return SQLHelper.ExecuteReadStrProc(CommandType.StoredProcedure, "GPS_ReadBound","GPS",sp);

        }


    }
}
