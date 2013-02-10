//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using C3.XNA;
//using HeroEngine.UIFramework;
//namespace HeroEngine.Render
//{
//    class Console
//    {
//        public bool On;
//        public string command = "";
//        SpriteFont Font;
//        public List<string> log = new List<string>();
//        public Console(SpriteFont font)
//        {
//            Font = font;
//        }

//        public void Update()
//        {
//            if (On)
//            {
//                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
//                {
//                    TakeCommand();
//                    log.Add(command);
//                    command = "";
//                }
//                else
//                {
//                    command +=;
//                }
//            }
//        }

//        public void Draw(SpriteBatch sb,Viewport screen)
//        {
//            if (On)
//            {
//                sb.FillRectangle(
//                    new Rectangle(0, 0, screen.Width, screen.Height / 12),
//                   new Color(255,255,255,80));
//                sb.DrawString(Font, command, new Vector2(0, screen.Height / 12 - 15), Color.White);
//                int x = 5;
//                foreach (var item in log)
//                {
//                    if ((screen.Height / 12) - x > -1)
//                    {
//                        sb.DrawString(Font, item, new Vector2(0, (screen.Height / 12) - x), Color.White);
//                        x += 5;
//                    }
//                    else
//                    {
//                        return;
//                    }

//                }
//            }
//        }

//        public void TakeCommand()
//        {

//        }
//    }
//}
