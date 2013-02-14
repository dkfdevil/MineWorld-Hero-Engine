using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroEngine.CoreGame
{
    /// <summary>
    /// Variables for details in the game. Will be editable in the console and menu.
    /// </summary>
    class GameVariables
    {
        //PLAYER
        public static float PLR_SPEEDX = 10;
        public static float PLR_SPEEDY = 10;
        public static bool PLR_NOCLIP = false;
        public static float PLR_LEG_TURN_SPEED = 4f; //0.01 - Very Slow 0.1 - Very Fast
        public static bool PLR_AUTORELOAD = true;

        //SOUND
        public static float SND_FX_VOL = 0.5f;
        public static float SND_MUSIC_VOL = 0.5f;
        public static float SND_MASTER_VOL = 1f;
    }
}
