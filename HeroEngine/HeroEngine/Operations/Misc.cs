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
    class MiscOperations
    {
        public static string[] GetDisplayModesToString(DisplayModeCollection items, float aspectlimit)
        {
            List<string> modes = new List<string>();
            int i = 0;
            foreach (var item in items)
            {
                string itm = items.ToArray<DisplayMode>()[i].Width + " " + items.ToArray<DisplayMode>()[i].Height;
                if (!ArrayContains(modes.ToArray(), itm) && Math.Round(item.AspectRatio, 1) == Math.Round(aspectlimit, 1))
                {
                    modes.Add(itm);
                }
                i++;
            }
            
            return modes.ToArray();
        }

        public static bool ArrayContains(object[] objs, object item)
        {
            foreach (var obj in objs)
	        {
                if (obj.Equals(item))
                    return true;
	        }

            return false;
        }

        public static float[] CommonAnglesToRadians()
        {
            float[] angles = new float[8];
            float angle = -MathHelper.ToRadians(45);
            int i = 0;
            foreach (var item in angles)
            {
                angle += MathHelper.ToRadians(45);
                angles[i] = angle;
                i++;
            }

            return angles;
        }
    }
}
