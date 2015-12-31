using LaGeBiaoQing.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LaGeBiaoQing.Utility
{
    enum WindowType { WindowTypeQQ, WindowTypeWeChat }

    class WindowsUtility
    {
        
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public static void sendTo(Expr expr, Image image, WindowType windowType)
        {
            copyToClipBoard(expr, image);

            if (windowType == WindowType.WindowTypeQQ) { 
                Process qqProcess = Process.GetProcessesByName("QQ").FirstOrDefault();
                if (qqProcess != null)
                {
                    IntPtr qqHandle = qqProcess.MainWindowHandle;
                    Console.Out.WriteLine(qqProcess.ProcessName);
                    SetForegroundWindow(qqHandle);
                    SendKeys.SendWait("^V");
                }
                else
                {
                    MessageBox.Show("找不到QQ窗口", "发送失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (windowType == WindowType.WindowTypeWeChat)
            {
                Process weChatProcess = Process.GetProcessesByName("WeChat").FirstOrDefault();
                if (weChatProcess != null)
                {
                    IntPtr weChatHandle = weChatProcess.MainWindowHandle;
                    Console.Out.WriteLine(weChatProcess.ProcessName);
                    SetForegroundWindow(weChatHandle);
                    SendKeys.SendWait("^V");
                }
                else
                {
                    MessageBox.Show("找不到微信窗口", "发送失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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

        public static void copyToClipBoard(Expr expr, Image image)
        {
            string[] s = new string[1];
            s[0] = FileUtility.FullPath(expr);
            DataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.FileDrop, s);
            dataObject.SetImage(image);
            Clipboard.SetDataObject(dataObject);
        }
    }
}
