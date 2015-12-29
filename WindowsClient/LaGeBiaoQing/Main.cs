using System.Windows.Forms;
using LaGeBiaoQing.View.TabPages;
using LaGeBiaoQing.View;
using LaGeBiaoQing.View.Settings;
using LaGeBiaoQing.Utility;

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

            this.TopMost = SettingUtility.getIsMainFormTopMost();
        }
    }
}
