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
        public Rectangle inital_rect;
        public Vector2 pivot;
        public float rotation;
        public Rectangle relative;
        public bool ParentRotation = false;
        Skeleton skeleton_parent;
        Bone bone_parent;
        bool isScaled;
        public Bone(AnimatedTexture tex, int id, Rectangle pos, Vector2 pivot, Skeleton parent, Bone bone, bool isScaled)
        {
            texture = tex;
            this.id = id;
            position = pos;
            relative = pos;
            inital_rect = pos;
            if (pivot != Vector2.Zero) { this.pivot = pivot; } else { this.pivot = new Vector2(position.Width / 2, position.Height / 2); }
            skeleton_parent = parent;
            if (bone != null) { bone_parent = bone; }
            this.isScaled = isScaled;
        }

        public void Update(Rectangle newpos, float rot)
        {
            position = newpos;
            if (ParentRotation) { rotation = rot; }
            if (isScaled)
                position = new Rectangle(newpos.X, newpos.Y, (int)(inital_rect.Width * EngineLimit.TileScaleX), (int)(inital_rect.Height * EngineLimit.TileScaleY));
        }
    }
}
