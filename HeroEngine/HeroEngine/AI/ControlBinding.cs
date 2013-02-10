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
using System.Threading;
using HeroEngine.Screen;
using HeroEngine.CoreGame;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.ScreenManagement.Screens;
using HeroEngine.Render;
namespace HeroEngine.AI
{
    class ControlBinding
    {
        /// <summary>
        /// Sets up player bindings so controls can be mapped to strings. Can read from cfg files.
        /// </summary>
        /// <param name="index">Player number</param>
        public ControlBinding(PlayerIndex index = PlayerIndex.One)
        {
            plr_index = index;
        }
        //Keyboard
        List<string> actions = new List<string>();
        List<Keys> bound_key = new List<Keys>();
        PlayerIndex plr_index;
        //Xbox 360
        List<string> action_360 = new List<string>();
        List<Buttons> bound_360 = new List<Buttons>();

        /// <summary>
        /// Creates a new binding to the keyboard. An action will be created if it does not exist.
        /// </summary>
        /// <param name="action">The unique action the key is bound to.</param>
        /// <param name="key">The key to be bound.</param>
        public void SetBind(string action, Keys key)
        {
            if (actions.Contains(action))
                bound_key[FindBindIndex(action)] = key;
            else
            {
                actions.Add(action);
                bound_key.Add(key);
            }
        }

        /// <summary>
        /// Creates a new binding to the 360 Controller. An action will be created if it does not exist.
        /// </summary>
        /// <param name="action">The unique action the key is bound to.</param>
        /// <param name="button">The button to be bound</param>
        public void SetBind360(string action, Buttons button)
        {
            if (actions.Contains(action))
                bound_360[FindBindIndex360(action)] = button;
            else
            {
                actions.Add(action);
                bound_360.Add(button);
            }
        }

        /// <summary>
        /// Is the bound key or button pressed.
        /// </summary>
        /// <param name="action">The action to be checked.</param>
        /// <returns></returns>
        public bool GetBindValue(string action)
        {
            if (Keyboard.GetState().IsKeyDown(bound_key[FindBindIndex(action)]) || GamePad.GetState(plr_index).IsButtonDown(bound_360[FindBindIndex360(action)]))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find the key the action has been bound to. If no keys are bound, it returns Keys.None.
        /// </summary>
        /// <param name="action">The action to be checked.</param>
        /// <returns></returns>
        public Keys GetBoundKeyName(string action)
        {
            if(FindBindIndex(action) < 0)
            {
                return Keys.None;
            }
            return bound_key[FindBindIndex(action)];
        }

        /// <summary>
        /// Find the button the action has been bound to. If no buttons are bound, it returns -1.
        /// </summary>
        /// <param name="action">The action to be checked.</param>
        /// <returns></returns>
        public Buttons GetBoundButtomName(string action)
        {
            if (FindBindIndex360(action) < 0)
            {
                return (Buttons)(-1);
            }

            return bound_360[FindBindIndex360(action)];
        }

        private int FindBindIndex(string action)
        {   
            int i = 0;
            foreach (var item in actions)
            {
                if (item == action)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private int FindBindIndex360(string action)
        {
            int i = 0;
            foreach (var item in action_360)
            {
                if (item == action)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Save the current control binding to file.
        /// </summary>
        /// <param name="file">Full file path to save to.</param>
        public void SaveBinding(string file = "binding")
        {
            string[] squished = new string[actions.Count];
            for (int i = 0; i < actions.Count; i++)
            {
                squished[i] = actions[i] + "=" + bound_key[i].ToString();
            }
            System.IO.File.Delete(file + ".cfg");
            System.IO.File.CreateText(file + ".cfg");
        }

        /// <summary>
        /// Loads a control binding to this class.
        /// </summary>
        /// <param name="file">Full file path to load from.</param>
        public void LoadBinding(string file = "binding")
        {
            string[] squished = System.IO.File.ReadAllLines(file + ".cfg");
            for (int i = 0; i < squished.Length; i++)
            {
                int indexofequals = squished[i].IndexOf("=");
                SetBind(squished[i].Substring(0,indexofequals),(Keys)Enum.Parse(typeof(Keys),squished[i].Substring(indexofequals + 1)));
                
            }

        }
    }
}
