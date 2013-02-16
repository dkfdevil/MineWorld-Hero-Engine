using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using HeroEngine.Render;

namespace HeroEngine.CoreGame
{   
    /// <summary>
    /// Holds all the resources for the engine. Saves time, space and also a huge amount of code searching for content.
    /// </summary>
    public class GameResources
    {
        ContentManager manager; //Content manager responsible for all game files.

        public ResourceCache<Texture2D> textures;//Caches.
        //public ResourceCache<AnimatedTexture> animatedtextures;//Caches.
        public ResourceCache<SpriteFont> fonts;
        public ResourceCache<SoundEffect> sounds;
        public ResourceCache<Song> music;
        public ResourceCache<Video> videos;

        Exception UnkownType = new Exception("The type you were trying to load is unkown");

        /// <summary>
        /// Load a content manager that shouldn't be closed during game time.
        /// </summary>
        /// <param name="mnger">The content manager to use.</param>
        public GameResources(ContentManager mnger)
        {
            manager = mnger;

            textures = new ResourceCache<Texture2D>(manager);
            //animatedtextures = new ResourceCache<AnimatedTexture>(manager);
            fonts = new ResourceCache<SpriteFont>(manager);
            sounds = new ResourceCache<SoundEffect>(manager);
            music = new ResourceCache<Song>(manager);
        }

        public void AddResource(string type,string filepath,string name)
        {
            switch (type)
            {
                case "texture2d":
                    {
                        textures.AddResource(manager.Load<Texture2D>(filepath), name);
                        break;
                    }
                case "animatetexture":
                    {
                        //Not implented
                        break;
                    }
                case "fonts":
                    {
                        fonts.AddResource(manager.Load<SpriteFont>(filepath), name);
                        break;
                    }
                case "sounds":
                    {
                        sounds.AddResource(manager.Load<SoundEffect>(filepath), name);
                        break;
                    }
                case "music":
                    {
                        music.AddResource(manager.Load<Song>(filepath), name);
                        break;
                    }
                case "videos":
                    {
                        videos.AddResource(manager.Load<Video>(filepath + "/" + name + ".wmv"), name);
                        break;
                    }
                default:
                    {
                        throw UnkownType;
                    }
            }
        }

        public void RemoveResource(string type, string name)
        {
            switch (type)
            {
                case "texture2d":
                    {
                        textures.RemoveResource(name);
                        break;
                    }
                case "animatetexture":
                    {
                        //Not implented
                        break;
                    }
                case "fonts":
                    {
                        fonts.RemoveResource(name);
                        break;
                    }
                case "sounds":
                    {
                        sounds.RemoveResource(name);
                        break;
                    }
                case "music":
                    {
                        music.RemoveResource(name);
                        break;
                    }
                case "videos":
                    {
                        videos.RemoveResource(name);
                        break;
                    }
                default:
                    {
                        throw UnkownType;
                    }
            }
        }

        public object GetResource(string type, string name)
        {
            switch (type)
            {
                case "texture2d":
                    {
                        return textures.GetResource(name);
                    }
                case "animatetexture":
                    {
                        return null;
                    }
                case "fonts":
                    {
                        return fonts.GetResource(name);
                    }
                case "sounds":
                    {
                        return sounds.GetResource(name);
                    }
                case "music":
                    {
                        return music.GetResource(name);
                    }
                case "videos":
                    {
                        return videos.GetResource(name);
                    }
                default:
                    {
                        throw UnkownType;
                    }
            }
        }
    }
}
