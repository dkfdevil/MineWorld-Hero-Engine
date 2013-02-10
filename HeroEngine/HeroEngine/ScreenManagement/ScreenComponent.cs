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
using HeroEngine.ScreenManagement;
namespace HeroEngine
{
    class ScreenComponent : DrawableGameComponent
    {

        public ScreenComponent(Game _game, ScreenManager manager, ScreenComponent _parent = null)
            : base(_game)
        {
            smanager = manager;
            gametime = manager.gametime;
            this._parent = _parent;
        }

        public ScreenState State = ScreenState.Hidden;
        SpriteBatch spriteBatch;
        //Transition
        public TimeSpan TransitionTime_On = TimeSpan.Zero;
        public TimeSpan TransitionTime_Off = TimeSpan.Zero;
        public ScreenComponent _parent;
        public GameTime gametime;
        float TransitionPos;
        float TransitionAlpha;

        public bool IsExiting = false;

        public ScreenManager screenManager
        {
            get { return smanager; }
            internal set { smanager = value; }
        }

        public ScreenManager smanager;


        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gt)
        {
            if (IsExiting)
            {
                State = ScreenState.Transition_off;
                if(!UpdateTransition(gt,TransitionTime_Off,1))
                {
                    // Still busy transitioning.
                    State = ScreenState.Transition_off;
                }
                else
                {
                    // Transition finished!
                    State = ScreenState.Hidden;
                }
            }
            else
            {
                if (State == ScreenState.Covered)
                {
                    //Do Fuck All
                }
                else
                {
                    if (UpdateTransition(gt, TransitionTime_On, -1))
                    {
                        // Still busy transitioning.
                        State = ScreenState.Transition_on;
                    }
                    else
                    {
                        // Transition finished!
                        State = ScreenState.Visible;
                    }
                }
            }
        }

        bool UpdateTransition(GameTime gt, TimeSpan time, int direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gt.ElapsedGameTime.TotalMilliseconds /
                                          time.TotalMilliseconds);

            // Update the transition position.
            TransitionPos += transitionDelta * direction;

            // Did we reach the end of the transition?
            if (((direction < 0) && (TransitionPos <= 0)) ||
                ((direction > 0) && (TransitionPos >= 1)))
            {
                TransitionPos = MathHelper.Clamp(TransitionPos, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }

        public virtual void Draw(GameTime gameTime) { }

        public virtual void Input(GameTime gt) { }

        public void ExitScreen()
        {
            if (TransitionTime_Off == TimeSpan.Zero)
            {
                // If the screen has a zero transition time, remove it immediately.
                smanager.RemoveScreen(this);
            }
            else
            {
                // Otherwise flag that it should transition off and then exit.
                IsExiting = true;
            }
        }

    }
}
