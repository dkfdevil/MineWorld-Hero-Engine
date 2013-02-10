using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace HeroEngine.Operations
{
    class RectangleOperations
    {
        public static bool InsideRectangle(bool AllIn,Rectangle Larger, Rectangle Smaller)
        {
            if (AllIn)
            {
                if(Smaller.X >= Larger.X &&
                   Smaller.X + Smaller.Width <= Larger.X &&
                   Smaller.Y >= Larger.Y &&
                   Smaller.Y + Smaller.Height <= Larger.Y
                   )
                {
                    return true;
                }

            }
            else
            {
                if (Smaller.X >= Larger.X &&
                    Smaller.X <= Larger.X + Larger.Width &&
                    Smaller.Y >= Larger.Y &&
                    Smaller.Y <= Larger.Y + Larger.Height
                    )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
