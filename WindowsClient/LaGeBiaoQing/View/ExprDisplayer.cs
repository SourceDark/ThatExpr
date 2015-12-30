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
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("发送至QQ窗口");
            cm.MenuItems.Add("发送至YY窗口");
            cm.MenuItems.Add("-");

            MenuItem addTagMenuItem = new MenuItem("收藏至");
            addTagMenuItem.MenuItems.Add("默认");
            cm.MenuItems.Add(addTagMenuItem);

            this.ContextMenu = cm;
            this.Click += ExprDisplayer_Click;
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private void ExprDisplayer_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            MouseEventArgs me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
            {
                // Copy to clipboard
                string[] s = new string[1];
                s[0] = FileUtility.FullPath(expr);
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.FileDrop, s);
                dataObject.SetData(DataFormats.Bitmap, pictureBox.Image);
                Clipboard.SetDataObject(dataObject);

                // Send "ctrl+v" to QQ window
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

                // Update recently used exprs
                List<Expr> recentlyUsedExprs = SettingUtility.getRecentlyUsedExprs();
                for (int i = 0; i < recentlyUsedExprs.Count; i++)
                {
                    if (recentlyUsedExprs[i].id == expr.id)
                    {
                        recentlyUsedExprs.Remove(recentlyUsedExprs[i]);
                    }
                }
                recentlyUsedExprs.Insert(0, expr);
                SettingUtility.setRecentlyUsedExprs(recentlyUsedExprs);
                if (SettingUtility.exprsDisplayer != null)
                {
                    SettingUtility.exprsDisplayer.loadRecentlyUsedExprs();
                }
            }
        }
    }
}
