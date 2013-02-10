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
using HeroEngine.Objects;
namespace HeroEngine.Render
{
    class Camera
    {
        public Point CameraPosOnWorld;
        public Vector2 speed_buffer = new Vector2();
        public float movex = 0;
        public float movey = 0;
        public float camspeed = 1f;

        public Camera(Game game)
        {
        }

        public void CameraMovement(Game game)
        {
                    
        }

        public void SnapToEnt(Entity ent,Game game)
        {
            ent.IsTracked = true;
            speed_buffer = ent.speed_buffer;
            CameraPosOnWorld.X = ent.Marker.x - (EngineLimit.MaxTilesScreenWidth / 2);
            CameraPosOnWorld.Y = ent.Marker.y - (EngineLimit.MaxTilesScreenHeight / 2);
        }

        public Rectangle GetBounds()
        {
            
            Rectangle rect = Rectangle.Empty;
            rect.X = CameraPosOnWorld.X;
            rect.Y = CameraPosOnWorld.Y;

            rect.Width = EngineLimit.MaxTilesScreenWidth;
            rect.Height = EngineLimit.MaxTilesScreenHeight;
            return rect;
        }
        
    }
}
