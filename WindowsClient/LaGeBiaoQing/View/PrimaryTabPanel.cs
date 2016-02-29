using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View
{
    class PrimaryTabPanel : Panel
    {
        private Panel boardPanel;
        private Panel tabPanel;

        public PrimaryTabPanel(int width, int boardHeight, int tabHeight, Color boardColor, Color tabColor)
        {
            this.Width = width;
            this.Height = boardHeight + tabHeight;

            boardPanel = new Panel();
            boardPanel.Width = width;
            boardPanel.Height = boardHeight;
            boardPanel.Left = 0;
            boardPanel.Top = 0;
            this.Controls.Add(boardPanel);

            tabPanel = new Panel();
            tabPanel.Width = width;
            tabPanel.Height = tabHeight;
            tabPanel.Left = 0;
            tabPanel.Top = boardHeight;
            this.Controls.Add(tabPanel);
        }
    }
}
