﻿using LaGeBiaoQing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using LaGeBiaoQing.View.FlowLayoutPanels;

namespace LaGeBiaoQing.Utility
{
    class SettingUtility
    {
        // TODO: this should be replace by global event dealer
        public static ExprsDisplayer exprsDisplayer;

        private static string KeyRecentlyUsedExprs = "KeyRecentlyUsedExprs";

        public static string getIdString()
        {
            return Properties.Settings.Default["IdString"] as string;
        }

        public static List<Expr> getRecentlyUsedExprs()
        {
            string jsonString = Properties.Settings.Default[KeyRecentlyUsedExprs] as string;
            return JsonConvert.DeserializeObject<List<Expr>>(jsonString);
        }

        public static void setRecentlyUsedExprs(List<Expr> recentlyUserExprs)
        {
            string jsonString = JsonConvert.SerializeObject(recentlyUserExprs);
            Properties.Settings.Default[KeyRecentlyUsedExprs] = jsonString;
            Properties.Settings.Default.Save();
        }
    }
}
