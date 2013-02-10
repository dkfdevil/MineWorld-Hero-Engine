using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.Render;
using HeroEngine.ScreenManagement.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeroEngine.LevelEditing;
namespace HeroEngine.Objects
{ 
    /// <summary>
    /// This class is used to manage all the entites in the world. Should they be drawn, tell them to think and get positions.
    /// </summary>
    class EntityManager
    {
        public EntityManager(Camera camera,GameScreen gameScreen, SpriteBatch sb, Map map)
        {
            Camera = camera;
            GameScreen = gameScreen;
            SpriteBatch = sb;
            currentMap = map;
        }

        public Camera Camera; //Get the game camera.
        public GameScreen GameScreen; //Get the parented game screen.
        public SpriteBatch SpriteBatch;
        public Map currentMap;
        List<Entity> ents = new List<Entity>();
        //Add a entity to the manager. Used in spawning.
        public void AddEntity(Entity ent)
        {
            ents.Add(ent);
            ent.Initialize();
        }

        //Get rid of an entity. Best used by the same entitiy.
        public void RemoveEntity(Entity ent)
        {
            ents.Remove(ent);
        }

        //Think for all entitys.
        public void Think()
        {
            foreach (var item in ents)
            {
                item.Think();
            }
        }

        //Draw the entitys.
        public void Draw()
        {
            foreach (var item in ents)
            {
                SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                if (IsVisible(item)) { item.Draw(); }
                SpriteBatch.End();
                
            }
        }

        public bool IsVisible(Entity item)
        {
            Rectangle bounds = Camera.GetBounds();
            if (item.AlwaysDrawn) { return true; }
            //Is X less than bounds?
            if (item.Marker.x < bounds.X)
            {
                return false;
            }

            //Is X more than bounds?
            if (item.Marker.x > bounds.Width + bounds.X)
            {
                return false;
            }

            //Is Y less than bounds?
            if (item.Marker.y < bounds.Y)
            {
                return false;
            }

            //Is Y more than bounds?
            if (item.Marker.y > bounds.Height + bounds.Y)
            {
                return false;
            }

            return true;
        }
    }
}
