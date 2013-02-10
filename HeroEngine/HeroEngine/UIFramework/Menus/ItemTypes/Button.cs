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
using HeroEngine.ScreenManagement;
using HeroEngine.UIFramework.Menus;
namespace HeroEngine.UIFramework.Menus.ItemTypes
{
    class Button : MenuItem
    {
        public string ButtonText;
        Color BackGroundColour;
        Color NormalColour;
        Color OutlineColor;
        public B_State state = B_State.Unselected;
        Vector2 location;
        SpriteFont font;
        int Selected = -1;

        public Button(MenuManager manager, Color colorA, Color colorB, Rectangle Position, string button_text, SpriteFont Font)
            : base(manager, colorA, colorB, Position)
        {
            ButtonText = button_text;
            NormalColour = colorA;
            OutlineColor = colorB;
            font = Font;
            
        }

        public override void Update(Screen.Cursor cursor, MouseInputManager mouse_man)
        {
            Position = new Rectangle(Position.X, Position.Y, (int)font.MeasureString(ButtonText).X + 20, font.LineSpacing + 10);
            if (RectangleOperations.InsideRectangle(false, Position, cursor.Location) && state != B_State.Pressed)
            {
                state = B_State.Selected;
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Click)
                {
                    state = B_State.PressedIn;
                }
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Held)
                {
                    state = B_State.PressedIn;
                }
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Released //&& state == B_State.PressedIn
                   )
                {
                    state = B_State.Pressed;
                }
            }
            else
            {
                state = B_State.Unselected;
            }

            switch (state)
            {
                case B_State.Selected:
                    BackGroundColour = Color.Gray;
                    break;
                case B_State.Unselected:
                    BackGroundColour = NormalColour;
                    break;
                case B_State.PressedIn:
                    BackGroundColour = Color.DarkBlue;
                    break;
                case B_State.Pressed:
                    BackGroundColour = NormalColour;
                    break;

            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.FillRectangle(Position, BackGroundColour);
            sb.DrawRectangle(Position, OutlineColor, 3);
            sb.DrawString(font, ButtonText, new Vector2(Position.X + 10, Position.Y + 5), Color.Black);
        }

        public override string ReturnState()
        {
            if (state == B_State.Pressed)
            {
                state = B_State.Selected;
                return "True";
            }
            return "False";
        }

        public enum B_State
        {
            Unselected,
            Selected,
            PressedIn,
            Pressed
        }
    }
}
