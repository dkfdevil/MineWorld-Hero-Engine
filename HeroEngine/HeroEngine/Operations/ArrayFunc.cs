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
using HeroEngine.Operations;
namespace HeroEngine.Operations
{
    class ArrayFunc
    {
        public static string GetLargestStringByChars(string[] array)
        {
            string selected = "";
            float size = selected.Length;
            foreach (string str in array)
            {
                if (str.Length > size)
                    size = str.Length;
                    selected = str;
            }

            return selected;
        }

        public static float GetLargestSizeX(SpriteFont font, string[] array)
        {
            float size = 0;
            foreach (string str in array)
            {
                if (font.MeasureString(str).X > size)
                    size = font.MeasureString(str).X;
            }
            return size;
        }

        public static float GetLargestSizeY(SpriteFont font, string[] array)
        {
            float size = 0;
            foreach (string str in array)
            {
                if (font.MeasureString(str).X > size)
                    size = font.MeasureString(str).Y;
            }
            return size;
        }

        public static string GetLargestStringBySize(SpriteFont font,string[] array)
        {
            string selected = "";
            float size = font.MeasureString(selected).X;
            foreach (string str in array)
            {
                if (font.MeasureString(str).X > size)
                    size = font.MeasureString(str).X;
                    selected = str;
            }

            return selected;
        }

        public static float GetTotalSizeY(int start_index, int count, string[] array, SpriteFont font, float spacing)
        {
            float accumilate = 0;
            for (int i = start_index; i < start_index + count; i++)
            {
                accumilate += font.MeasureString(array[i]).Y;
                accumilate += spacing;
            }
            return accumilate;
        }

        public static int GetIndexOfObject(object[] array, object item)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static object[,] GetChunk(object[,] array, Rectangle bounds)
        {
            object[,] chunk = new object[bounds.Width, bounds.Height];

            for (int x = bounds.X; x < bounds.Width; x++)
            {
               for (int y = bounds.Y; x < bounds.Height; y++)
               {
                   chunk[x - bounds.X,y - bounds.Y] = array[x, y];
               }
            }

            return chunk;
        }
    }
}
