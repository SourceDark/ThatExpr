using LaGeBiaoQing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.PictureBoxs
{
    class ExprDisplayer : PictureBox
    {
        Expr expr;

        public ExprDisplayer()
        {
            this.Click += ExprDisplayer_Click;
        }

        private void ExprDisplayer_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            string[] s = new string[1];
            s[0] = cachePath + "\\" + exprs[(int)pictureBox.Tag].md5 + exprs[(int)pictureBox.Tag].extension;
            DataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.FileDrop, s);
            dataObject.SetData(DataFormats.Bitmap, pictureBox.Image);
            Clipboard.SetDataObject(dataObject);
            if (qqHandle != null)
            {
                SetForegroundWindow(qqHandle);
                SendKeys.SendWait("^V");
            }
        }
    }
}
