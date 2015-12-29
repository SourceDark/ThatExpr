using LaGeBiaoQing.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.Settings
{
    class SettingsTabPage : TabPage
    {
        private GroupBox topMostSetting;
        private Label topMostSettingTitle;
        private Panel topMostSettingPanel;
        private RadioButton topMostSettingTrue;
        private RadioButton topMostSettingFalse;

        public SettingsTabPage()
        {
            this.Location = new System.Drawing.Point(4, 22);
            this.Name = "SettingsPage";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(282, 394);
            this.Text = "设置";
            this.UseVisualStyleBackColor = true;

            topMostSetting = new GroupBox();
            topMostSetting.Location = new System.Drawing.Point(7, 7);
            topMostSetting.Name = "groupBox1";
            topMostSetting.Size = new System.Drawing.Size(270, 200);
            topMostSetting.TabIndex = 4;
            topMostSetting.TabStop = false;
            topMostSetting.Text = "全局设置";
            this.Controls.Add(topMostSetting);

            topMostSettingPanel = new Panel();
            topMostSettingPanel.Location = new System.Drawing.Point(0, 0);
            topMostSettingPanel.Size = new System.Drawing.Size(270, 200);
            topMostSetting.Controls.Add(topMostSettingPanel);

            topMostSettingTitle = new Label();
            topMostSettingTitle.Location = new System.Drawing.Point(15, 26);
            topMostSettingTitle.AutoSize = true;
            topMostSettingTitle.Text = "窗体保持置顶";
            topMostSettingPanel.Controls.Add(topMostSettingTitle);

            topMostSettingTrue = new RadioButton();
            topMostSettingTrue.Location = new System.Drawing.Point(160, 25);
            topMostSettingTrue.Text = "是";
            topMostSettingTrue.Click += TopMostSettingTrue_Click;
            topMostSettingTrue.Checked = SettingUtility.getIsMainFormTopMost();
            topMostSettingPanel.Controls.Add(topMostSettingTrue);
            topMostSettingTrue.AutoSize = true;

            topMostSettingFalse = new RadioButton();
            topMostSettingFalse.Location = new System.Drawing.Point(220, 25);
            topMostSettingFalse.Text = "否";
            topMostSettingFalse.Click += TopMostSettingFalse_Click;
            topMostSettingFalse.Checked = !SettingUtility.getIsMainFormTopMost();
            topMostSettingPanel.Controls.Add(topMostSettingFalse);
            topMostSettingFalse.AutoSize = true;
        }

        private void TopMostSettingTrue_Click(object sender, EventArgs e)
        {
            this.FindForm().TopMost = true;
            SettingUtility.setIsMainFormTopMost(true);
        }

        private void TopMostSettingFalse_Click(object sender, EventArgs e)
        {
            this.FindForm().TopMost = false;
            SettingUtility.setIsMainFormTopMost(false);
        }
    }
}
