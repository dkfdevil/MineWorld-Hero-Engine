using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace HeroEngine.Operations
{
    public enum Direction
    {
        North = 0, //0
        NorthEast = 1, //45
        East = 2, //90
        SouthEast = 3, //135
        South = 4, //180
        SouthWest = 5, //225
        West = 6, //270
        NorthWest = 7, //315
    }

    public enum DegreesDirection
    {
        North = 0, //0
        NorthEast = 45, //45
        East = 90, //90
        SouthEast = 135, //135
        South = 180, //180
        SouthWest = 225, //225
        West = 270, //270
        NorthWest = 315, //315
    }

    public static class DirectionOperations
    {
        public static Direction RadiansToDirection(float radians)
        {
            return DegreesToDirection((int)MathHelper.ToDegrees(radians));
        }

        public static Direction DegreesToDirection(int Degrees)
        {
            float div = Degrees / 45;
            //One segment is equal to 5.625;
            int dir = (int)Math.Round(div);
            if (dir == 8)
            {
                return Direction.North;
            }

            return (Direction)dir;
        }
    }
}
