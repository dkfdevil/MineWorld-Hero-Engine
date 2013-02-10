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
using HeroEngine.Screen;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.LevelEditing;
namespace HeroEngine.ScreenManagement.Screens.MenuObjects
{
    /// <summary>
    /// A clickable object on the menu that has an action which can be checked for by the methods.
    /// </summary>
    class MenuItem
    {
        string name; 
        bool selected = false;
        ClickButton button;
        int ID;
        public MenuItem(string Name, int id,ClickButton Button)
        {
            name = Name;
            button = Button;
            ID = id;
        }

        public void DrawItem(SpriteBatch sb)
        {
            button.Draw(sb);
        }

        public bool Selected()
        {
            if (button.state == ClickButtonState.Selected || button.state == ClickButtonState.PressedIn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Clicked()
        {
           return button.ClickListener();
        }
    }
}
