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
using HeroEngine.CoreGame;
using HeroEngine.Render;
using HeroEngine.GameStates;
namespace HeroEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        //Standard Variable
        GameStateManager manager;

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
            manager = new GameStateManager(graphics, Content, this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            manager.LoadContent();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            manager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            manager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
