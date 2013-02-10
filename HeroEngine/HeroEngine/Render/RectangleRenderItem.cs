using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace HeroEngine.Render
{
    class RectangleRenderItem
    {
        public RectangleRenderItem(Texture2D Texture, Rectangle DestinationRectangle, Color Colour)
        {
            this.Texture = Texture;
            this.DestinationRectangle = DestinationRectangle;
            this.Colour = Colour;
        }

        public Texture2D Texture;

        public Rectangle DestinationRectangle;

        public Rectangle SourceRectangle;

        public Color Colour;

        public float Rotation = 0f;

        public float LayerDepth = 0;

        public Vector2 Origin = new Vector2(0,0);

        public SpriteEffects SpriteEffects = SpriteEffects.None;

    }
}
