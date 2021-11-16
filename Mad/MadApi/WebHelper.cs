using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadApi
{
    public class WebHelper
    {
        public static string ConnectionString()
        {
            return @"Data Source=DBN-NKOSINATHIN\SQLEXPRESS;Initial Catalog=Mad;Integrated Security=True;";
        }
    }
}
