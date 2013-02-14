using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroEngine.LevelEditing;
using HeroEngine.Render;
using HeroEngine.CoreGame;

namespace HeroEngine.Objects
{
    class NPC : Entity 
    {
        public NPC(string name, EntityManager manager, SpriteBatch spriteBatch, Camera cam) : base(name,manager,spriteBatch,cam)
        {

        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Think()
        {
            base.Think();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public virtual void ACT_IDLE()
        {

        }

        public virtual void ACT_ATTACK()
        {

        }

        public virtual void ACT()
        {

        }
    }
}
