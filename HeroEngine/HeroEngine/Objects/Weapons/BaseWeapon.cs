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
using HeroEngine.Objects;
using HeroEngine.Objects.Weapons.Bullets;
using HeroEngine.CoreGame;

namespace HeroEngine.Objects.Weapons
{ 
    class BaseWeapon
    {
        /// <summary>
        /// A base weapon template for weapon classes.
        /// </summary>
        /// <param name="User">The entity using the gun. Player or otherwise.</param>
        /// <param name="textures">Each texture used by the gun.
        /// 0 Drop
        /// 1 Held
        /// 2 Flash
        /// </param>
        Entity User;
        Texture2D[] textures;
        BaseBullet Bullet_Type;
        int CurrentAmmo;
        int CurrentClips;

        int MaxClips;
        int MaxClipSize;

        bool Automatic;
        public BaseWeapon(int StartClips, Entity parent, Texture2D[] preloaded_textures)
        {
            User = parent;
            StartClips = CurrentClips;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Flash()
        {

        }

        public virtual void Ammo()
        {

        }

        public virtual bool Fire()
        {
            if (CurrentAmmo < 1)
            {
                return false;
            }
            //Ammo Math
            CurrentAmmo--; //Lose us one bullet.
            //Fire a bullet

            //New clip
            if (CurrentAmmo < 1 && GameVariables.PLR_AUTORELOAD && CurrentClips > 1)
            {
                CurrentClips--;
                CurrentAmmo = MaxClipSize;
            }
            return true;
        }
    }
}
