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
using C3.XNA;
using HeroEngine.LevelEditing;
using HeroEngine.Objects;
namespace HeroEngine.Render
{
    class HUD
    {
        Texture2D[] bars = new Texture2D[2];
        Texture2D[] xpbar = new Texture2D[2];
        Texture2D[] ammo_nut = new Texture2D[2];
        Texture2D t_radar;
        Rectangle radar_rect;
        Rectangle hpbar_rect;
        Rectangle armour_rect;
        Rectangle hpbar_mask;
        Rectangle armour_mask;
        Player plr;
        public HUD (Viewport vp, Texture2D radar,Texture2D[] statbars)
        {
            bars = statbars;
            radar_rect = new Rectangle(vp.Width / 10, (vp.Height / 20) * 16, 73, 74);
            hpbar_rect = new Rectangle(vp.Width / 4, (vp.Height / 20) * 17, bars[0].Width, bars[0].Height);
            armour_rect = new Rectangle(vp.Width / 2, (vp.Height / 20) * 17, bars[1].Width, bars[1].Height);
            t_radar = radar;

        }

        public void Draw(SpriteBatch sb, Map mp,TileRenderer tl)
        {
            Radar(mp, sb,tl);
            sb.Draw(t_radar, radar_rect, Color.White);
            sb.Draw(bars[0], hpbar_rect, hpbar_mask, Color.White);
            sb.Draw(bars[1], armour_rect, armour_mask, Color.White);
        }

        public void Update(Player plr_o)
        {
            plr = plr_o;

            //Health
            hpbar_mask = new Rectangle(0, 0, 43 + (int)(((bars[0].Width - 43) / 100) * plr.Health), 42);
            hpbar_rect = new Rectangle(hpbar_rect.X, hpbar_rect.Y, (int)(((bars[0].Width - 43) / 100) * plr.Health), 42);
            //Armour
            armour_mask = new Rectangle(0, 0, 43 + (int)(((bars[1].Width - 43) / 100) * plr.Armour), 42);
            armour_rect = new Rectangle(armour_rect.X, armour_rect.Y, 43 + (int)(((bars[1].Width - 43) / 100) * plr.Armour), 42);

        }


        public void Radar(Map refmap,SpriteBatch sb,TileRenderer tr)
        {
            Point screensize = new Point(radar_rect.Width - 5,radar_rect.Height - 5);
            Color[,] tiles = new Color[screensize.X, screensize.Y];

            for (int x = -(screensize.Y / 2); x < screensize.X / 2; x++)
            {
                for (int y = -(screensize.Y /2); y < screensize.Y / 2; y++)
                {
                    int xtile = x;
                    int ytile = y;

                    Point tile = new Point(plr.Marker.x + xtile, plr.Marker.y + ytile);

                    if (tile.X < 1 || tile.Y < 1)
                    {
                        tiles[x + (screensize.Y / 2), y + (screensize.Y / 2)] = Color.Blue;
                        continue;
                    }

                    if (tile.X >= EngineLimit.TotalMapAreaWidth || tile.Y >= EngineLimit.TotalMapAreaHeight)
                    {
                        tiles[x + (screensize.Y / 2), y + (screensize.Y / 2)] = Color.Blue;
                        continue;
                    }

                    if (refmap.ground[tile.Y, tile.X].Type == "WATER")
                    {
                        tiles[x + (screensize.Y / 2), y + (screensize.Y / 2)] = Color.Blue;
                    }

                    switch (refmap.ground[tile.Y, tile.X].Type)
                    {
                        case "WATER":
                            tiles[x + (screensize.Y / 2), y + (screensize.Y / 2)] = Color.Blue;
                            break;

                        case "SAND":
                            tiles[x + (screensize.Y / 2), y + (screensize.Y / 2)] = Color.Yellow;
                            break;

                        case "GRASS":
                            tiles[x + (screensize.Y / 2), y + (screensize.Y / 2)] = Color.Green;
                            break;
                    }
                }
            }


            for (int x = 6; x < screensize.X; x++)
            {
                for (int y = 6; y < screensize.Y; y++)
                {

                    if (x == screensize.X / 2 && y == screensize.Y / 2)
                    {
                        C3.XNA.Primitives2D.PutPixel(sb, new Vector2(radar_rect.X + screensize.X / 2, radar_rect.Y + screensize.Y / 2), Color.Red);
                    }
                    else
                    {
                        C3.XNA.Primitives2D.PutPixel(sb, new Vector2(radar_rect.X + x, radar_rect.Y + y), tiles[x - 6, y - 6]);
                    }
                }
            }
            
        }
    }
}
