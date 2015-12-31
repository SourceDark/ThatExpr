using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.InfoExtended
{
    class InfoExtendedMenuItem : MenuItem
    {
        public object info;

        public InfoExtendedMenuItem(string title, object _info) : base(title)
        {
            info = _info;
        }
    }
}
