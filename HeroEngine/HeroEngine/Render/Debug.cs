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
using HeroEngine.Screen;
using HeroEngine.UIFramework;
using HeroEngine.Operations;
using HeroEngine.LevelEditing;
using HeroEngine.ScreenManagement.Screens;
namespace HeroEngine.Render
{
    class Debug
    {
        int tiles_rendering;
        int lightlevel;
        string timeofday;
        string fps;
        public bool isDebugging = false;
        string player_c;
        string camera;
        string playerrot;
        string precise;
        public void Update(Map map, int tiles, int lightlevel, string fps,string player_c,string camera,string precise,string rot, GameScreen game)
        {
            tiles_rendering = tiles;
            this.lightlevel = lightlevel;
            this.timeofday = map.world_time.ToShortTimeString();
            this.fps = fps;
            this.player_c = player_c;
            this.camera = camera;
            this.playerrot = rot;
            this.precise = precise;
        }

        public void Draw(SpriteBatch sb,SpriteFont font,int y = 0)
        {
            sb.DrawString(font,"T:" + tiles_rendering.ToString(),new Vector2(10,y),Color.Red);
            sb.DrawString(font, "LL:" + lightlevel.ToString(), new Vector2(10, y + 20), Color.Red);
            sb.DrawString(font, "Time:" + timeofday, new Vector2(10, y + 40), Color.Red);
            sb.DrawString(font, "FPS:" + fps, new Vector2(10, y + 60), Color.Red);
            sb.DrawString(font, "PLR:" + player_c, new Vector2(10, y + 80), Color.Red);
            sb.DrawString(font, " ^Precise:" + precise, new Vector2(10, y + 100), Color.Red);
            sb.DrawString(font, "P_ROT:" + playerrot, new Vector2(10, y + 120), Color.Red);
            sb.DrawString(font, "CAM:" + camera,new Vector2(10,y + 140),Color.Red);
            sb.DrawString(font, "Build " + EngineLimit.VersionNumber, new Vector2(EngineLimit.HalfScreenWidth - (font.MeasureString("Build " + EngineLimit.VersionNumber).X) / 2, 0), Color.White);
        }
    }
}
