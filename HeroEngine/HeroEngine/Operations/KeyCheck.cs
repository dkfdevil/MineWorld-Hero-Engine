using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using HeroEngine.Screen;
using HeroEngine.CoreGame;
using HeroEngine.UIFramework;
namespace HeroEngine.Operations
{
    class KeyCheck
    {
        public KeyCheck(Keys Key)
        {
            key = Key;
        }
        Keys key;
        bool down = false;
        bool up = false;
        
        public bool KeyDownAndUp()
        {

            if (Keyboard.GetState().IsKeyDown(key))
                down = true;

            if (Keyboard.GetState().IsKeyUp(key) && down)
                up = true;


            if(down && up)
            {
                down = false;
                up = false;
                return true;
            }

            return false;

        }
    }
}
