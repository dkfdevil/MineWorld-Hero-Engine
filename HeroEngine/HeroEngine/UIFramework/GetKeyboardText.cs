using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace HeroEngine.UIFramework
{
    class GetKeyboardText
    {
        public static char GetKeyChar()
        {

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                Keys key = Keyboard.GetState().GetPressedKeys()[0];
                char chr =  key.ToString().ToCharArray()[0];

                return chr;
            }
            return new char();
        }
    }
}
