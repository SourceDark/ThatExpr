using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
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
            this.Click += ExprDisplayer_Click;
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private void ExprDisplayer_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            string[] s = new string[1];
            s[0] = FileUtility.FullPath(expr);
            DataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.FileDrop, s);
            dataObject.SetData(DataFormats.Bitmap, pictureBox.Image);
            Clipboard.SetDataObject(dataObject);

            Process qqProcess = Process.GetProcessesByName("QQ").FirstOrDefault();
            if (qqProcess != null)
            {
                IntPtr qqHandle = qqProcess.MainWindowHandle;
                SetForegroundWindow(qqHandle);
                SendKeys.SendWait("^V");
            }
            else
            {
                Console.WriteLine("找不到QQ窗口");
            }
        }
    }
}
