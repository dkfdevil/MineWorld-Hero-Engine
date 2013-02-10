using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroEngine.Objects.Weapons.Bullets
{
    class BaseBullet : Entity
    {
        float FiredDirection;
        float Speed;
        int Damage;
        public BaseBullet(EntityManager manager, string bulletname)
            : base(bulletname, manager, manager.SpriteBatch, manager.Camera)
        {

        }

        public override void Think()
        {
            //Move our bullet forward.
        }

        public override void Draw()
        {

        }
    }
}
