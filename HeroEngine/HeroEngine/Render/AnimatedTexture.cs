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
using HeroEngine.Sound;
using HeroEngine.LevelEditing;
using HeroEngine.Render;
namespace HeroEngine.Render
{
    class AnimatedTexture
    {
        public Texture2D sheet;
        public Rectangle tilesize;
        int frames;
        int speed;
        int miliseconds = 0;
        int framenumber = 1;
        /// <summary>
        /// A animated texture that requires a good few methods to be effective.
        /// </summary>
        /// <param name="_sheet">The whole texture used by the animation.</param>
        /// <param name="_tilesize">How big is each frame</param>
        /// <param name="_frames">How many frames are there</param>
        /// <param name="Speed">How fast should it play, in ms.</param>
        public AnimatedTexture(Texture2D _sheet, Rectangle _tilesize, int _frames, int Speed)
        {
            sheet = _sheet;
            tilesize = _tilesize;
            frames = _frames;
            speed = Speed;
        }
        /// <summary>
        /// Gets the bounds rectangle for masking.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetSourceRectange()
        {
            return new Rectangle(tilesize.Width * (framenumber - 1), 0, tilesize.Width, tilesize.Height);
        }

        public void StepFrameForward()
        {
            if ((int)DateTime.Now.TimeOfDay.TotalMilliseconds - miliseconds > speed)
            {
                if (framenumber + 1 <= frames)
                {
                    framenumber++;
                }
                else
                {
                    framenumber = 1;
                }
                miliseconds = (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            }
        }

        public void StepFrameBackward()
        {
            if ((int)DateTime.Now.TimeOfDay.TotalMilliseconds - miliseconds > speed)
            {
                if (framenumber - 1 >= 0)
                {
                    framenumber--;
                }
                else
                {
                    framenumber = frames;
                }
                miliseconds = (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            }
        }
    }
}
