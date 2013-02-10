using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.ScreenManagement;
using HeroEngine.UIFramework.Menus;
using HeroEngine.Operations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeroEngine.Screen;
using HeroEngine.ScreenManagement.Screens;
namespace HeroEngine.UIFramework
{
    class MenuManager
    {
        MenuScreen parent_screen;
        MouseInputManager manager;
        List<MenuItem> items = new List<MenuItem>();
        public Point Start_at;
        int ypos;
        bool isHidden = true;
        int itemid = 1;
        public int menuid;
        public MenuManager(MenuScreen parent, Point startat, int MENUID) 
        {
            parent_screen = parent;
            manager = new MouseInputManager();
            Start_at = startat;
            menuid = MENUID;
        }

        public void ResetRes(Point startat)
        {
            Start_at = startat;
            ypos = 0;
            foreach (var item in items)
            {
            item.Position = new Rectangle(Start_at.X, ypos + Start_at.Y, 0, 0);
            ypos += items[itemid - 2].Position.Height + 50;
            }
        }

        //Property
        public MenuScreen parent_menu
        {
            get { return parent_screen; }
        }

        public void Update()
        {
            if (!isHidden)
            {
                //Update Mouse
                manager.Update();
                //Update items
                foreach (var item in items)
                {
                    if (!item.isDisabled)
                        item.Update(parent_menu.cursor, manager);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if(!isHidden)
            {
                foreach (var item in items)
                {
                    item.Draw(sb);
                }
            }
        }

        public void AddItem(MenuItem item)
        {

            MenuItem Item = item;
            Item.Position = new Rectangle(Start_at.X, ypos + Start_at.Y, 0, 0);
            Item.ItemID = itemid;
            items.Add(Item);
            itemid++;
            ypos += items[itemid - 2].Position.Height + 50;
            
        }

        public void DisableItem(int itemindex)
        {
            items[itemindex].Disable();
        }

        public void EnableItem(int itemindex)
        {
            items[itemindex].Enable();
        }

        public void RemoveItem(int itemindex)
        {
            items.RemoveAt(itemindex);
        }

        public string GetItemValue(int itemindex)
        {
           return items[itemindex].ReturnState();
        }

        public List<MenuItem> GetItems()
        {
            return items;
        }

        public void Show()
        {
            isHidden = false;
        }

        public void Hide()
        {
            isHidden = true;
        }
    }
}
