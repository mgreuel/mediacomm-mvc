using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCommMvc.Web.Infrastructure
{
    public static class Config
    {
        static Config()
        {
            // todo move to db
            Sitename = "Absolutmoments";
        }

        public static string Sitename { get; set; }
    }
}
