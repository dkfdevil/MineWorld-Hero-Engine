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
using C3.XNA;

namespace HeroEngine.Screen
{   
    /// <summary>
    /// This class can be used to create a overlay with a specified Alpha. Useful for loading screens and darkening the play area.
    /// </summary>
    /// 
       
    class Overlay
    {   
        /// <summary>
        /// Size of screen area.
        /// </summary>
        public Rectangle screensize;
        /// <summary>
        /// Colour of the screens overlay.
        /// </summary>
        public Color colour = Color.White;
        /// <summary>
        /// The alpha of the Overlay, if not specifed in the Colour.
        /// </summary>
        public int Alpha;
        /// <summary>
        /// The Texture of the overlay, if applicable.
        /// </summary>
        public Texture2D Texture;
        public bool IsUsingTexture = false;
        public Overlay(Color color, Rectangle Size, int Alpha = 255 ,Texture2D tex = null)
        {
           if(tex != null)
           {
               Texture = tex;
               IsUsingTexture = true;
           }
           colour = color;
           colour.A = (byte)Alpha;
           screensize = Size;
        }

        public void DrawOverlay(SpriteBatch sb)
        {
            if (IsUsingTexture)
            {
                sb.Draw(Texture, screensize,null,colour);
            }
            else
            {
                sb.FillRectangle(screensize, colour);
            }
        }
    }
}
