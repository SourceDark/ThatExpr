using LaGeBiaoQing.Model;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Utility;
using LaGeBiaoQing.View.InfoExtended;
using LaGeBiaoQing.View.PictureBoxs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.Menu
{
    class ExprDisplayerRightClickMenuFactory
    {
        private static ContextMenu contextMenu = null;

        public static ContextMenu getInstance()
        {
            if (contextMenu == null)
            {
                contextMenu = new ContextMenu();
                regenerateMenu();
            }
            return contextMenu;
        }

        private static void regenerateMenu()
        {
            contextMenu.MenuItems.Clear();
            contextMenu.MenuItems.Add("发送至QQ窗口");
            contextMenu.MenuItems.Add("发送至微信窗口");
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems[0].Click += sendToQQ;
            contextMenu.MenuItems[1].Click += sendToWeChat;

            MenuItem addTagMenuItem = new MenuItem("收藏至");
            addTagMenuItem.MenuItems.Add("新标签");
            addTagMenuItem.MenuItems.Add("-");

            MenuItem addDefaultTagMenuItem = new MenuItem("默认");
            addDefaultTagMenuItem.Click += tryCollectDefault;
            addTagMenuItem.MenuItems.Add(addDefaultTagMenuItem);

            List<TagContent> tagContents = SettingUtility.getUsedTags();
            foreach (TagContent tagContent in tagContents)
            {
                if (tagContent.content.Length > 0)
                {
                    InfoExtendedMenuItem infoExtendedMenuItem = new InfoExtendedMenuItem(tagContent.content, tagContent);
                    infoExtendedMenuItem.Click += tryCollect;
                    addTagMenuItem.MenuItems.Add(infoExtendedMenuItem);
                }
            }
            contextMenu.MenuItems.Add(addTagMenuItem);

            contextMenu.MenuItems.Add("-");

            MenuItem removeTagMenuItem = new MenuItem("取消收藏");
            removeTagMenuItem.Click += tryRemove;
            contextMenu.MenuItems.Add(removeTagMenuItem);
        }

        private static void tryCollectDefault(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu contextMenu = menuItem.GetContextMenu() as ContextMenu;
            ExprDisplayer exprDisplayer = contextMenu.SourceControl as ExprDisplayer;
            CollectionService.createCollection("", exprDisplayer.expr.id);
        }

        private static void tryCollect(object sender, EventArgs e)
        {
            InfoExtendedMenuItem infoExtendedMenuItem = sender as InfoExtendedMenuItem;
            ContextMenu cms = infoExtendedMenuItem.GetContextMenu() as ContextMenu;
            ExprDisplayer exprDisplayer = cms.SourceControl as ExprDisplayer;
            TagContent tagContent = infoExtendedMenuItem.info as TagContent;
            CollectionService.createCollection(tagContent.content, exprDisplayer.expr.id);
        }

        private static void tryRemove(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu contextMenu = menuItem.GetContextMenu() as ContextMenu;
            ExprDisplayer exprDisplayer = contextMenu.SourceControl as ExprDisplayer;
            CollectionService.removeCollection(exprDisplayer.expr.id);
        }


        static private void sendToQQ(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu cms = menuItem.GetContextMenu() as ContextMenu;
            ExprDisplayer exprDisplayer = cms.SourceControl as ExprDisplayer;
            WindowsUtility.sendTo(exprDisplayer.expr, exprDisplayer.Image, WindowType.WindowTypeQQ);
        }

        static private void sendToWeChat(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu cms = menuItem.GetContextMenu() as ContextMenu;
            ExprDisplayer exprDisplayer = cms.SourceControl as ExprDisplayer;
            WindowsUtility.sendTo(exprDisplayer.expr, exprDisplayer.Image, WindowType.WindowTypeWeChat);
        }
    }
}
