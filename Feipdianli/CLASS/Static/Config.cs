using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Feipdianli.CLASS.Static
{
   static public class Config
    {
        static public string m_connectionString = ConfigurationManager.AppSettings["m_connectionString"];
        //static public string mapGray = ConfigurationManager.AppSettings["mapGray"];
    }
}