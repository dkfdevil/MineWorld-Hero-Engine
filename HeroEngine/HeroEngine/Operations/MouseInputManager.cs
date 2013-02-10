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
namespace HeroEngine.Operations
{
    enum MouseButton
    {
        Left,
        Right,
        Middle
    }

    enum MouseStates
    {
        Blank,
        Click,
        Held,
        Released,
    }

    class MouseInputManager
    {
        MouseState old_state;
        MouseState new_state = Mouse.GetState();

        public void Update()
        {
            old_state = new_state;
            new_state = Mouse.GetState();
        }

        public MouseStates MouseButtonStates(MouseButton button)
        {
            MouseStates states = MouseStates.Blank;
            switch (button)
            {
                case MouseButton.Left:
                                if (old_state.LeftButton == ButtonState.Released && new_state.LeftButton == ButtonState.Released)
                                {
                                    states = MouseStates.Blank;
                                }       

                                if (old_state.LeftButton == ButtonState.Released && new_state.LeftButton == ButtonState.Pressed)
                                {
                                    states = MouseStates.Click;
                                }

                                if (old_state.LeftButton == ButtonState.Pressed && new_state.LeftButton == ButtonState.Pressed)
                                {
                                    states = MouseStates.Held;
                                }

                                if (old_state.LeftButton == ButtonState.Pressed && new_state.LeftButton == ButtonState.Released)
                                {
                                    states = MouseStates.Released;
                                }
                                break;

                case MouseButton.Middle:
                                if (old_state.MiddleButton == ButtonState.Released && new_state.MiddleButton == ButtonState.Released)
                                {
                                    states = MouseStates.Blank;
                                }

                                if (old_state.MiddleButton == ButtonState.Released && new_state.MiddleButton == ButtonState.Pressed)
                                {
                                    states = MouseStates.Click;
                                }

                                if (old_state.MiddleButton == ButtonState.Pressed && new_state.MiddleButton == ButtonState.Pressed)
                                {
                                    states = MouseStates.Held;
                                }

                                if (old_state.MiddleButton == ButtonState.Pressed && new_state.MiddleButton == ButtonState.Released)
                                {
                                    states = MouseStates.Released;
                                }
                                break;

                case MouseButton.Right:
                                if (old_state.RightButton == ButtonState.Released && new_state.RightButton == ButtonState.Released)
                                {
                                    states = MouseStates.Blank;
                                }

                                if (old_state.RightButton == ButtonState.Released && new_state.RightButton == ButtonState.Pressed)
                                {
                                    states = MouseStates.Click;
                                }

                                if (old_state.RightButton == ButtonState.Pressed && new_state.RightButton == ButtonState.Pressed)
                                {
                                    states = MouseStates.Held;
                                }

                                if (old_state.RightButton == ButtonState.Pressed && new_state.RightButton == ButtonState.Released)
                                {
                                    states = MouseStates.Released;
                                }
                                break;
            }
            return states;
        }
    }
}
