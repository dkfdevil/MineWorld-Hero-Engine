using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using HeroEngine.Screen;
using HeroEngine.CoreGame;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.ScreenManagement.Screens;
using HeroEngine.Render;
namespace HeroEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        //Standard Variable
        ScreenManagement.ScreenManager sman;

        //Parameters
        string[] parameters;
        public MainGame(string[] args)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            parameters = args;
        }

        protected override void Initialize()
        {
            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(this.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2, this.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2);
            Resolution.SetResolution(this.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2, this.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2, false);
            
            sman = new ScreenManagement.ScreenManager(this);
            Components.Add(sman);
            VideoIntroScreen vis;
            vis = new VideoIntroScreen(this, sman);
            sman.AddScreen(vis);
            graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            sman.gametime = gameTime;
            base.Draw(gameTime);
        }

    }
}
