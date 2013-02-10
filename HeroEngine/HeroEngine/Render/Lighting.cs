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
namespace HeroEngine.Render
{
    class Lighting
    {
        public Color light_level = new Color(0,0,0,0);
        public float[] RawLight = new float[3];
        public void Update(Map map)
        {   
            //Get Time of Day
            DateTime time = map.world_time;
            float light = 0;
            if (time.Hour < 12)
            {
                light = ((float)time.TimeOfDay.TotalMinutes / 60) / 12f;
            }
            else
            {
                light = (float)(24 -((float)time.TimeOfDay.TotalMinutes / 60)) / 24f;
            }

            light += 0.04f;
            RawLight[0] = light;
            RawLight[1] = light;
            RawLight[2] = light;
            light_level = new Color(light, light, light, 1);
        }
    }
}
