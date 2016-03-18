using LaGeBiaoQing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaGeBiaoQing.Utility
{
    public delegate void CollectedExprsChangedEventHandler();

    class EnvUtility
    {
        public static List<Expr> collectedExprs;
        public static void CollectedExprsChangeFinishied()
        {
            CollectedExprsChanged();
        }
        public static event CollectedExprsChangedEventHandler CollectedExprsChanged;
    }
}
