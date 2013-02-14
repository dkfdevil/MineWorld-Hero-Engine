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
using HeroEngine.LevelEditing;
using HeroEngine.Operations;
using HeroEngine.Render;
using HeroEngine.AI;
using HeroEngine.CoreGame;
using HeroEngine.Objects.Body;

namespace HeroEngine.Objects
{

    class Player : Entity
    {
        public static Point PLR_SIZE = new Point(16, 16); 
        public bool IsControlled = false;
        public Point bounds = new Point(4, 4);
        public Point SpeedMod = new Point();
        //Player Stats
        public Skeleton skeleton;
        public float Money = 0;
        Point TileSize = new Point((int)(EngineLimit.TileScaleX * EngineLimit.TileSize), (int)(EngineLimit.TileScaleY * EngineLimit.TileSize));
        Viewport vp;
        KeyCheck torchkey;
        //Control Bindings
        public ControlBinding binding = new ControlBinding();
        public bool torch_enable = false;
        public float[] angles = MiscOperations.CommonAnglesToRadians();
        public Player(string name, int Health, TileMarker marker, bool isControlled,Viewport viewport,ControlBinding control,EntityManager manager,SpriteBatch sb,Camera camera) : base(name,manager,sb,camera)
        {
            Name = name;
            this.Health = Health;
            Marker = marker;
            IsControlled = isControlled;
            vp = viewport;
            binding = control;

            skeleton = new Skeleton(this,true);

            Texture2D holder = (Texture2D)GameResources.textures.GetResource("PLAYER_WALK");
            skeleton.ParentBone("LEGS", new AnimatedTexture(holder, new Rectangle(0, 0, 24, 48), 9, 200), new Rectangle(0, 0, 23, 48), Vector2.Zero);

            holder = GameResources.textures.GetResource("PLAYER_STAND");
            skeleton.ParentBone("HEAD", new AnimatedTexture(holder, holder.Bounds, 1, 1000), holder.Bounds, Vector2.Zero);
            
            skeleton.GetBone("HEAD").ParentRotation = true;
            torchkey = new KeyCheck(binding.GetBoundKeyName("TORCH"));
        }

        public override void Initialize()
        {
            base.Initialize();
            //We set IsDrawn to true as the game dosn't detect a texture by default.
            IsDrawn = true;
            Health = 100;
            Armour = 15;
            IsTracked = IsControlled;
        }

        public override void Think()
        {
            Movement();
            Rotation(); //Rotation Controls
            OtherControls(); //Torch or crouch.
            SpeedMod.X = (int)(GameVariables.PLR_SPEEDX * EngineLimit.TileScaleX);
            SpeedMod.Y = (int)(GameVariables.PLR_SPEEDY * EngineLimit.TileScaleY);
            //Placement on screen.
            Size.Width = (int)(PLR_SIZE.X * EngineLimit.TileScaleX) ;
            Size.Height = (int)(PLR_SIZE.Y * EngineLimit.TileScaleY);
            origin.X = Size.Width / 2;
            origin.Y = Size.Height / 2;
            base.Think();
            //Set up skeleton
            skeleton.GetBone("HEAD").rotation = skeleton.rotation;
            skeleton.position = new Vector2(pos.X, pos.Y);
            skeleton.rotation = this.rotation;
            skeleton.Update();
        }

        public override void Draw()
        {
            
            //base.Draw();
            //Using a skeleton

            skeleton.Draw();
        }

        public bool Movement()
        {
            bool moved = false;
            Point TileSize = new Point((int)(EngineLimit.TileScaleX * EngineLimit.TileSize), (int)(EngineLimit.TileScaleY * EngineLimit.TileSize));
            moved = SpeedBuffer();
            #region movement&binds
            if (binding.GetBindValue("MOVEUP") && Marker.y > bounds.Y)
            {
                if (!CanMove(Direction.North)) { return false; }
                speed_buffer.Y -= (float)SpeedMod.Y / TileSize.Y;
                moved = true;
                skeleton.GetBone("LEGS").texture.StepFrameForward();

                //Slowly move legs around.
                //if (skeleton.GetBone("LEGS").rotation < angles[(int)Direction.North])
                //{
                //    skeleton.GetBone("LEGS").rotation += GameVariables.PLR_LEG_TURN_SPEED;
                //}
                //else if (skeleton.GetBone("LEGS").rotation > angles[(int)Direction.North])
                //{
                //    skeleton.GetBone("LEGS").rotation -= GameVariables.PLR_LEG_TURN_SPEED;
                //}
                TurnLegs((int)MathHelper.ToDegrees(skeleton.GetBone("LEGS").rotation), DegreesDirection.North);
                
            }

            if (binding.GetBindValue("MOVEDOWN") && Marker.y < EngineLimit.TotalMapAreaHeight - bounds.Y)
            {
                if (!CanMove(Direction.South)) { return false; }
                speed_buffer.Y += (float)SpeedMod.Y / TileSize.Y;
                moved = true;
                skeleton.GetBone("LEGS").texture.StepFrameForward();

                //Slowly move legs around.

                //if (skeleton.GetBone("LEGS").rotation < angles[(int)Direction.South])
                //{
                //    skeleton.GetBone("LEGS").rotation += GameVariables.PLR_LEG_TURN_SPEED;
                //}
                //else if (skeleton.GetBone("LEGS").rotation > angles[(int)Direction.South])
                //{
                //    skeleton.GetBone("LEGS").rotation -= GameVariables.PLR_LEG_TURN_SPEED;
                //}
                TurnLegs((int)MathHelper.ToDegrees(skeleton.GetBone("LEGS").rotation), DegreesDirection.South);
            }

            if (binding.GetBindValue("MOVELEFT") && Marker.x > bounds.X)
            {
                if (!CanMove(Direction.West)) { return false; }
                speed_buffer.X -= (float)SpeedMod.X / TileSize.X;
                moved = true;
                skeleton.GetBone("LEGS").texture.StepFrameForward();

                //Slowly move legs around.

                //if (skeleton.GetBone("LEGS").rotation < angles[(int)Direction.West])
                //{
                //    skeleton.GetBone("LEGS").rotation += GameVariables.PLR_LEG_TURN_SPEED;
                //}
                //else if (skeleton.GetBone("LEGS").rotation > angles[(int)Direction.West])
                //{
                //    skeleton.GetBone("LEGS").rotation -= GameVariables.PLR_LEG_TURN_SPEED;
                //}

                TurnLegs((int)MathHelper.ToDegrees(skeleton.GetBone("LEGS").rotation), DegreesDirection.West);
            }


            if (binding.GetBindValue("MOVERIGHT") && Marker.x < EngineLimit.TotalMapAreaWidth - bounds.X)
            {
                if (!CanMove(Direction.East)) { return false; }
                speed_buffer.X += (float)SpeedMod.X / TileSize.X;
                skeleton.GetBone("LEGS").texture.StepFrameForward();

                //if (skeleton.GetBone("LEGS").rotation < angles[(int)Direction.East])
                //{
                //    skeleton.GetBone("LEGS").rotation += GameVariables.PLR_LEG_TURN_SPEED;
                //}
                //else if (skeleton.GetBone("LEGS").rotation > angles[(int)Direction.East])
                //{
                //    skeleton.GetBone("LEGS").rotation -= GameVariables.PLR_LEG_TURN_SPEED;
                //}

                TurnLegs((int)MathHelper.ToDegrees(skeleton.GetBone("LEGS").rotation), DegreesDirection.East);
            }

            #endregion
            //Check to see if i buggered up the speed_buffer.
            if (float.IsNaN(speed_buffer.X) || float.IsNaN(speed_buffer.Y))
            {
                throw new Exception("speed_buffer buggered up. Your a crap programmer, thats how you fix it you idiot, go to xna school.");
            }


            return moved;
        }
        public bool SpeedBuffer()
        {
            Point transition = new Point(
                    (int)((TileSize.X / 20f) * 0),
                    (int)((TileSize.Y / 20f) * 0)
                    );

            if (speed_buffer.X > EngineLimit.TileSize / 2 + transition.X)
            {
                if (!CanMove(Direction.East)) { return false; }
                Marker.x += 1;
                speed_buffer.X = -10;
            }

            if (speed_buffer.Y > EngineLimit.TileSize / 2 + transition.Y)
            {
                if (!CanMove(Direction.South)) { return false; }
                Marker.y += 1;
                speed_buffer.Y = -10;
            }

            if (speed_buffer.X < -(EngineLimit.TileSize / 2) - transition.X)
            {
                if (!CanMove(Direction.West)) { return false; }
                Marker.x -= 1;
                speed_buffer.X = 10;
            }

            if (speed_buffer.Y < -(EngineLimit.TileSize / 2) - transition.Y)
            {
                if (!CanMove(Direction.North)) { return false; }
                Marker.y -= 1;
                speed_buffer.Y = 10;
            }

            return true;
        }

        public void Rotation()
        {
            Vector2 relative = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - new Vector2(pos.X + pos.Width / 2, pos.Y + pos.Height / 2);
            rotation = (float)Math.Atan2(relative.Y, relative.X) + MathHelper.ToRadians(90);
        }

        public void OtherControls()
        {

            if (binding.GetBindValue("TORCH"))
                torch_enable = !torch_enable;
                 

        }

        public bool CanMove(Direction dir)
        {
            if (GameVariables.PLR_NOCLIP) { return true; } //Is the player unblocked by anything XD
            Map map = EntityManager.currentMap;
            //Check through ground
            switch (dir)
            {
                case Direction.West:
                    return map.ground[Marker.x - 1, Marker.y].CanWalkOn;
                case Direction.East:
                    return map.ground[Marker.x + 1, Marker.y].CanWalkOn;
                case Direction.North:
                    return map.ground[Marker.x, Marker.y - 1].CanWalkOn;
                case Direction.South:
                    return map.ground[Marker.x, Marker.y + 1].CanWalkOn;
                default:
                    return true;
            }

        }

        public void TurnLegs(int current, DegreesDirection dest)
        {
            if (current == (int)dest)
            {
                return;
            }

            if (current < (int)dest)
            {
                skeleton.GetBone("LEGS").rotation += MathHelper.ToRadians(GameVariables.PLR_LEG_TURN_SPEED);
            }
            else if (current > (int)dest)
            {
                skeleton.GetBone("LEGS").rotation -= MathHelper.ToRadians(GameVariables.PLR_LEG_TURN_SPEED);
            }
        }
    }
}
