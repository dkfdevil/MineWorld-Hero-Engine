using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroEngine.Operations
{
    class Toggle
    {
        public static bool ToggleBool(bool value)
        {
            if (value == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
