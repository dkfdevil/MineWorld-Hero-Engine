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
namespace HeroEngine.ScreenManagement.Screens
{
    class OptionsScreen : ScreenComponent
    {
        string Title = "No Name";
        string Description = "No Discription";
        //Content
        SpriteFont[] fonts = new SpriteFont[3];
        Texture2D background;
        MouseInputManager mman = new MouseInputManager();
        bool isPaused = false;
        List<ClickButton> buttons = new List<ClickButton>();
        //Classes
        Cursor cursor;
        ContentManager content;
        public OptionsScreen(Game game, ScreenManager sman, string title, string Description, Cursor Cursor, bool isPaused)
            : base(game, sman)
        {
            cursor = Cursor;
            Title = title;
            this.Description = Description;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            content = new ContentManager(Game.Services, "Content");
            cursor.tex = Game.Content.Load<Texture2D>("gui/cursor");
            background = content.Load<Texture2D>("back/background00");
            fonts[0] = content.Load<SpriteFont>("gui/font/TitleText");
            fonts[1] = content.Load<SpriteFont>("gui/font/menu_item");
            fonts[2] = content.Load<SpriteFont>("gui/font/ammo_count");

            buttons.Add(new ClickButton("Controls", Color.DarkGray, Color.Transparent, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 8), fonts[1]));
            buttons.Add(new ClickButton("Audio Settings", Color.DarkGray, Color.Transparent, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 9), fonts[1]));
            if (isPaused)
                buttons.Add(new ClickButton("Video Settings", Color.DarkGray, Color.Transparent, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 10), fonts[1]));
            else
                buttons.Add(new ClickButton("Extras", Color.DarkGray, Color.Transparent, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 10), fonts[1]));

            buttons.Add(new ClickButton("Back", Color.DarkGray, Color.Transparent, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 11), fonts[1]));
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gt)
        {
            foreach (var item in buttons)
            {
                item.Update(cursor, mman);
                CheckButtons();
            }
            mman.Update();
            base.Update(gt);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = screenManager.SpriteBatch;
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix());
            if (!isPaused)
            {
                sb.Draw(background, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White);
            }
            sb.DrawString(fonts[0], Title, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 5), Color.DarkGray);
            sb.DrawString(fonts[1], Description, new Vector2(20, (Game.GraphicsDevice.Viewport.Height / 12) * 6), Color.DarkGray);
            foreach (var item in buttons)
            {
                item.Draw(sb);
            }
            cursor.DrawCursor(sb);

            foreach (var item in buttons)
            {
                item.Update(cursor, mman);
            }
            sb.End();
            base.Draw(gameTime);
        }

        public void CheckButtons()
        {
            foreach (var item in buttons)
            {
                if (item.ClickListener())
                {
                    ClicksOnButtonsUpdate(item.ButtonText);
                }
            }
        }

        public void ClicksOnButtonsUpdate(string text)
        {
            switch (text)
            {
                case "Start New World":
                    smanager.AddScreen(new LevelGeneratorScreen(Game, screenManager));
                    smanager.RemoveScreen(this);

                    break;
                case "Quit":
                    Screens.ExitScreen exitscreen = new ExitScreen(Game, smanager, "Goodbye ..." + Environment.UserName + " .... but remember the hero engine has you now >:D.", 0.5f);
                    smanager.AddScreen(exitscreen);
                    smanager.RemoveScreen(this);
                    break;
                case "Save World":
                    //Do Nothing
                    break;
                case "Options":
                    smanager.AddScreen(new OptionsScreen(Game, screenManager, "Game Options", "Change game configuration.", new Cursor(), false));
                    break;

                case "Load World":
                    break;
            }
        }
    }
}
