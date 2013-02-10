using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroEngine
{
    class GameStates
    {
        public int State;
        public string[] StateName;
        public GameStates(int states, string[] statenames)
        {
            State = 0;
            StateName = statenames;
        }

        public void ChangeState(int NewState)
        {
            State = NewState;
        }

        public string GetStateName()
        {
           return StateName[State];
        }
    }
}
