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
using C3;
using HeroEngine.Screen;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.LevelEditing;

namespace HeroEngine.ScreenManagement.Screens
{

    class LevelGeneratorScreen : ScreenComponent
    {
        ContentManager content;
        Texture2D backdrop;
        Texture2D[] loadingbar = new Texture2D[2];
        Rectangle[] rects = new Rectangle[3];
        Map load_map;
        public LevelGeneratorScreen(Game _game, ScreenManager sman)
            : base(_game, sman)
        {
            load_map = LevelGenerator.CreateLevel();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (content == null)
                content = new ContentManager(screenManager.Game.Services, "Content");

            backdrop = content.Load<Texture2D>(@"back\generator");
            loadingbar[0] = content.Load<Texture2D>(@"hud\loadingbar\back");
            loadingbar[1] = content.Load<Texture2D>(@"hud\loadingbar\bar");
            rects[0] = new Rectangle((Game.GraphicsDevice.Viewport.Width / 2) - 100, (Game.GraphicsDevice.Viewport.Height / 2) - 47, 200, 94);
            rects[1] = new Rectangle(rects[0].X + 22,rects[0].Y + 48,loadingbar[1].Width,loadingbar[1].Height);
            rects[2] = new Rectangle(rects[1].X,rects[1].Y,5,rects[1].Height);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            content.Unload();
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (load_map != null)
            {
                //screenManager.AddScreen(new GameScreen(Game, screenManager));
                screenManager.RemoveScreen(this);
            }
        }

        public override void Draw(GameTime gt)
        {
            screenManager.GraphicsDevice.Clear(Color.Brown);
            SpriteBatch sb = screenManager.SpriteBatch;
            sb.Begin();
                sb.Draw( loadingbar[0], rects[0], Color.White);
                sb.Draw( loadingbar[1], rects[1], Color.Red);
            sb.End();
        }

    }
}
