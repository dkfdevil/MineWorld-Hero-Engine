using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using HeroEngine.Input;

namespace HeroEngine.UserInterface.Items
{
    public class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        bool down;
        public bool IsClicked;


        public Button(Texture2D newTexture, int screenHeight, int screenWidth)
        {
            texture = newTexture;

            // ScreenWidth = 800, ScreenHeight = 600
            // ImageWidth = 100, ImgHeight = 20
            size = new Vector2(screenWidth / 8, screenHeight / 30);
        }

        public void update(InputHelper input)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRetangle = new Rectangle((int)input.MousePosition.X, (int)input.MousePosition.Y, 15, 15);

            if (mouseRetangle.Intersects(rectangle))
            {
                //This creates the fading in/out effect
                if (colour.A == 255)
                {
                    down = false;
                }
                if (colour.A == 0)
                {
                    down = true;
                }
                if (down == true)
                {
                    colour.A += 3;
                }
                else
                {
                    colour.A -= 3;
                }
                if (input.IsNewPress(MouseButtons.LeftButton))
                {
                    IsClicked = true;
                }
            }
            else
            {
                colour.A = 255;
                IsClicked = false;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, colour);
            spriteBatch.End();
        }
    }
}
