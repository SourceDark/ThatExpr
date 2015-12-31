using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using LaGeBiaoQing.View.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.PictureBoxs
{
    class ExprDisplayer : PictureBox
    {
        public Expr expr;

        public ExprDisplayer(Expr expr)
        {
            this.expr = expr;

            this.ContextMenu = ExprDisplayerRightClickMenuFactory.getInstance();
            this.Click += ExprDisplayer_Click;
        }

        private void ExprDisplayer_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            MouseEventArgs me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
            {
                WindowsUtility.sendTo(expr, pictureBox.Image, WindowType.WindowTypeQQ);
            }
        }
    }
}
