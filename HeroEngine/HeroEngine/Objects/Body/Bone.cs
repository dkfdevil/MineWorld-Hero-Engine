using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroEngine.Render;
namespace HeroEngine.Objects.Body
{
    class Bone
    {
        public string name = "Bone";
        public int id = 0;
        public AnimatedTexture texture;
        public Rectangle position;
        public Vector2 pivot;
        public float rotation;
        public Rectangle relative;
        public bool ParentRotation = false;
        Skeleton skeleton_parent;
        Bone bone_parent;

        public Bone(AnimatedTexture tex, int id, Rectangle pos, Vector2 pivot, Skeleton parent, Bone bone)
        {
            texture = tex;
            this.id = id;
            position = pos;
            relative = pos;
            if (pivot != Vector2.Zero) { this.pivot = pivot; } else { this.pivot = new Vector2(position.Width / 2, position.Height / 2); }
            skeleton_parent = parent;
            if (bone != null) { bone_parent = bone; }
        }

        public void Update(Rectangle newpos, float rot)
        {
            position = newpos;
            if (ParentRotation) { rotation = rot; }
            
        }
    }
}
