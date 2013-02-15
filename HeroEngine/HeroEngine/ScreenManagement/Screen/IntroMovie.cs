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
namespace HeroEngine.Screen
{
    class IntroMovie
    {
        public Video video;
        public Rectangle AreaOfWindow;
        VideoPlayer player = new VideoPlayer();
        public bool playing = false;
        public void DrawVideo(SpriteBatch sprbtch)
        {
            if (player.State != MediaState.Playing)
            {
                player.Play(video);
            }
            Texture2D videoTex = player.GetTexture();
            sprbtch.Draw(videoTex, AreaOfWindow, Color.White);
        }


        public bool IsPlaying()
        {
            if (player.State == MediaState.Playing)
                playing = true;
            else
                playing = false;
            return playing;
        }

        public bool FinishedPlaying()
        {
            if (player.State == MediaState.Playing)
            {
                if (player.PlayPosition == player.Video.Duration)
                 return true;
            }
            return false;
        }

        public void Unload()
        {
            player.Dispose();
        }
    }
}
