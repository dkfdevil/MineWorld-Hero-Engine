using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.GameState.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using HeroEngine.CoreGame;
using System.IO;
using HeroEngineShared;
using HeroEngine.Input;
using Microsoft.Xna.Framework.Graphics;
using HeroEngine.Resources.Classes;

namespace HeroEngine.GameStates.GameStates
{
    public class IntroState : BaseState
    {
        readonly GameStateManager _gamemanager;
        GameResources resourcemanager;
        bool _introstarted;
        Rectangle _size;
        Texture2D screentodraw;

        public IntroState(GameStateManager manager, State.GameState associatedState)
            : base(manager, associatedState)
        {
            _gamemanager = manager;
        }

        public override void LoadContent(GameResources manager)
        {
            //HACKY
            resourcemanager = manager;
            manager.AddResource(GameResourceTypes.Video, "videos/intromovie","intromovie");
            _gamemanager.game.IsMouseVisible = false;
            _size.Width = _gamemanager.Graphics.PreferredBackBufferWidth;
            _size.Height = _gamemanager.Graphics.PreferredBackBufferHeight;
        }

        public override void Unload(GameResources manager)
        {
            manager.RemoveResource(GameResourceTypes.Video, "intromovie");
        }

        public override void Update(GameTime gameTime, InputHelper input)
        {
            if (!_introstarted)
            {
                _gamemanager.VideoPlayer.Play((Video)resourcemanager.GetResource(GameResourceTypes.Video, "intromovie"));
                _introstarted = true;
            }
            if (_gamemanager.VideoPlayer.State == MediaState.Stopped)
            {
                //This means our video is done playing
                _gamemanager.SwitchState(State.GameState.MainMenuState);
            }
            if (_gamemanager.VideoPlayer.State == MediaState.Playing)
            {
                screentodraw = _gamemanager.VideoPlayer.GetTexture();
            }
            if(input.AnyKeyPressed(true))
            {
                _gamemanager.VideoPlayer.Stop();
                _gamemanager.SwitchState(State.GameState.MainMenuState);
            }
        }

        public override void Draw(GameTime gameTime, GraphicsDevice gDevice, SpriteBatch sBatch)
        {
            gDevice.Clear(Color.Black);
            sBatch.Begin();
            sBatch.Draw(screentodraw, _size, Color.White);
            sBatch.End();
        }
    }
}
