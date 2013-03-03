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
using HeroEngine.Input;
using HeroEngine.UserInterface.Items;
namespace HeroEngine.Screen
{
    public class Cursor
    {
        int screenHeight;
        int screenWidth;
        public Texture2D tex;
        public Rectangle texturesize;
        public Vector2 Location;

        public Cursor(Texture2D cursor)
        {
            tex = cursor;
            texturesize = new Rectangle(0, 0, GlobalMenuConstants.CursorSize, GlobalMenuConstants.CursorSize);
        }

        public void Update(InputHelper input)
        {
            Location.X = input.MousePosition.X;
            Location.Y = input.MousePosition.Y;
        }

        public void Draw(SpriteBatch sprbtch)
        {
            sprbtch.Begin();
            sprbtch.Draw(tex, Location, texturesize, Color.White);
            sprbtch.End();
        }
    }
}
