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
using System.IO;
using System.Diagnostics;
namespace HeroEngine.ScreenManagement.Screens 
{
    class ExitScreen : ScreenComponent
    {
        ContentManager content;
        string DrawText = "";
        string EventualText = "";
        TimeSpan start_time;
        TimeSpan last_letter;
        SpriteFont font;
        float speed = 0.3f;
        public ExitScreen(Game _game,ScreenManager sman, string Message, float speed):base(_game,sman)
        {
            EventualText = Message;
            this.speed = speed;
        }

        public override void LoadContent()
        {
            base.Initialize();
            if (content == null)
                content = new ContentManager(screenManager.Game.Services, "Content");
           font = content.Load<SpriteFont>("gui/font/small_text");
           base.LoadContent();
        }

        public override void  UnloadContent()
        {
        
        }

        public override void Update(GameTime gt)
        {
            if (last_letter.TotalSeconds == 0)
            {
                start_time = gt.TotalGameTime;
            }
            if (DrawText == EventualText)
            {
                Game.Exit();
            }

            if ((float)start_time.Milliseconds / 1000f > speed)
            {
                
                last_letter = gt.TotalGameTime;
                if (DrawText.Length == EventualText.Length)
                {
                    System.Threading.Thread.Sleep(2000);
                    Game.Exit();
                }
                else
                {
                    DrawText += EventualText[DrawText.Length];
                }
            }

            base.Update(gt);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch sb = screenManager.SpriteBatch;
            sb.Begin();
            sb.DrawString(font, DrawText, new Vector2(20, 20), Color.White);
            sb.End();
 	        base.Draw(gameTime);
        }

    }
}
