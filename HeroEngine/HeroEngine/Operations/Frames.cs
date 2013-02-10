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
namespace HeroEngine.Operations
{
    
    public class Frames
    {
        public static string  GetFramesPerSecond(GameTime gt)
        {
            if (gt.ElapsedGameTime.Milliseconds == 0)
            {
                return "0";
            }
          int frames = 1000 / gt.ElapsedGameTime.Milliseconds;
          return frames.ToString();
        }
    }
}
