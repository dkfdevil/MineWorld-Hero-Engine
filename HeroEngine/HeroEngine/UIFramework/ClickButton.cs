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
using C3.XNA;
using HeroEngine.Operations;

namespace HeroEngine.UIFramework
{
    class ClickButton
    {
        public string ButtonText;
        Color BackGroundColour;
        Color NormalColour;
        Color OutlineColor;
        public ClickButtonState state = ClickButtonState.Unselected;
        Vector2 location;
        Rectangle button_postion;
        SpriteFont font;
        int Selected = -1;

        public ClickButton(string button_text, Color bg_colour, Color line_color, Vector2 button_position, SpriteFont Font)
        {
            ButtonText = button_text;
            NormalColour = bg_colour;
            OutlineColor = line_color;
            Vector2 location = button_position;
            font = Font;
            button_postion = new Rectangle((int)location.X, (int)location.Y, (int)font.MeasureString(button_text).X + 20, font.LineSpacing + 10);
        }

        public void Update(Screen.Cursor cursor, MouseInputManager mouse_man)
        {
            if (RectangleOperations.InsideRectangle(false, button_postion, cursor.Location) && state != ClickButtonState.Pressed)
            {
                state = ClickButtonState.Selected;
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Click)
                {
                    state = ClickButtonState.PressedIn;
                }
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Held)
                {
                    state = ClickButtonState.PressedIn;
                }
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Released //&& state == ClickButtonState.PressedIn
                   )
                {
                    state = ClickButtonState.Pressed;
                }
            }
            else
            {
                state = ClickButtonState.Unselected;
            }
            
            
            switch (state)
            {
                case ClickButtonState.Selected :
                    BackGroundColour = Color.Gray;
                    break;
                case ClickButtonState.Unselected:
                    BackGroundColour = NormalColour;
                    break;
                case ClickButtonState.PressedIn:
                    BackGroundColour = Color.DarkBlue;
                    break;
                case ClickButtonState.Pressed:
                    BackGroundColour = NormalColour;
                    break;

                    
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.FillRectangle(button_postion, BackGroundColour);
            sb.DrawRectangle(button_postion, OutlineColor, 3);
            sb.DrawString(font, ButtonText, new Vector2(button_postion.X + 10, button_postion.Y + 5), Color.Black);
        }

        public bool ClickListener()
        {
            if (state == ClickButtonState.Pressed)
            {
                state = ClickButtonState.Selected;
                return true;
            }
            return false;
        }
    }
}
