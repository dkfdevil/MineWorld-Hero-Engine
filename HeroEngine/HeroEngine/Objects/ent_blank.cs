using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeroEngine.Screen;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.LevelEditing;
using HeroEngine.ScreenManagement.Screens;
using HeroEngine.Objects;
using HeroEngine.CoreGame;
using HeroEngine.Render;
using C3;
namespace HeroEngine.Objects
{
    class ent_blank : Entity
    {
        public ent_blank(string name, EntityManager manager, SpriteBatch spriteBatch, Camera cam)
            : base(name, manager, spriteBatch, cam)
        {

        }

        public override void Initialize()
        {
            Marker.x = 25;
            Marker.y = 25;
            AlwaysDrawn = true;
            base.Initialize();
        }

        //Think for entity.
        public override void Think()
        {

            base.Think();
        }

        //Draw the entity.
        public override void Draw()
        {
            C3.XNA.Primitives2D.DrawRectangle(SpriteBatch, new Rectangle(Marker.x, Marker.y, 1, 1), Color.Purple, 1);
            base.Draw();
        }
    }
}
