using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using HeroEngine.LevelEditing;
using HeroEngine.Render;
using HeroEngine.CoreGame;

namespace HeroEngine.Objects
{
    class Entity
    {
        public Entity(string name, EntityManager manager, SpriteBatch spriteBatch, Camera cam)
        {
            Name = name;
            EntityManager = manager;
            SpriteBatch = spriteBatch;
            camera = cam;
        }
        public string Name;
        public int Health;
        public int Armour;
        public Camera camera;
        public EntityManager EntityManager;
        public SpriteBatch SpriteBatch;
        public Vector2 speed_buffer = new Vector2();
        public TileMarker Marker = new TileMarker(0,0);
        public Texture2D current_tex;
        public Rectangle mask;
        public float rotation = 0;
        public Vector2 origin = Vector2.Zero;
        public Rectangle Size = new Rectangle();
        public bool IsDrawn = true;
        public bool AlwaysDrawn = false;
        public bool IsTracked = false;
        public Rectangle pos = new Rectangle();
        //Entity might need to do stuff first before it can be used
        public virtual void Initialize()
        {
            System.Diagnostics.Debug.WriteLine(Name + " has been initialized");
            if (current_tex == null)
            {
                IsDrawn = false;
                System.Diagnostics.Debug.WriteLine(Name + ": No Texture Loaded. Draw must be overridden");
            }
        }

        //Think for entity.
        public virtual void Think()
        {
            if (!IsDrawn) { return; }
            Point TileSize = new Point((int)EngineLimit.TileScaledX, (int)EngineLimit.TileScaledY);
            Point precise = new Point((int)(speed_buffer.X * ((float)TileSize.X / 20)), (int)(speed_buffer.Y * ((float)TileSize.Y / 20)));
            pos.X = (int)((Marker.x - camera.CameraPosOnWorld.X) * EngineLimit.TileScaledX);
            pos.Y = (int)((Marker.y - camera.CameraPosOnWorld.Y) * EngineLimit.TileScaledY);

            if (!IsTracked)
            {
                pos.X += precise.X - (TileSize.X / 2);
                pos.Y += precise.Y - (TileSize.Y / 2);
            }
            else
            {
                //Snap the camera after every movement. This means no jitter.
                camera.SnapToEnt(this, EntityManager.GameScreen.screenManager.Game);
            }

            pos.Width = Size.Width;
            pos.Height = Size.Height;
        }

        //Draw the entity.
        public virtual void Draw()
        {



            
            //If the texture is still null, use the ERROR one.
            if (current_tex == null) { current_tex = GameResources.textures.GetResource("NOTEX"); };

            if (mask != null)
            {
                SpriteBatch.Draw(current_tex, pos, mask, Color.White, rotation, origin ,SpriteEffects.None,0);
            }
            else
            {
                SpriteBatch.Draw(current_tex, pos, Color.White);
            }
        }

        public virtual void Collision()
        {
        }
    }
}
