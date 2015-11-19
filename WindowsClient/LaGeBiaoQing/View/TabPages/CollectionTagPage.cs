using LaGeBiaoQing.View.ComboBoxes;
using LaGeBiaoQing.View.FlowLayoutPanels;
using System.Windows.Forms;
using LaGeBiaoQing.Model;
using System;
using LaGeBiaoQing.Utility;

namespace LaGeBiaoQing.View.TabPages
{
    class CollectionTagPage : TabPage
    {
        private TagContentComboBox tagContentComboBox;
        private ExprsDisplayer exprsDisplayer;

        public CollectionTagPage()
        {
            this.Location = new System.Drawing.Point(4, 22);
            this.Name = "CollectionPage";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(282, 394);
            this.Text = "收藏";
            this.UseVisualStyleBackColor = true;

            tagContentComboBox = new TagContentComboBox(TagContentComboBoxType.InCollectionTabPage);
            tagContentComboBox.FormattingEnabled = true;
            tagContentComboBox.Location = new System.Drawing.Point(6, 8);
            tagContentComboBox.Name = "tagContentComboBox";
            tagContentComboBox.Size = new System.Drawing.Size(121, 20);
            tagContentComboBox.TabIndex = 0;
            tagContentComboBox.SelectTagContent += TagContentComboBox_SelectTagContent;
            //tagContentComboBox.SelectNewest += TagContentComboBox_SelectNewest;
            this.Controls.Add(tagContentComboBox);

            exprsDisplayer = new ExprsDisplayer();
            exprsDisplayer.AutoScroll = true;
            exprsDisplayer.AutoScrollMargin = new System.Drawing.Size(10, 0);
            exprsDisplayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            exprsDisplayer.Location = new System.Drawing.Point(6, 34);
            exprsDisplayer.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            exprsDisplayer.Size = new System.Drawing.Size(270, 167);
            this.Controls.Add(exprsDisplayer);

            tagContentComboBox.loadTagContents();
        }

        private void TagContentComboBox_SelectTagContent(object sender, TagContent selectTagContent)
        {
            Console.Out.WriteLine("!!!" + selectTagContent.content);
            exprsDisplayer.loadRemoteExprs(selectTagContent.content, SettingUtility.getIdString());
        }
    }
}
