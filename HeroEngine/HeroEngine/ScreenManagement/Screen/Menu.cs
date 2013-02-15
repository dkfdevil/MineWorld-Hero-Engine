using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace HeroEngine.Screen
{
    class Menu
    {
        List<string> MenuItems = new List<string>();
        public List<Texture2D> MenuItemThumbs = new List<Texture2D>();
        public SpriteFont sprfont;
        public int selected = 0;
        Rectangle thumbplace = new Rectangle(500, 10, 180, 460);
        Vector2[] points = new Vector2[5];
        /// <summary>
        /// Draws the menu for the engine. You must specify thumbnails for each item.
        /// </summary>
        /// <param name="sprbtch"></param>
        /// <param name="startlocation"></param>
        /// <param name="Colour"></param>
        /// <param name="spacing"></param>
        /// 

        public Menu()
        {
            points[4] = new Vector2(0, 0);
            points[3] = new Vector2(25, 100);
            points[2] = new Vector2(75, 200);
            points[1] = new Vector2(125, 300);
            points[0] = new Vector2(150, 400);
        }

        public void DrawMenu(SpriteBatch sprbtch, Vector2 startlocation, Cursor cursor, Rectangle[] holders, Color Colour, int spacing = 50)
        {
                sprbtch.Draw(MenuItemThumbs[selected], thumbplace, Color.White);
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    int position = i - selected;
                    if (position <= -1)
                    {
                        position = 4;
                    }
                    sprbtch.DrawString(sprfont, MenuItems[i], points[position], Colour, 0, new Vector2(0, 0), points[position].Y / 100, SpriteEffects.None, 0);       
                }
        }

        public void AddMenuItem(string item)
        {
            MenuItems.Add(item);
        }

        public void SelectedItemDown()
        {
            selected--;
            if (selected < 0)
            {
                selected++;
            }
        }  

        public void SelectedItemUp()
        {
            selected++;
            if (selected > (MenuItems.Count - 1))
            {
                selected--;
            }
        }

        public bool HoverOverSprite(Cursor cursor, Rectangle rect)
        {
            bool result;
            cursor.Location.Intersects(ref rect, out result);
            return result;
        }

        public void MenuItemChosen()
        {

        }
    }
}
