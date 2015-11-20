using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using System.Windows.Forms;

namespace LaGeBiaoQing.View
{
    class ExprPreviewer : GroupBox
    {
        PictureBox largeViewer;
        private Expr expr;
        public Expr Expr
        {
            set
            {
                expr = value;
                largeViewer.ImageLocation = FileUtility.FullPath(expr);
            }
        }
        
        public ExprPreviewer()
        {
            largeViewer = new PictureBox();
            largeViewer.Location = new System.Drawing.Point(6, 20);
            largeViewer.Name = "largeViewer";
            largeViewer.Size = new System.Drawing.Size(257, 154);
            largeViewer.SizeMode = PictureBoxSizeMode.Zoom;
            //largeViewer.Click += new System.EventHandler(this.largerViewerr_Click);
            this.Controls.Add(largeViewer);
        }
    }
}
