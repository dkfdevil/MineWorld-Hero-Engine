using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeroEngine.UserInterface.Items
{
    public class BackGround
    {
        Texture2D texture;
        Rectangle rectangle;

        public BackGround(Texture2D newTexture, int screenHeight, int screenWidth)
        {
            texture = newTexture;
            rectangle = new Rectangle(0, 0, screenWidth, screenHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
