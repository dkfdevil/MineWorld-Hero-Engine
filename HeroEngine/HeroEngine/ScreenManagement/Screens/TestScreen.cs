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
namespace HeroEngine.ScreenManagement.Screens
{
    class TestScreen : ScreenComponent
    {
        ContentManager content;
        Texture2D TestLogo;
        SpriteFont font;
        SoundEffect sound;
        SoundEffectInstance soundplayer;
        Vector2[] midpoint;
        float[] rotation = new float[3];
        public TestScreen(Game _game, ScreenManager manager)
            : base(_game,manager)
        {
            State = ScreenState.Hidden;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (content == null)
                content = new ContentManager(screenManager.Game.Services, "Content");
            TestLogo = Game.Content.Load<Texture2D>("dev/testimage");
            font = Game.Content.Load<SpriteFont>("gui/font/small_text");
            sound = Game.Content.Load<SoundEffect>("dev/gaben");
            soundplayer = sound.CreateInstance();
            midpoint = new Vector2[3];
            midpoint[0] = new Vector2(font.MeasureString("Does it work?").X / 2, font.MeasureString("Does it work?").Y / 2);
            midpoint[1] = new Vector2(font.MeasureString("Do windmills cool you down?").X / 2, font.MeasureString("Do windmills cool you down?").Y / 2);
            midpoint[2] = new Vector2(font.MeasureString("TIME TO 'Epsiode 3 Release' : 0.00000000").X / 2, font.MeasureString("TIME TO 'Epsiode 3 Release' : 0.00000000").Y / 2);
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (soundplayer.State == SoundState.Stopped)
            {
                soundplayer.Play();
            }
            rotation[0] += 0.01f;
            rotation[1] -= 0.01f;
            rotation[2] += 0.01f;
        }

        public override void Draw(GameTime gt)
        {
            screenManager.GraphicsDevice.Clear(Color.Brown);
            SpriteBatch sb = screenManager.SpriteBatch;
            sb.Begin();
            sb.Draw(TestLogo, Game.GraphicsDevice.Viewport.Bounds, Color.White);
            
            sb.DrawString(font, "Does it work?", new Vector2(Game.GraphicsDevice.Viewport.Width / 6, Game.GraphicsDevice.Viewport.Height / 6), Color.Red,rotation[0],midpoint[0],1,SpriteEffects.None,0);
            sb.DrawString(font, "Do windmills cool you down?", new Vector2((Game.GraphicsDevice.Viewport.Width / 6) * 4, (Game.GraphicsDevice.Viewport.Height / 6) * 4), Color.Red, rotation[1], midpoint[1], 1, SpriteEffects.None, 0);
            sb.DrawString(font, "TIME TO 'Epsiode 3 Release' :" + gt.TotalGameTime.TotalSeconds.ToString(), new Vector2(200, 300), Color.Blue, rotation[2], midpoint[2], 1, SpriteEffects.None, 0);
            sb.End();
        }
    }
}
