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
namespace HeroEngine.ScreenManagement
{
    class ScreenManager : DrawableGameComponent
    {
        List<ScreenComponent> screens = new List<ScreenComponent>();
        List<ScreenComponent> screensToUpdate = new List<ScreenComponent>();

        SpriteBatch spriteBatch;
        SpriteFont default_font;
        public HeroEngine.Operations.MouseInputManager mouse_man = new Operations.MouseInputManager();
        Texture2D blank_texture;
        public GameTime gametime;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public ScreenManager(Game game) : base(game)
        {

            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            default_font = content.Load<SpriteFont>("gui/font/menu_item");
            blank_texture = content.Load<Texture2D>("gui/close");

            // Tell each of the screens to load their content.
            foreach (ScreenComponent screen in screens)
            {
                screen.LoadContent();
            }

        }

        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (ScreenComponent screen in screens)
            {
                screen.UnloadContent();
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            screensToUpdate.Clear();

            foreach (ScreenComponent screen in screens)
                if (screen.State != ScreenState.Covered)
                {
                    screensToUpdate.Add(screen);
                }
            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                ScreenComponent screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime);

                if (screen.State == ScreenState.Transition_on ||
                    screen.State == ScreenState.Visible)

                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            foreach (ScreenComponent screen in screens)
            {
                if (screen.State == ScreenState.Hidden)
                    continue;
                
                screen.Draw(gameTime);
            }
            
        }

        public void AddScreen(ScreenComponent screen, ScreenComponent caller = null)
        {
            screen.smanager = this;
            screen.IsExiting = false;
            // If we have a graphics device, tell the screen to load content.
            screen.LoadContent();
            screens.Add(screen);

        }

        public void SetState(int screenindex, ScreenState state)
        {
            screens[screenindex].State = state;
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(ScreenComponent screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            screen.UnloadContent();
            screens.Remove(screen);
            screensToUpdate.Remove(screen);

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public ScreenComponent[] GetScreens()
        {
            return screens.ToArray();
        }

    }
}
