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
using HeroEngine.Resources.Classes;

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
            videos = new ResourceCache<Video>(manager);
        }

        public void AddResource(GameResourceTypes type, string filepath, string name)
        {
            switch (type)
            {
                case GameResourceTypes.Texture2D:
                    {
                        textures.AddResource(manager.Load<Texture2D>(filepath), name);
                        break;
                    }
                case GameResourceTypes.AnimatedTexture:
                    {
                        //Not implented
                        break;
                    }
                case GameResourceTypes.Fonts:
                    {
                        fonts.AddResource(manager.Load<SpriteFont>(filepath), name);
                        break;
                    }
                case GameResourceTypes.Sounds:
                    {
                        sounds.AddResource(manager.Load<SoundEffect>(filepath), name);
                        break;
                    }
                case GameResourceTypes.Music:
                    {
                        music.AddResource(manager.Load<Song>(filepath), name);
                        break;
                    }
                case GameResourceTypes.Video:
                    {
                        videos.AddResource(manager.Load<Video>(filepath), name);
                        break;
                    }
                default:
                    {
                        throw UnkownType;
                    }
            }
        }

        public void RemoveResource(GameResourceTypes type, string name)
        {
            switch (type)
            {
                case GameResourceTypes.Texture2D:
                    {
                        textures.RemoveResource(name);
                        break;
                    }
                case GameResourceTypes.AnimatedTexture:
                    {
                        //Not implented
                        break;
                    }
                case GameResourceTypes.Fonts:
                    {
                        fonts.RemoveResource(name);
                        break;
                    }
                case GameResourceTypes.Sounds:
                    {
                        sounds.RemoveResource(name);
                        break;
                    }
                case GameResourceTypes.Music:
                    {
                        music.RemoveResource(name);
                        break;
                    }
                case GameResourceTypes.Video:
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

        public object GetResource(GameResourceTypes type, string name)
        {
            switch (type)
            {
                case GameResourceTypes.Texture2D:
                    {
                        return textures.GetResource(name);
                    }
                case GameResourceTypes.AnimatedTexture:
                    {
                        return null;
                    }
                case GameResourceTypes.Fonts:
                    {
                        return fonts.GetResource(name);
                    }
                case GameResourceTypes.Sounds:
                    {
                        return sounds.GetResource(name);
                    }
                case GameResourceTypes.Music:
                    {
                        return music.GetResource(name);
                    }
                case GameResourceTypes.Video:
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
