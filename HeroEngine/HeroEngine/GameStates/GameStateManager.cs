using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HeroEngine.Input;
using HeroEngine.GameState.GameStates;
using HeroEngine.GameStates;
using Microsoft.Xna.Framework.Media;
using HeroEngine.CoreGame;
using HeroEngine.Render;
using HeroEngine.GameStates.GameStates;
using HeroEngineShared;
using System.IO;
using EasyConfig;

namespace HeroEngine.GameStates
{
    public class GameStateManager
    {
        //This class is the masterclass
        //This class holds all the tools for the different screen to function even the game state
        public GraphicsDeviceManager Graphics;
        public GraphicsDevice Device;
        public SpriteBatch SpriteBatch;
        public ResolutionMonitorManager ResolutionManager;

        //Need to add more media here like the music player and the soundeffectplayer
        //TODO
        public VideoPlayer VideoPlayer;

        //Resources part
        public MainGame game;
        public ContentManager Conmanager;
        public GameResources GameResources;
        public ConfigFile Configfile;

        //Input part
        private readonly InputHelper _inputhelper;
        public ControlBinding Controlbinding;

        private readonly BaseState[] _screens;

        private BaseState _curScreen;

        public GameStateManager(GraphicsDeviceManager man,ContentManager cman,MainGame gam)
        {
            //Check for the files otherwise the game wont run
            CheckForRequiredFiles();

            Graphics = man;
            Device = Graphics.GraphicsDevice;
            SpriteBatch = new SpriteBatch(Device);
            ResolutionManager = new ResolutionMonitorManager();
            ResolutionManager.Init(ref Graphics);

            VideoPlayer = new VideoPlayer();

            game = gam;
            Conmanager = cman;
            GameResources = new GameResources(Conmanager);
            Configfile = new ConfigFile(Directory.GetCurrentDirectory() + Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Settings + Constants.HeroEngine_Config_Extension);

            _inputhelper = new InputHelper();
            Controlbinding = new ControlBinding();

            _screens = new BaseState[]
                           {
                               new IntroState(this,State.GameState.IntroState)
                           };

            //Load the settings file
            LoadSettings();

            //Set initial state in the manager itself
            SwitchState(State.GameState.IntroState);
        }

        public void LoadContent()
        {
            foreach (BaseState screen in _screens)
                screen.LoadContent(GameResources);
        }

        public void Update(GameTime gameTime)
        {
            _inputhelper.Update();
            _curScreen.Update(gameTime,_inputhelper);
        }

        public void SwitchState(State.GameState newState)
        {
            foreach (BaseState screen in _screens)
            {
                if (screen.AssociatedState == newState)
                {
                    //This is true for the first time
                    if (_curScreen != null)
                    {
                        //Call unload for our currentscreen
                        _curScreen.Unload(GameResources);
                        _curScreen.Contentloaded = false;
                    }

                    //Switch our currentscreen to our new screen
                    _curScreen = screen;

                    //If our new screen content isnt loaded yet call it
                    if (_curScreen.Contentloaded == false)
                    {
                        _curScreen.LoadContent(GameResources);
                        _curScreen.Contentloaded = true;
                    }
                    break;
                }
            }
        }

        public void ExitGame()
        {
            game.Exit();
        }

        public void Draw(GameTime gameTime)
        {
            _curScreen.Draw(gameTime,Device,SpriteBatch);
        }

        public void LoadSettings()
        {
            game.Window.Title = Constants.HeroEngine_Version;
            //game.Window.AllowUserResizing = true;
            //Game.Window.ClientSizeChanged += WindowClientSizeChanged;

            Graphics.PreferredBackBufferHeight = Configfile.SettingGroups["Video"].Settings["Height"].GetValueAsInt();
            Graphics.PreferredBackBufferWidth = Configfile.SettingGroups["Video"].Settings["Width"].GetValueAsInt();
            Graphics.IsFullScreen = Configfile.SettingGroups["Video"].Settings["Fullscreen"].GetValueAsBool();

            Graphics.ApplyChanges();
        }

        public void SaveSettings()
        {
            Configfile.SettingGroups["Video"].Settings["Height"].SetValue(Graphics.PreferredBackBufferHeight);
            Configfile.SettingGroups["Video"].Settings["Width"].SetValue(Graphics.PreferredBackBufferWidth);
            Configfile.SettingGroups["Video"].Settings["Fullscreen"].SetValue(Graphics.IsFullScreen);

            Configfile.Save(Directory.GetCurrentDirectory() + Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Settings + Constants.HeroEngine_Config_Extension);
        }

        //void WindowClientSizeChanged(object sender, EventArgs e)
        //{
        //    Graphics.PreferredBackBufferWidth = game.Window.ClientBounds.Width;
        //    Graphics.PreferredBackBufferHeight = game.Window.ClientBounds.Height;
        //}

        static void CheckForRequiredFiles()
        {
            CheckDirectorys();
            CheckFiles();
        }

        static void CheckDirectorys()
        {
            //Check for the directorys and if they dont exist then create them
            string[] dirs = new string[3];
            dirs[0] = Constants.HeroEngine_Folder_Config;
            dirs[1] = Constants.HeroEngine_Folder_Data;
            dirs[2] = Constants.HeroEngine_Folder_World;

            foreach (string dir in dirs)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + dir);
                }
            }

        }

        static void CheckFiles()
        {
            //If the files dont exsist let it create default ones
            string[] files = new string[7];
            files[0] = Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Binding + Constants.HeroEngine_Config_Extension;
            files[1] = Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Settings + Constants.HeroEngine_Config_Extension;
            files[2] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Fonts + Constants.HeroEngine_Data_Extension;
            files[3] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Music + Constants.HeroEngine_Data_Extension;
            files[4] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Sounds + Constants.HeroEngine_Data_Extension;
            files[5] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Texture + Constants.HeroEngine_Data_Extension;
            files[6] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Tiles + Constants.HeroEngine_Data_Extension;

            foreach (string file in files)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + file))
                {
                    FileInfo ifo = new FileInfo(file);
                    string filename = ifo.Name.Replace(ifo.Extension, "");
                    string data = (string)DefaultResources.ResourceManager.GetObject(filename);
                    File.WriteAllText(Directory.GetCurrentDirectory() + file, data);
                }
            }
        }
    }
}
