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
    public class TextArea
    {
        List<string> lines;
        SpriteFont Spr_font;
        Color DefaultColor;
        public Rectangle res;
        int capacity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="font"></param>
        /// <param name="TextColor1">The only required parameter for Color.</param>
        public TextArea(SpriteFont font, Color TextColor1, Rectangle size)
        {
            Spr_font = font;
            DefaultColor = TextColor1;
            res = size;
            capacity = size.Height / font.LineSpacing;
            lines = new List<string>(capacity + 1);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.FillRectangle(res, Color.DarkSlateGray);
            sb.DrawRectangle(res, Color.DarkSlateGray, 4);
            int count = 0;
            foreach (string item in lines)
            {
                sb.DrawString(Spr_font, item, new Vector2(res.X, res.Y + (count * Spr_font.LineSpacing)), DefaultColor);
                count++;
            }
        }

        public void AddLine(string line)
        {
            lines.Add(line);

            if(lines.Count > 1)
                ReSizeBuffer();
        }

        public void ReSizeBuffer()
        {
            if (lines.Count > capacity)
            {
                int count = 0;
                foreach (string item in lines)
                {
                    lines[count] = lines[count + 1];
                    count++;
                }
            }
        }
    }
}
