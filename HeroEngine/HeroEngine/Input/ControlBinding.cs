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
using HeroEngine.CoreGame;
using HeroEngine.Render;
using System.IO;
using HeroEngineShared;

namespace HeroEngine.Input
{
    public class ControlBinding
    {
        /// <summary>
        /// Sets up player bindings so controls can be mapped to strings.
        /// </summary>
        public ControlBinding()
        {
        }

        //Keyboard
        Dictionary<ClientKey, Keys> Controlbindings = new Dictionary<ClientKey, Keys>();

        public void SetBind(ClientKey ckey, Keys key)
        {
            if(Controlbindings.ContainsKey(ckey))
            {
                Controlbindings[ckey] = key;
            }
            else
            {
                Controlbindings.Add(ckey, key);
            }
        }

        public Keys GetBindKey(ClientKey ckey)
        {
            if (Controlbindings.ContainsKey(ckey))
            {
                return Controlbindings[ckey];
            }
            else
            {
                //We are checking for a key that doesnt exsist
                return Keys.None;
            }
        }

        public void SaveBinding()
        {
            StringBuilder bindingtext = new StringBuilder();

            foreach (KeyValuePair<ClientKey,Keys> pair in Controlbindings)
            {
                bindingtext.AppendLine(pair.Key + "=" + pair.Value);
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Binding + Constants.HeroEngine_Config_Extension, bindingtext.ToString());
        }

        public void LoadBinding()
        {
            string[] bindinglines = File.ReadAllLines(Directory.GetCurrentDirectory() + Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Binding + Constants.HeroEngine_Config_Extension);
            for (int i = 0; i < bindinglines.Length; i++)
            {
                int indexofequals = bindinglines[i].IndexOf("=");
                SetBind((ClientKey)Enum.Parse(typeof(ClientKey), bindinglines[i].Substring(0, indexofequals)), (Keys)Enum.Parse(typeof(Keys), bindinglines[i].Substring(indexofequals + 1)));
            }
        }
    }
}
