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
using HeroEngine.ScreenManagement;
using HeroEngine.UIFramework.Menus;
using HeroEngine.Operations;
namespace HeroEngine.UIFramework.Menus.ItemTypes
{
    class Dropdown : MenuItem
    {
        string[] Items;
        int Selected = 0;
        string Label;
        SpriteFont Font;
        Rectangle BoxHeight;
        Rectangle DropDown;
        Rectangle[] DroppedItems;
        Color ForegroundColor;
        Color BackgroundColor;
        bool Dropped = false;
        const float spacing = 4;
        B_State state = B_State.Unselected;
        public Dropdown(MenuManager manager, Color colorA, Color colorB, Rectangle position, string label,string[] items, SpriteFont font) 
            : base(manager, colorA, colorB, position)
        {

            Label = label;
            ForegroundColor = colorA;
            BackgroundColor = colorB;
            Font = font;
            Items = items;
            DroppedItems = new Rectangle[items.Length];


        }

        public override void Update(Screen.Cursor cursor, MouseInputManager mouse_man)
        {
            BoxHeight = new Rectangle((int)Font.MeasureString(Label).X + 10 + Position.X, Position.Y, (int)ArrayFunc.GetLargestSizeX(Font, Items), (int)ArrayFunc.GetLargestSizeY(Font, Items));
            DropDown = new Rectangle((int)Font.MeasureString(Label).X + 10 + Position.X, Position.Y + BoxHeight.Height, (int)ArrayFunc.GetLargestSizeX(Font, Items),(int)ArrayFunc.GetTotalSizeY(0,Items.Length,Items,Font,spacing));
            if (Dropped)
            { 
                int i = 0;
                int y = 0;
                foreach (var item in Items)
                {
                    DroppedItems[i] = new Rectangle(DropDown.X, DropDown.Y + y, BoxHeight.Width, (int)Font.MeasureString(Items[i]).Y);
                    y += (int)spacing + (int)Font.MeasureString(Items[i]).Y;
                    i++;
                }
            }
            if (RectangleOperations.InsideRectangle(false, BoxHeight, cursor.Location) && state != B_State.Pressed)
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
                    Dropped = !Dropped;
                }
            }
            else
            {
                state = B_State.Unselected;
            }
            int slctd = 0;
            foreach (var item in DroppedItems)
            {
                if (RectangleOperations.InsideRectangle(false, item, cursor.Location) && state != B_State.Pressed)
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
                    Selected = slctd;
                    Dropped = false;
                }
                }
                else
                {
                    state = B_State.Unselected;
                }
                slctd++;
            }

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, Label, new Vector2(Position.X, Position.Y + BoxHeight.Height / 4), Color.Black);
            sb.FillRectangle(BoxHeight, BackgroundColor);
            sb.DrawRectangle(BoxHeight, ForegroundColor, 2);
            sb.DrawString(Font, Items[Selected], new Vector2(BoxHeight.X, Position.Y + BoxHeight.Height / 4), Color.Black);

            //Dropdown
            if(Dropped)
            {
                sb.FillRectangle(DropDown, BackgroundColor);
                sb.DrawRectangle(DropDown, ForegroundColor, 2);
                int i = 0;
                foreach (var item in Items)
                {
                    sb.DrawRectangle(DroppedItems[i], ForegroundColor, 2f);
                    sb.DrawString(Font, Items[i], new Vector2(DroppedItems[i].X, DroppedItems[i].Height / 4 + DroppedItems[i].Y), Color.Black);
                    i++;
                }
            }

        }

        public override string ReturnState()
        {
                return Items[Selected];
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
