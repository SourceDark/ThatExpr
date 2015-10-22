using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaGeBiaoQing
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (Properties.Settings.Default["IdString"] != null && Properties.Settings.Default["IdString"].ToString().Length > 0)
            {
                Application.Run(new Main());
            }
            else
            {
                Application.Run(new Form1());
            }

        }
    }
}
