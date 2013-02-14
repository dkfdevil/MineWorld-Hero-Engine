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
    static class GameResources
    {
        static ContentManager manager; //Content manager responsible for all game files.
        public static ResourceCache<Texture2D> textures;//Caches.
        public static ResourceCache<AnimatedTexture> animatedtextures;//Caches.
        public static ResourceCache<SpriteFont> fonts;
        public static ResourceCache<SoundEffect> sounds;
        public static ResourceCache<Song> music;
        /// <summary>
        /// Load a content manager that shouldn't be closed during game time.
        /// </summary>
        /// <param name="mnger">The content manager to use.</param>
        public static void SetManager(ContentManager mnger)
        {
            manager = mnger;
        }
    }
}
