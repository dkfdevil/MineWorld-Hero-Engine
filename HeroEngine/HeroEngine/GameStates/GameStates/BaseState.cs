using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HeroEngine.GameStates;
using HeroEngine.Input;
using HeroEngine.CoreGame;

namespace HeroEngine.GameState.GameStates
{
    public abstract class BaseState
    {
        public GameStateManager Manager;
        public State.GameState AssociatedState;
        public bool Contentloaded;

        protected BaseState(GameStateManager manager, State.GameState associatedState)
        {
            Manager = manager;
            AssociatedState = associatedState;
        }

        public abstract void Unload(GameResources contentloader);
        public abstract void LoadContent(GameResources contentloader);
        public abstract void Update(GameTime gameTime, InputHelper input);
        public abstract void Draw(GameTime gameTime,GraphicsDevice gDevice,SpriteBatch sBtach);
    }
}