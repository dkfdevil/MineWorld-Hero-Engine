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
    class Cursor
    {
        public Color Colour = Color.Red;
        public Rectangle Size = new Rectangle(0, 0, 15, 15);
        public Texture2D tex;
        public Rectangle Location;
        public void DrawCursor(SpriteBatch sprbtch)
        {
            Location = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, Size.Width, Size.Height);
            sprbtch.Draw(tex, Location, Colour);
        }
    }
}
