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
            /*
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("发送至QQ窗口");
            cm.MenuItems.Add("发送至微信窗口");
            cm.MenuItems.Add("-");
            cm.MenuItems[0].Click += ExprDisplayer_SendToQQ;
            cm.MenuItems[1].Click += ExprDisplayer_SendToWeChat;

            
            List<TagContent> tagContents = SettingUtility.getUsedTags();
            Console.Out.WriteLine(tagContents);
            addTagMenuItem.MenuItems.Add("默认");
            cm.MenuItems.Add(addTagMenuItem);*/

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
