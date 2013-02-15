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
using HeroEngine.Render;

namespace HeroEngine
{
    /// <summary>
    /// Class responsible for the global variables that shouldn't ever be touched by users. Even developers run scared.
    /// </summary>
    class EngineLimit
    {
        public const int TotalMapAreaWidth = 256; //Muhahaha, change this to change everything in the engine. Bugs WILL OCCUR.
        public const int TotalMapAreaHeight = 256;
        public const int TileSize = 20;
        public const int MinFetch = 5;
        public const int MaxFetch = 15;
        public const int MaxTilesScreenWidth = 30;
        public const int MaxTilesScreenHeight = 20;
        public const int MinBeachCoast = 5;
        public const int MaxBeachCoast = 10;

        public static float TileScaleX = 0;
        public static float TileScaleY = 0;

        public static float TileScaledX = 0;
        public static float TileScaledY = 0;

        public static int HalfScreenWidth = 0;
        public static int HalfScreenHeight = 0;

        public const string VersionNumber = "Development Build ";
        public static void SetTileScale()
        {
            TileScaleX = (Resolution._VWidth / (float)TileSize) / (float)MaxTilesScreenWidth;
            TileScaleY = (Resolution._VHeight / (float)TileSize) / (float)MaxTilesScreenHeight;

            HalfScreenWidth = Resolution._VWidth / 2;
            HalfScreenHeight = Resolution._VHeight / 2;
            TileScaledX = TileScaleX * TileSize;
            TileScaledY = TileScaleY * TileSize;
        }


    }
}
