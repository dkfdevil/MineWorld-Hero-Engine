using Microsoft.Xna.Framework.Input;

namespace HeroEngine.Input
{
    public enum ClientKey
    {
        MoveForward = Keys.W,
        MoveLeft = Keys.A,
        MoveRight = Keys.D,
        MoveBack = Keys.S,
        Run = Keys.LeftShift,

        //Ugly need to find a better solution
        ActionOne = MouseButtons.LeftButton,
        ActionTwo = MouseButtons.RightButton,

        Debug = Keys.F1,
        FullScreen = Keys.F3,
        Exit = Keys.Escape,
    }
}
