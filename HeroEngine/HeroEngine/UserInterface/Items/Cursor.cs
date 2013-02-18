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
namespace HeroEngine.Screen
{
    public class Cursor
    {
        public int cursorsize = 15;
        public Rectangle rectangle;
        public Texture2D tex;
        public Rectangle texturesize;
        public Vector2 Location;

        public Cursor(Texture2D cursor)
        {
            tex = cursor;
            texturesize = new Rectangle(0, 0, cursorsize, cursorsize);
        }

        public void Update(InputHelper input)
        {
            Location.X = input.MousePosition.X;
            Location.Y = input.MousePosition.Y;

            rectangle = new Rectangle((int)Location.X, (int)Location.Y, (int)Location.X + cursorsize, (int)Location.Y + cursorsize);
        }

        public void Draw(SpriteBatch sprbtch)
        {
            sprbtch.Begin();
            sprbtch.Draw(tex, Location, texturesize, Color.White);
            sprbtch.End();
        }
    }
}
