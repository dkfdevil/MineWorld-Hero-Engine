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
namespace HeroEngine.UIFramework
{
    class DialogBox
    {
        private string message;
        private Rectangle rect;
        private Texture2D icn;
        private ClickButton defbutton;
        private Texture2D closeicon;
        private SpriteFont font;
        public bool Show = false;
        public bool DragDrop = false;
        public Point startdrag = new Point();
        public TextArea ta;
        public string Name = "NimNom";
        public int width;
        public void DrawDialog(SpriteBatch spritebatch)
        {
            //Start
            if (Show)
            {
                Primitives2D.FillRectangle(spritebatch, rect, Color.DarkGray);
                Primitives2D.DrawRectangle(spritebatch, rect, Color.SlateGray, 3f);
                if (rect.Width >= width)
                {
                    rect = new Rectangle(rect.X, rect.Y, width + 40, rect.Height);
                    ta.res = new Rectangle(ta.res.X, ta.res.Y, width + 40, ta.res.Height); 
                }
                
                if (icn != null)
                {
                    if (rect.X > 50 && rect.Y > 50)
                    {
                        spritebatch.Draw(icn, new Rectangle(25, 25, 25, 25), Color.White);
                        Primitives2D.DrawRectangle(spritebatch, rect, Color.SlateGray, 1f);
                    }
                }
                spritebatch.DrawString(font, message, new Vector2(rect.X + 50, rect.Y + 50), Color.Red);
                spritebatch.DrawString(font, Name, new Vector2(rect.X, rect.Y), Color.Red);
                //DrawButton

                //DrawTextarea
                ta.Draw(spritebatch);
                //End
            }
        }

        public DialogBox(string msg, Rectangle placement, SpriteFont fnt, Texture2D icon = null, ClickButton button = null, TextArea TextArea = null, Texture2D close = null)
        {
            message = msg;
            rect = placement;
            icn = icon;
            defbutton = button;
            closeicon = close;
            font = fnt;
            ta = TextArea;
            Name = msg;
            width = (int)font.MeasureString(message).Y;
            width *= 10;
        }

        public void UpdateDialog()
        {
            if (MouseInDialog())
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && DragDrop == false)
                {
                    DragDrop = true;
                    startdrag = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    DragAndDrop();
                    startdrag = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                    DragDrop = true;
                }

                if (Mouse.GetState().LeftButton == ButtonState.Released && DragDrop == true)
                {
                    DragAndDrop();
                    DragDrop = false;
                    startdrag = new Point();
                }
            }
        }

        public void DragAndDrop()
        {
            rect = new Rectangle(rect.X + (Mouse.GetState().X - startdrag.X), rect.Y + (Mouse.GetState().Y - startdrag.Y),rect.Width,rect.Height);
            if (ta != null)
            {
                ta.res = new Rectangle(ta.res.X + (Mouse.GetState().X - startdrag.X), ta.res.Y + (Mouse.GetState().Y - startdrag.Y), ta.res.Width, ta.res.Height);
            }
        }

        public bool MouseInDialog()
        {
            if (Mouse.GetState().X <= rect.X) //Outside Left of X
            {
                return false;
            }
            if (Mouse.GetState().X >= rect.X + rect.Width) // Outside Right of X
            {
                return false;
            }
            if (Mouse.GetState().Y <= rect.Y) //Before the top of y
            {
                return false;
            }
            if (Mouse.GetState().Y >= rect.Y + rect.Height) //After the bottom of y
            {
                return false;
            }


            return true;
            
        }

        public void AddLineToDialogBox(string line)
        {
            ta.AddLine(line);
        }
    }
}
