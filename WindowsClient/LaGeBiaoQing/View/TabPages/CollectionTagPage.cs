using LaGeBiaoQing.View.ComboBoxes;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.TabPages
{
    class CollectionTagPage : TabPage
    {
        private TagContentComboBox tagContentComboBox;

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
            //tagContentComboBox.SelectTagContent += TagContentComboBox_SelectTagContent;
            //tagContentComboBox.SelectNewest += TagContentComboBox_SelectNewest;
            this.Controls.Add(tagContentComboBox);

            tagContentComboBox.loadTagContents();
        }

    }
}
