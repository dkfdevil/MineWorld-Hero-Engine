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
using HeroEngine.Operations;
namespace HeroEngine.Sound
{
    class SoundEffectPlayer
    {
        List<SoundEffectInstance> sounds = new List<SoundEffectInstance>();
        List<string> sound_name = new List<string>();
        public SoundEffectPlayer()
        {

        }

        public void AddSoundToBank(SoundEffect se, string name)
        {
            sounds.Add(se.CreateInstance());
            sound_name.Add(name);
        }

        public void PlaySound(string name)
        {
            int i = ArrayFunc.GetIndexOfObject(sound_name.ToArray(), name);

            if (i == -1)
            {
                throw new Exception("You tried to play " + name + " but it wasn't loaded into the soundbank.");
            }

            sounds[i].Play();
        }

        public void RemoveSound(string name)
        {
            int i = ArrayFunc.GetIndexOfObject(sound_name.ToArray(), name);

            if (i == -1)
            {
                throw new Exception("You tried to remove " + name + " but it wasn't loaded into the soundbank.");
            }

            if (sounds[i].State != SoundState.Stopped)
            {
                sounds.RemoveAt(i);
            }
        }
    }
}
