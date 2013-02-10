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
using HeroEngine.Sound;
using HeroEngine.LevelEditing;
using HeroEngine.Render;
using HeroEngine.Operations;
using System.IO;
using HeroEngine.Objects;
using HeroEngine.UIFramework;
using HeroEngine.Screen;
namespace HeroEngine.UIFramework.Menus
{
    class MenuItem
    {
        MenuManager Manager;
        Color[] colors;
        public Rectangle Position;
        public bool isDisabled;
        public int ItemID;


        public MenuItem(MenuManager manager,Color colorA, Color colorB,Rectangle xypos)
        {
            Manager = manager;
            colors = new Color[2];
            colors[0] = colorA;
            colors[1] = colorB;
            Position = new Rectangle(xypos.X, xypos.Y, 0, 0);
        }

        public virtual void Update(Cursor cursor, MouseInputManager mouse_man)
        {
            
        }

        public virtual void Draw(SpriteBatch sb)
        {

        }

        public void Disable()
        {
            isDisabled = true;
        }

        public void Enable()
        {
            isDisabled = false;
        }

        public virtual string ReturnState()
        {
            return this.ReturnState();
        }
    }
}
