using System.Windows.Forms;
using LaGeBiaoQing.View.TabPages;
using LaGeBiaoQing.View;
using LaGeBiaoQing.View.Settings;
using LaGeBiaoQing.Utility;
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
        // this is used to create shadows
        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private Panel titlePanel;
        private Button minusButton;
        private Button closeButton;
        private PrimaryTabPanel primaryTabPanel;

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponent();
        }

        public void InitializeCustomComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));

            titlePanel = new Panel();
            titlePanel.BackColor = ConstUtility.GlobalTitleColor();
            titlePanel.Left = 0;
            titlePanel.Top = 0;
            titlePanel.Width = this.Width;
            titlePanel.Height = 30;
            this.Controls.Add(titlePanel);

            closeButton = new Button();
            closeButton.TabStop = false;
            closeButton.Width = 55;
            closeButton.Height = 30;
            closeButton.Left = titlePanel.Width - 55;
            closeButton.Top = 0;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            closeButton.Image = ((System.Drawing.Image)(resources.GetObject("Close-16-3")));
            closeButton.Click += CloseButton_Click;
            titlePanel.Controls.Add(closeButton);

            minusButton = new Button();
            minusButton.TabStop = false;
            minusButton.Width = 55;
            minusButton.Height = 30;
            minusButton.Left = titlePanel.Width - 55 * 2;
            minusButton.Top = 0;
            minusButton.FlatStyle = FlatStyle.Flat;
            minusButton.FlatAppearance.BorderSize = 0;
            minusButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            minusButton.Image = ((System.Drawing.Image)(resources.GetObject("Minus-16-2")));
            minusButton.Click += MinusButton_Click;
            titlePanel.Controls.Add(minusButton);

            primaryTabPanel = new PrimaryTabPanel(this.Width, this.Height - titlePanel.Height - 50, 50, ConstUtility.GlobalBackColor(), ConstUtility.TabBar_BackColor);
            primaryTabPanel.Left = 0;
            primaryTabPanel.Top = titlePanel.Height;
            this.Controls.Add(primaryTabPanel);

            titlePanel.MouseDown += This_MouseDown;

            this.BackColor = ConstUtility.GlobalBackColor();
            /*
            tabControl1.Controls.Add(new CollectionTagPage());
            tabControl1.Controls.Add(new DiscoverTabPage());
            tabControl1.Controls.Add(new SettingsTabPage());
            */
            //tabControl1.Hide();

            // Local settings
            this.TopMost = SettingUtility.getIsMainFormTopMost();

            // Event
            this.MouseDown += This_MouseDown;

            //TransparencyKey = Color.Lime;
            //BackColor = Color.Lime;
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
