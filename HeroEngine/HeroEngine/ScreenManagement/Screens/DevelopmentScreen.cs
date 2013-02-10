using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    class DevelopmentScreen : ScreenComponent
 {
        ContentManager content;
        Texture2D Background;
        SpriteFont font;
        SpriteFont item_text;
        SoundEffectInstance snd_suprise;
        SoundEffectInstance soundplayer;
        Cursor cursor = new Cursor();
        ClickButton[] Buttons = new ClickButton[5];
        DialogBox suprise;
        public MouseInputManager mouse_man;
        string[] options = new string[3];
        int Selected;
        int pitch = 0;
        public DevelopmentScreen(Game _game, ScreenManager manager) : base(_game,manager)
        {
            mouse_man = manager.mouse_man;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (content == null)
                content = new ContentManager(screenManager.Game.Services, "Content");
            Background = content.Load<Texture2D>("back/background00");
            font = content.Load<SpriteFont>("gui/font/TitleText");
            item_text = content.Load<SpriteFont>("gui/font/small_text");
            soundplayer = content.Load<SoundEffect>("sound/music/menumusic00").CreateInstance();
            snd_suprise = content.Load<SoundEffect>("sound/music/wierd00").CreateInstance();
            cursor.tex = Game.Content.Load<Texture2D>("gui/cursor");
            Buttons[0] = new ClickButton("Play Demo Screen", Color.DarkGray, Color.Black, new Vector2(300, 100), item_text);
            Buttons[1] = new ClickButton("Quit and return to Desktop", Color.DarkGray, Color.Black, new Vector2(300, 200), item_text);
            Buttons[2] = new ClickButton("Actually, im still working on 2", Color.DarkGray, Color.Black, new Vector2(300, 300), item_text);
            Buttons[3] = new ClickButton("Full Screen", Color.DarkGray, Color.Black, new Vector2(300, 400), item_text);
            Buttons[4] = new ClickButton("Clear Map File", Color.DarkGray, Color.Black, new Vector2(500, 400), item_text);
            Rectangle size = new Rectangle(Game.GraphicsDevice.Viewport.Width / 4,Game.GraphicsDevice.Viewport.Height / 4,200,200);
            suprise = new DialogBox("Oh God, WHY did you do that. YOU LOST THE GAME.", size, item_text, null, null, new TextArea(item_text, Color.Red, new Rectangle(size.X + 10, size.Y + (int)item_text.MeasureString("Oh God, WHY did you do that").Y, size.Width - 20, size.Height - (int)item_text.MeasureString("Oh God, WHY did you do that").Y - 10)));
            suprise.AddLineToDialogBox("Bet you regret that now THE GAME");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            content.Unload();
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if (suprise.Show)
            {
                soundplayer.Stop();
                if (snd_suprise.State == SoundState.Stopped)
                {
                    snd_suprise.Play();
                }
            }
            else
            {
                snd_suprise.Stop();
                if (soundplayer.State == SoundState.Stopped)
                {
                    soundplayer.Play();
                }
                snd_suprise.Pitch = 0f;
            }


            foreach (ClickButton item in Buttons)
            {
                item.Update(cursor,mouse_man);

                if(item.ClickListener())
                    ButtonClickEvent(item);
            }
            mouse_man.Update();
            suprise.UpdateDialog();
        }

        public override void Draw(GameTime gt)
        {
            screenManager.GraphicsDevice.Clear(Color.Brown);
            SpriteBatch sb = screenManager.SpriteBatch;
            Resolution.BeginDraw();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix());
            sb.Draw(Background, Game.GraphicsDevice.Viewport.Bounds, Color.White);
            foreach (ClickButton item in Buttons)
            {
                item.Draw(sb);
                if (item.ClickListener())
                    ButtonClickEvent(item);
            }
            suprise.DrawDialog(sb);
            cursor.DrawCursor(sb);
            sb.End();
        }

        public void ButtonClickEvent(ClickButton button)
        {
            switch (button.ButtonText)
            {
                case "Play Demo Screen":
                    smanager.AddScreen(new LevelGeneratorScreen(Game,screenManager));
                    smanager.RemoveScreen(this);
                    
                    break;
                case "Quit and return to Desktop":
                    Screens.ExitScreen exitscreen = new ExitScreen(Game, smanager, "Goodbye ..." + Environment.UserName + " .... but remember the hero engine has you now >:D.",0.5f);
                    smanager.AddScreen(exitscreen);
                    smanager.RemoveScreen(this);
                    break;
                case "Actually, im still working on 2":
                    //Do Nothing
                    suprise.Show = Toggle.ToggleBool(suprise.Show);
                    break;
                case "Full Screen":

                    break;

                case "Clear Map File":
                    File.Delete(Environment.CurrentDirectory + @"\\WorldFiles\" + "Random" + ".wld");
                    break;
            }

        }
    }
}
