using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LaGeBiaoQing.Utility
{
    class ConstUtility
    {
        public static Color GlobalBackColor() {
            return System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
        }

        public static Color GlobalTitleColor()
        {
            return System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
        }

        public static Color TabBar_BackColor_Selected { get { return Color.FromArgb(255, 52, 73, 94); } }
        public static Color TabBar_BackColor_MouseOver { get { return Color.FromArgb(255, 57, 67, 87); } }
        public static Color TabBar_BackColor { get { return Color.FromArgb(255, 44, 62, 80); } }
        
    }
}
