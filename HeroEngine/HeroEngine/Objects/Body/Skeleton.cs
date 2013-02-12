using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroEngine.Render;
namespace HeroEngine.Objects.Body
{
    class Skeleton
    {
        List<Bone> bones = new List<Bone>();
        Entity parent;
        public Vector2 position;
        public float rotation;
        SpriteBatch sb;
        bool IsScaled = true;
        public Skeleton(Entity parent,bool Scaled)
        {
            this.parent = parent;
            IsScaled = Scaled;
            sb = parent.SpriteBatch;
        }

        public void ParentBone(string name, AnimatedTexture texture, Rectangle pos, Vector2 pivot, float rotationoffset = 0, Bone parent = null)
        {
            Bone bone = new Bone(texture, bones.Count, pos, pivot, this, parent, IsScaled);
            bones.Add(bone);
            bone.name = name;
        }

        public void Update()
        {
            Point TileSize = new Point((int)EngineLimit.TileScaledX, (int)EngineLimit.TileScaledY);
            Point precise = new Point((int)(parent.speed_buffer.X * ((float)TileSize.X / 20)), (int)(parent.speed_buffer.Y * ((float)TileSize.Y / 20)));
            position.X = (int)((parent.Marker.x - parent.camera.CameraPosOnWorld.X) * EngineLimit.TileScaledX);
            position.Y = (int)((parent.Marker.y - parent.camera.CameraPosOnWorld.Y) * EngineLimit.TileScaledY);

            if (!parent.IsTracked)
            {
                position.X += precise.X - (TileSize.X / 2);
                position.Y += precise.Y - (TileSize.Y / 2);
            }

            foreach (Bone bone in bones )
            {
                Rectangle newpos = new Rectangle(bone.relative.X + (int)position.X, bone.relative.Y + (int)position.Y, bone.relative.Width, bone.relative.Height);
                bone.Update(newpos,rotation);
            }
        }

        public void Draw()
        {
            foreach (Bone bone in bones)
            {
                sb.Draw(bone.texture.sheet, bone.position,bone.texture.GetSourceRectange() ,Color.White,bone.rotation,bone.pivot,SpriteEffects.None,0f);
            }
        }

        public Bone GetBone(string name)
        {
            foreach (var bone in bones)
            {
                if (bone.name == name)
                {
                    return bone;
                }
                
            }

            return null;
        }

    }
}
