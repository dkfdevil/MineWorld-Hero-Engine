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
using HeroEngine.Operations;
using System.IO;
using HeroEngine.Objects;
using HeroEngine.UIFramework;
using HeroEngine.Screen;
namespace HeroEngine.ScreenManagement.Screens
{
    class VideoIntroScreen : ScreenComponent
    {
        List<Texture2D> logos = new List<Texture2D>(); //Grab 3 logos.
        int logonumber = 1;
        float logoalpha = 0;
        int logostage = 0;
        Rectangle screen;
        DateTime fadestarttime = new DateTime();
        public VideoIntroScreen(Game game, ScreenManager sman) : base(game, sman) { }
        const float fadedelay = 2000;
        const float logoonscreen = 2000;
        DateTime starttime;
        DateTime updatetime;
        public override void Initialize()
        {
            base.Initialize();

        }

        public override void LoadContent()
        {
            ContentManager cman = new ContentManager(Game.Services,"Content");
            logos.Add(cman.Load<Texture2D>(@"loadup\logos\logo1")); //logos 1
            logos.Add(cman.Load<Texture2D>(@"loadup\logos\logo2")); //logos 2
            logos.Add(cman.Load<Texture2D>(@"gui\loadscreen")); //logos 3
            screen = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
            starttime = DateTime.Now;
            updatetime = DateTime.Now;
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        public override void Update(GameTime gt)
        {
            switch (logostage)
                {
                case 0: //Fade In
                        TimeSpan diff = DateTime.Now - updatetime; //Get the time to update
                        
                        if(diff.Milliseconds > 9) //Update Me
                        {
                            logoalpha = 255 / (fadedelay / (float)(DateTime.Now - starttime).Seconds);
                            updatetime = DateTime.Now;
                        }

                        

                        if ( (DateTime.Now - starttime).Seconds > 2)
                        {
                            logostage++;
                            fadestarttime = DateTime.Now;

                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                            logostage = 2;
                        break;
                case 1: //Display
                        TimeSpan diff1 = DateTime.Now - fadestarttime;
                        if (diff1.TotalSeconds > logoonscreen)
                        {
                            logostage++;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                            logostage = 2;
                        break;
                case 2: //Fade Out
                        logoalpha -= 255 / 50;
                        if (logoalpha <= 0)
                        {
                            logostage = 0;
                            logonumber++;
                            starttime = DateTime.Now;
                        }
                        break;
                }

            if (logonumber > 3)
            {
                MenuScreen vis;
                vis = new MenuScreen(Game, screenManager, "Main Menu", "Links for the main game", new Cursor(), false,null);
                screenManager.AddScreen(vis);
                smanager.RemoveScreen(this);
            }
            base.Update(gt);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = smanager.SpriteBatch;
            screenManager.GraphicsDevice.Clear(Color.Red);
            sb.Begin(SpriteSortMode.BackToFront,BlendState.Opaque);
            sb.Draw(logos[logonumber - 1],screen,new Color(50,50,50,logoalpha));
            sb.End();
            base.Draw(gameTime);
        }
    }
}
