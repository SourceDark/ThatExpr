using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaGeBiaoQing.Utility
{
    class SettingUtility
    {
        public static string getIdString()
        {
            return Properties.Settings.Default["IdString"] as string;
        }

        public static string CachePath
        {
            get
            {
                return Properties.Settings.Default["CachePath"] as string;
            }
            set
            {
                Properties.Settings.Default["CachePath"] = value;
            }
        }
    }
}
