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
using HeroEngine.Operations;
using HeroEngine.ScreenManagement;
using HeroEngine.UIFramework.Menus;
using C3;

namespace HeroEngine.UIFramework.Menus.ItemTypes
{
    class Checkbox : MenuItem
    {
        public string ButtonText;
        Color NormalColour;
        Color OutlineColor;
        public B_State state = B_State.Unselected;
        Vector2 location;
        Texture2D[] checkbox;
        SpriteFont font;
        int Selected = -1;
        bool isChecked = false;
        public Checkbox(MenuManager manager, Color colorA, Color colorB, Rectangle Position, string button_text, SpriteFont Font, Texture2D[] textures)
            : base(manager, colorA, colorB, Position)
        {
            ButtonText = button_text;
            NormalColour = colorA;
            OutlineColor = colorB;
            font = Font;
            checkbox = textures;
        }

        public override void Update(Screen.Cursor cursor, MouseInputManager mouse_man)
        {
            Position = new Rectangle((int)font.MeasureString(ButtonText).X + 20, Position.Y, checkbox[0].Width, checkbox[0].Height);
            if (RectangleOperations.InsideRectangle(false, new Rectangle((int)font.MeasureString(ButtonText).X + 20, Position.Y, checkbox[0].Width, checkbox[0].Height), cursor.Location) && state != B_State.Pressed)
            {
                state = B_State.Selected;
                if (mouse_man.MouseButtonStates(MouseButton.Left) == MouseStates.Click)
                {
                    isChecked = !isChecked;
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

 
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isChecked)
            {
                sb.Draw(checkbox[1], Position, Color.White);
            }
            else
            {
                sb.Draw(checkbox[0], Position, Color.White);
            }

            sb.DrawString(font, ButtonText, new Vector2(Position.X - Position.Width * 2, Position.Y + (checkbox[0].Height / 4)), Color.Black);
        }

        public override string ReturnState()
        {
            if (isChecked)
            {
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
