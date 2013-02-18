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
using HeroEngine.UserInterface.Items;
using HeroEngine.Screen;

namespace HeroEngine.GameStates.GameStates
{
    public class MainMenuState : BaseState
    {
        readonly GameStateManager _gamemanager;
        GameResources resourcemanager;

        //Menu stuff
        BackGround background;
        Button optionsbutton;
        Cursor mousecursor;

        public MainMenuState(GameStateManager manager, State.GameState associatedState)
            : base(manager, associatedState)
        {
            _gamemanager = manager;
        }

        public override void LoadContent(GameResources manager)
        {
            manager.AddResource(GameResourceTypes.Texture2D,"back/background00","mainmenubackground");
            manager.AddResource(GameResourceTypes.Texture2D, "menu/mainmenu/optionsbtn", "optionsbutton");
            manager.AddResource(GameResourceTypes.Texture2D, "gui/cursor", "mousecursor");
            background = new BackGround((Texture2D)manager.GetResource(GameResourceTypes.Texture2D, "mainmenubackground"),_gamemanager.ResolutionManager.Height,_gamemanager.ResolutionManager.Width);
            optionsbutton = new Button((Texture2D)manager.GetResource(GameResourceTypes.Texture2D, "optionsbutton"), _gamemanager.ResolutionManager.Height, _gamemanager.ResolutionManager.Width);
            optionsbutton.SetPosition(new Vector2(50,50));
            mousecursor = new Cursor((Texture2D)manager.GetResource(GameResourceTypes.Texture2D, "mousecursor"));
        }

        public override void Unload(GameResources manager)
        {
        }

        public override void Update(GameTime gameTime, InputHelper input)
        {
            mousecursor.Update(input);
            optionsbutton.update(input);
            if (optionsbutton.IsClicked)
            {
                _gamemanager.ExitGame();
            }
        }

        public override void Draw(GameTime gameTime, GraphicsDevice gDevice, SpriteBatch sBatch)
        {
            gDevice.Clear(Color.Black);
            background.Draw(sBatch);
            optionsbutton.Draw(sBatch);
            mousecursor.Draw(sBatch);
        }
    }
}
