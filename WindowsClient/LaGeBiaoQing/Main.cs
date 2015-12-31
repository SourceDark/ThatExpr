using System.Windows.Forms;
using LaGeBiaoQing.View.TabPages;
using LaGeBiaoQing.View;
using LaGeBiaoQing.View.Settings;
using LaGeBiaoQing.Utility;
using System.Runtime.InteropServices;
using System;

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
               
        // Functions

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponent();
        }

        public void InitializeCustomComponent()
        {
            tabControl1.Controls.Add(new CollectionTagPage());
            tabControl1.Controls.Add(new DiscoverTabPage());
            tabControl1.Controls.Add(new SettingsTabPage());

            // Local settings
            this.TopMost = SettingUtility.getIsMainFormTopMost();

            // Event
            this.MouseDown += This_MouseDown;
        }

        // The following code is to make the form still dragable even if we hide its border
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void This_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
