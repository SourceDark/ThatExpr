using LaGeBiaoQing.View.ComboBoxes;
using LaGeBiaoQing.View.FlowLayoutPanels;
using System.Windows.Forms;
using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using System;
using System.ComponentModel;
using LaGeBiaoQing.Service;
using System.Drawing;
using System.Collections.Generic;

namespace LaGeBiaoQing.View.TabPages
{
    class CollectionTabPage : Panel
    {
        private static int numberOfRows = 5;
        private static int numberOfColunms = 6;
        private static int borderWidth = 50;
        private BackgroundWorker worker;

        private List<ExprsDisplayer> exprDisplayers;

        public CollectionTabPage(int width, int height)
        {
            // View
            this.Width = width;
            this.Height = height;

            int topToBound = (height - numberOfRows * (borderWidth - 1) - 1) / 2;
            int leftToBound = (width - numberOfColunms * (borderWidth - 1) - 1) / 2;
            for (int i = 0; i < numberOfRows; i++)
                for (int j = 0; j < numberOfColunms; j++) {
                    Panel panel = new Panel();
                    panel.Top = topToBound + i * (borderWidth - 1) + 1;
                    panel.Left = leftToBound + j * (borderWidth - 1) + 1;
                    panel.Width = borderWidth;
                    panel.Height = borderWidth;
                    panel.BackColor = ConstUtility.GlobalBorderColor;
                    this.Controls.Add(panel);
                    
                    Panel panel1 = new Panel();
                    panel1.Top = topToBound + i * (borderWidth - 1) + 2;
                    panel1.Left = leftToBound + j * (borderWidth - 1) + 2;
                    panel1.Width = borderWidth - 2;
                    panel1.Height = borderWidth - 2;
                    panel1.BackColor = ConstUtility.GlobalBackColor();
                    this.Controls.Add(panel1);
                    panel1.BringToFront();


                }

            // Data
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();

            // Event
            EnvUtility.CollectedExprsChanged += EnvUtility_CollectedExprsChanged;
        }
        
        private void EnvUtility_CollectedExprsChanged()
        {
            Console.Out.WriteLine("0");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            EnvUtility.collectedExprs = ExprService.GetCollectedExprs();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.Out.WriteLine(EnvUtility.collectedExprs.Count);
            EnvUtility.CollectedExprsChangeFinishied();
        }
        
    }
}
