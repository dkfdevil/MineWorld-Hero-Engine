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
namespace HeroEngine.UIFramework
{
    enum BackgroundDirection
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
    }

    class BackgroundSlideShow
    {
        List<Texture2D> backgrounds;
        float speed;
        int transitiontime;

        int starttime;
        int time = 0;
        float msecondperpixel;
        float distance = 0;
        int distance_so_far = 0;
        DateTime lastupdate = DateTime.Now;

        int back_number;
        GameTime gt;
        Vector2 position;
        Rectangle fill;
        SpriteBatch sb;
        BackgroundDirection direction;
        public BackgroundSlideShow(List<Texture2D> bgs, GameTime gametime, SpriteBatch spritebatch, float spd = 20f, int time = 15)
        {
            backgrounds = bgs;
            speed = spd;
            gt = gametime;
            transitiontime = time;
            sb = spritebatch;
            distance = speed * time;
            fill = sb.GraphicsDevice.Viewport.Bounds;
            fill.Width *= 3;
            fill.Height*= 3;
            SetupScale();
        }

        public void Update()
        {
            if (DateTime.Now.TimeOfDay.TotalMilliseconds - lastupdate.TimeOfDay.TotalMilliseconds > speed)
            {
                lastupdate = DateTime.Now;
                if (distance != distance_so_far)
                {
                    switch (direction)
                    {
                        case BackgroundDirection.UP:
                            position.Y -= 1;
                            break;
                        case BackgroundDirection.DOWN:
                            position.Y += 1;
                            break;
                        case BackgroundDirection.LEFT:
                            position.X -= 1;
                            break;
                        case BackgroundDirection.RIGHT:
                            position.X += 1;
                            break;
                        default:
                            break;
                    }
                    distance_so_far++;
                    fill.X = (int)position.X;
                    fill.Y = (int)position.Y;
                }
                else
                {
                    SetupScale();
                }



            }
        }

        public void Draw()
        {
            Color color = new Color(255, 255, 255);
            if (distance - distance_so_far > distance - 200) //Fade in
            {
                color = new Color(255, 255, 255, distance_so_far / 25.5f);
            }

            if (distance - distance_so_far < 200) //Fade out
            {
                color = new Color(255, 255, 255, (distance - distance_so_far) / 22.5f);
            }

            sb.Draw(backgrounds[back_number], fill, color);
        }

        public void GetRandomDirection()
        {
            direction = (BackgroundDirection)new Random().Next(0, 3);
            back_number = new Random().Next(0, backgrounds.Count() - 1);
        }

        public void SetupScale()
        {
            position.X  = -fill.Width / 3;
            position.Y  = -fill.Height / 3;
            GetRandomDirection();
            distance_so_far = 0;
            switch (direction)
            {
                case BackgroundDirection.UP:
                    position.Y += distance;
                    break;
                case BackgroundDirection.DOWN:
                    position.Y -= distance;
                    break;
                case BackgroundDirection.LEFT:
                    position.X += distance;
                    break;
                case BackgroundDirection.RIGHT:
                    position.X -= distance;
                    break;
                default:
                    break;
            }
            starttime = (int)DateTime.Now.TimeOfDay.TotalSeconds;
            this.time = starttime + time;
        }
    
    }
}
