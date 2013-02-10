using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace HeroEngine.CoreGame
{
    class GamePadState
    { 
        /// <summary>
        /// Returns a array responding to the buttons pressed in Boolean. This does not test the GamePads Joysticks or back triggers.
        /// </summary>
        /// <param name="GamePadID">Gamepad Number to testify</param>
        public bool[] GetState(PlayerIndex GamePadID)
        {
            bool[] butstate = new bool[13];
            if (GamePad.GetState(GamePadID).Buttons.A == ButtonState.Pressed) //A (0)
                butstate[0] = true;
            if (GamePad.GetState(GamePadID).Buttons.B == ButtonState.Pressed) //B (1)
                butstate[1] = true;
            if (GamePad.GetState(GamePadID).Buttons.X == ButtonState.Pressed) //X (2)
                butstate[2] = true;
            if (GamePad.GetState(GamePadID).Buttons.Y == ButtonState.Pressed) //Y (3)
                butstate[3] = true;
            if (GamePad.GetState(GamePadID).Buttons.LeftStick == ButtonState.Pressed) //Left Joystick Button (4)
                butstate[4] = true;
            if (GamePad.GetState(GamePadID).Buttons.RightStick == ButtonState.Pressed) //Right Joystick Button (5)
                butstate[5] = true;
            if (GamePad.GetState(GamePadID).Buttons.Start == ButtonState.Pressed) //Start (6)
                butstate[6] = true;
            if (GamePad.GetState(GamePadID).Buttons.Back == ButtonState.Pressed) //Back (7)
                butstate[7] = true;
            if (GamePad.GetState(GamePadID).DPad.Left == ButtonState.Pressed) //DPAD Left (8)
                butstate[8] = true;
            if (GamePad.GetState(GamePadID).DPad.Down == ButtonState.Pressed) //DPAD Down (9)
                butstate[9] = true;
            if (GamePad.GetState(GamePadID).DPad.Right == ButtonState.Pressed) //DPAD Right (10)
                butstate[10] = true;
            if (GamePad.GetState(GamePadID).DPad.Up == ButtonState.Pressed) //DPAD Up (11)
                butstate[11] = true;
            if (GamePad.GetState(GamePadID).Buttons.LeftShoulder == ButtonState.Pressed) //Top Trigger Left (12)
                butstate[12] = true;
            if (GamePad.GetState(GamePadID).Buttons.RightShoulder == ButtonState.Pressed) //Top Trigger Right (13)
                butstate[13] = true;
            return butstate;
        }
    }
}
