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
using HeroEngine.Screen;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.LevelEditing;
using HeroEngine.Render;
using HeroEngine.UIFramework.Menus.ItemTypes;
using HeroEngine.UIFramework.Menus;
using HeroEngine.Sound;
namespace HeroEngine.ScreenManagement.Screens
{
    class MenuScreen : ScreenComponent
    {
        string Title = "No Name";
        string Description = "No Discription";
        //Content
        SpriteFont[] fonts = new SpriteFont[3];
        List<Song> MenuThemes = new List<Song>();
        List<Texture2D> backgrounds = new List<Texture2D>();
        BackgroundSlideShow slides;
        bool isPaused = false;
        //Classes
        public Cursor cursor;
        ContentManager content;
        //MenuManager
        MenuManager menu;
        MenuManager options;
        KeyCheck pausekey = new KeyCheck(Keys.Escape);
        MusicPlayer player;
        MenuManager selected_menu;
        public MenuScreen(Game game, ScreenManager sman, string title, string Description, Cursor Cursor, bool isPaused,ScreenComponent parent)
            : base(game, sman,parent)
        {
            cursor = Cursor;
            Title = title;
            this.Description = Description;
            menu = new MenuManager(this, new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 7),1);
            options = new MenuManager(this, new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 7),2);
            this.isPaused = isPaused;
        }

        public override void Initialize()
        {
            Resolution.SetVirtualResolution(Resolution._Width, Resolution._Height);
            
            base.Initialize();
        }

        public override void LoadContent()
        {
            
            content = new ContentManager(Game.Services, "Content");
            cursor.tex = Game.Content.Load<Texture2D>("gui/cursor");

            backgrounds.Add(content.Load<Texture2D>("back/background00"));
            backgrounds.Add(content.Load<Texture2D>("back/background1"));
            backgrounds.Add(content.Load<Texture2D>("back/background2"));
            backgrounds.Add(content.Load<Texture2D>("back/generator"));

            fonts[0] = content.Load<SpriteFont>("gui/font/TitleText");
            fonts[1] = content.Load<SpriteFont>("gui/font/menu_item");
            fonts[2] = content.Load<SpriteFont>("gui/font/ammo_count");

            MenuThemes.Add(content.Load<Song>("sound/music/menumusic00"));
            MenuThemes.Add(content.Load<Song>("sound/music/wierd00"));

            menu = new MenuManager(this, new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 4), 1);
            options = new MenuManager(this, new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 4), 2); 

            menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Start New World" ,fonts[1]));
            menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Load World", fonts[1]));
            menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Save World", fonts[1]));

            if (isPaused)
            {
                menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Back to Game", fonts[1]));
                menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Disconnect", fonts[1]));
            }
            else
            {
                menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Options", fonts[1]));
                menu.AddItem(new Button(menu, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Quit", fonts[1]));
            }

                

            Texture2D[] chk = new Texture2D[2];

            chk[0] = content.Load<Texture2D>(@"menu/checkbox");
            chk[1] = content.Load<Texture2D>(@"menu/checkboxchecked");

            options.AddItem(new Checkbox(options, Color.Red, Color.Red, Rectangle.Empty, "Fullscreen", fonts[1], chk));
            options.AddItem(new Button(options, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Apply", fonts[1]));
            options.AddItem(new Button(options, Color.DarkGray, Color.Transparent, Rectangle.Empty, "Back", fonts[1]));
            options.AddItem(new Dropdown(options, Color.DarkGray, Color.Gray , Rectangle.Empty, "Resolution", MiscOperations.GetDisplayModesToString(Game.GraphicsDevice.Adapter.SupportedDisplayModes,1.77777773f), fonts[1]));
            selected_menu = menu;
            selected_menu.Show();
            player = new MusicPlayer(MenuThemes.ToArray(), null);
            player.GetRandomSong();
            slides = new BackgroundSlideShow(backgrounds, gametime, screenManager.SpriteBatch,20f,15);
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gt)
        {
            if (!isPaused)
            {
                player.UpdateSong();
                slides.Update();
            }
            selected_menu.Update();
            CheckButtons();
            if (pausekey.KeyDownAndUp() && isPaused)
            {
                smanager.RemoveScreen(this);
                smanager.SetState(0, ScreenState.Visible);
                if (MediaPlayer.State == MediaState.Paused)
                    MediaPlayer.Resume();
            }
            
            base.Update(gt);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = screenManager.SpriteBatch;
            sb.Begin(SpriteSortMode.BackToFront,BlendState.Additive);
            
            sb.End();
            sb.Begin();
            if (!isPaused)
            {
                slides.Draw();
            }
            sb.DrawString(fonts[0], Title, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 2), Color.DarkGray);
            sb.DrawString(fonts[1], Description, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 3), Color.DarkGray);
            selected_menu.Draw(sb);
            cursor.DrawCursor(sb);
            sb.End();
           base.Draw(gameTime);
        }

        public void CheckButtons()
        {
            foreach (MenuItem item in selected_menu.GetItems())
            {
                if (item is Button)
                {
                    if (item.ReturnState() == "True")
                    {
                        ClicksOnButtonsUpdate(item.ItemID, selected_menu.menuid);
                    }
                }
            }
        }

        public void ClicksOnButtonsUpdate(int id,int mnu)
        {
            switch (mnu)
            {
                case 1:
                        switch (id)
                        {
                             case 1:
                                //Create new level
                                GameContentLoadScreen gs = new GameContentLoadScreen(Game, smanager);
                                smanager.AddScreen(gs,this);
                                MediaPlayer.Stop();
                                smanager.RemoveScreen(this);
                                break;

                             case 2:
                                //Load 

                                break;

                             case 3:
                                //Save
                                break;

                             case 4:
                                //Options or Back
                                selected_menu = options;
                                selected_menu.Show();

                                if (isPaused)
                                {
                                    if(MediaPlayer.State == MediaState.Paused)
                                        MediaPlayer.Resume();

                                    smanager.SetState(0, ScreenState.Visible);
                                    smanager.RemoveScreen(this);
                                }
                                break;

                            case 5:
                                //Quit
                                    if (isPaused)
                                    {
                                        isPaused = false;
                                        MenuScreen vis;
                                        vis = new MenuScreen(Game, screenManager, "Main Menu", "Links for the main game", new Cursor(), false, null);
                                        smanager.AddScreen(vis);
                                        smanager.RemoveScreen(_parent);
                                        smanager.RemoveScreen(this);
                                        break;
                                    }
                                    smanager.RemoveScreen(this);
                                    MediaPlayer.Stop();
                                    Screens.ExitScreen exitscreen = new ExitScreen(Game, smanager, "Goodbye ..." + Environment.UserName + " .... but remember the hero engine has you now >:D.", 0.5f);
                                    smanager.AddScreen(exitscreen,this);
                                    smanager.RemoveScreen(this);

                                break;
                        }
                        break;

                case 2:
                        switch (id)
                        {
                            case 1:
                                //Fullscreen .. No output
                                break;

                            case 2:
                                //Apply Settings
                                bool fs;

                                if(selected_menu.GetItemValue(0) == "True")
                                { 
                                    fs = true ;
                                } 
                                else 
                                {
                                    fs = false;
                                }
                                int splitindex = selected_menu.GetItemValue(3).IndexOf(" ");
                                int width = int.Parse(selected_menu.GetItemValue(3).Substring(0,splitindex + 1));
                                int height = int.Parse(selected_menu.GetItemValue(3).Substring(splitindex, selected_menu.GetItemValue(3).Length - splitindex));
                                Resolution.SetResolution(width ,height, fs);
                                Resolution.SetVirtualResolution(width, height);
                                EngineLimit.SetTileScale();
                                Resolution.ResetViewport();
                                options.ResetRes(new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 7));
                                menu.ResetRes(new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 7));
                                selected_menu.ResetRes(new Point(20, (Game.GraphicsDevice.Viewport.Height / 12) * 7));
                                break;

                            case 3:
                                //Back
                                selected_menu = menu;
                                selected_menu.Show();
                                break;

                            case 4:
                                //Options
                                break;

                            case 5:
                                //Quit
                                break;
                        }
                        break;
                }
        }
    }
}
