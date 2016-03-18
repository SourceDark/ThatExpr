using LaGeBiaoQing.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LaGeBiaoQing.View.TabPages;

namespace LaGeBiaoQing.View
{
    class TabBarItem : Button
    {
        public int index;
    }
    class PrimaryTabPanel : Panel
    {
        private Panel boardPanel;
        private Panel tabPanel;

        private static int totalCount = 3;
        private static TabBarItem[] tabBarItems;
        private TabBarItem selected;

        static string[] normal_files = { "Favorites-24-1", "Compass-24-2", "Settings-24-2" };
        static string[] selected_files = { "Favorites-24-1-Selected", "Compass-24-2-Selected", "Settings-24-2-Selected" };
        static System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));

        public PrimaryTabPanel(int width, int boardHeight, int tabHeight, Color boardColor, Color tabColor)
        {

            this.Width = width;
            this.Height = boardHeight + tabHeight;

            boardPanel = new Panel();
            boardPanel.Width = width;
            boardPanel.Height = boardHeight;
            boardPanel.Left = 0;
            boardPanel.Top = 0;
            boardPanel.BackColor = boardColor;
            this.Controls.Add(boardPanel);

            tabPanel = new Panel();
            tabPanel.Width = width;
            tabPanel.Height = tabHeight;
            tabPanel.Left = 0;
            tabPanel.Top = boardHeight;
            tabPanel.BackColor = tabColor;
            this.Controls.Add(tabPanel);

            CollectionTabPage tabPage = new CollectionTabPage(this.Width, boardHeight);
            tabPage.Left = 0;
            tabPage.Top = 0;
            boardPanel.Controls.Add(tabPage);

            tabBarItems = new TabBarItem[totalCount];
            for (int i = 0; i < totalCount; i++)
            {
                TabBarItem newItem = new TabBarItem();
                newItem.index = i;
                newItem.Left = this.Width / totalCount * i + Math.Min(i, this.Width % totalCount);
                newItem.Top = 0;
                newItem.Width = this.Width / totalCount + (i < this.Width % totalCount ? 1 : 0);
                newItem.Height = tabHeight;
                newItem.FlatStyle = FlatStyle.Flat;
                newItem.FlatAppearance.MouseDownBackColor = ConstUtility.TabBar_BackColor_Selected;
                newItem.FlatAppearance.MouseOverBackColor = ConstUtility.TabBar_BackColor_Selected;
                newItem.FlatAppearance.BorderSize = 0;
                newItem.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                newItem.Image = ((System.Drawing.Image)(resources.GetObject(normal_files[i])));
                
                newItem.Click += NewItem_Click;
                tabBarItems[i] = newItem;
                tabPanel.Controls.Add(newItem);
            }
            // 
            tabBarItems[0].PerformClick();
        }

        private void NewItem_Click(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));

            TabBarItem item = sender as TabBarItem;
            if (item != selected) {
                if (selected != null)
                {
                    selected.Image = ((System.Drawing.Image)(resources.GetObject(normal_files[selected.index])));
                }
                item.Image = ((System.Drawing.Image)(resources.GetObject(selected_files[item.index])));
                selected = item;
            }
        }
    }
}
