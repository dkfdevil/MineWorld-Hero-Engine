using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
using C3;
namespace HeroEngine.Render
{
    class TileRenderer
    {
        Tile[,] tiles_to_render;
        Tile[,] tiles_to_render2;
        Tile[,] tiles_to_render3;
        Tile[,] tiles_to_render4;
        bool debug = false;
        int[] tilecalc = new int[3];
        Color[] testcolors = new Color[2];
        Point screen_tile_half;
        Point screen_tile;
        Lighting lghting;
        GameScreen game;

        public int tilesrendered = 0;
        List<RectangleRenderItem> ItemsToRender = new List<RectangleRenderItem>();
        List<RectangleRenderItem> LastItemsToRender = new List<RectangleRenderItem>();

        Camera camera;

        public enum Direction
        {
                Left,
                Right,
                Up,
                Down
        }

        public TileRenderer(Map refmap, Viewport vp,Game game, Camera cam,GameScreen gs)
        {
            tiles_to_render = refmap.ground;
            tiles_to_render2 = refmap.flora;
            tiles_to_render3 = refmap.objects;
            tiles_to_render4 = refmap.upper_objects;
            testcolors[0] = new Color(255, 0, 0, 125);
            testcolors[1] = new Color(0, 255, 0, 125);
            tilecalc[0] = EngineLimit.TileSize / 2;
            tilecalc[1] = EngineLimit.TileSize / 4;
            tilecalc[2] = EngineLimit.TileSize / 8;

            screen_tile_half = new Point((game.GraphicsDevice.Viewport.Width / 2) / EngineLimit.TileSize, (game.GraphicsDevice.Viewport.Height / 2) / EngineLimit.TileSize);
            screen_tile = new Point(game.GraphicsDevice.Viewport.Width / EngineLimit.TileSize, game.GraphicsDevice.Viewport.Height / EngineLimit.TileSize);
            camera = cam;

            this.game = gs;
        }

        public void Draw(SpriteBatch sprbtch, Player plr)
        {
            DateTime timestart = DateTime.Now;
            tilesrendered = ItemsToRender.Count;

            
            if (ItemsToRender.Count != 0) //Are we still making tiles.
            {
                //Is the buffer different
                if (LastItemsToRender != ItemsToRender)
                {
                    LastItemsToRender.Clear();
                    LastItemsToRender.AddRange(ItemsToRender);
                }
            }

            foreach (var item in LastItemsToRender)
            {
                if (item.SourceRectangle != Rectangle.Empty)
                    sprbtch.Draw(item.Texture, item.DestinationRectangle, item.SourceRectangle, item.Colour, item.Rotation, item.Origin, item.SpriteEffects, item.LayerDepth);
                else
                    sprbtch.Draw(item.Texture, item.DestinationRectangle, item.Texture.Bounds, item.Colour, item.Rotation, item.Origin, item.SpriteEffects, item.LayerDepth);
            }
            System.Diagnostics.Debug.WriteLine("Finished Tile Draw Cycle at " + (DateTime.Now - timestart).TotalMilliseconds + "ms");
            ItemsToRender.Clear();
       }

        public void Update(GameScreen gs, Player plr)
        {
            DateTime timestart = DateTime.Now;
            DateTime timefinish;
            DateTime timetotstart = DateTime.Now;
           // System.Diagnostics.Debug.WriteLine("Beginning Tile Update Cycle at " + timestart.ToShortTimeString());
            lghting = gs.g_lighting;
            timefinish = DateTime.Now;
           // System.Diagnostics.Debug.WriteLine("Finished Tileset 1 Update Cycle at " + timefinish.ToShortTimeString() + "(" + (timefinish - timestart).TotalMilliseconds + "ms)");
            timestart = DateTime.Now;

            List<RectangleRenderItem> ItemsToClear = new List<RectangleRenderItem>();
            foreach (var item in ItemsToRender) //Render Safe-Checks
            {
                if (item.Texture == null || item.DestinationRectangle == Rectangle.Empty || item.Colour == null)
                {
                    ItemsToClear.Add(item);
                }
            }

            foreach (var item in ItemsToClear)
            {
                ItemsToRender.Remove(item);
            }
            ItemsToClear.Clear();

            UpdateTileset(tiles_to_render);

            System.Diagnostics.Debug.WriteLine("Total Cycle Time " + (timefinish - timetotstart).TotalMilliseconds + "ms");
        }

        public void UpdateTileset(Tile[,] tileset)
        {
            Rectangle bounds = camera.GetBounds();
            
            Point TileSize = new Point((int)(EngineLimit.TileScaleX * EngineLimit.TileSize), (int)(EngineLimit.TileScaleY * EngineLimit.TileSize));
            Point relative_cord = Point.Zero;
            Point precise = new Point((int)(camera.speed_buffer.X * ((float)TileSize.X / 20)), (int)(camera.speed_buffer.Y * ((float)TileSize.Y / 20)));
            for (int x = bounds.X - 2; x < bounds.Width + bounds.X + 2; x++)
            {
                relative_cord.X++;
                for (int y = bounds.Y - 2; y < bounds.Height + bounds.Y + 2; y++)
                {
                    relative_cord.Y++;
                    Texture2D tile_tex;
                    if (x < 0 || y < 0 || x >= EngineLimit.TotalMapAreaWidth || y >= EngineLimit.TotalMapAreaHeight)
                    {
                        tile_tex = GameResources.textures.GetResource("BLANK");
                    }
                    else
                    {
                        tile_tex = GameResources.textures.GetResource(tiles_to_render[x, y].Type);
                    }

                    Rectangle pos = new Rectangle((relative_cord.X * TileSize.X) //MarkerX
                                                  - (TileSize.X * 2) //Bit of borderX
                                                  - (precise.X + (TileSize.X / 2)) //Precise Layer
                                                  , (relative_cord.Y * TileSize.Y) - (TileSize.Y / 2) - TileSize.Y - (precise.Y + (TileSize.Y / 2)), TileSize.X, TileSize.Y);
                    RectangleRenderItem item = new RectangleRenderItem(tile_tex, pos, Color.White);
                    ItemsToRender.Add(item);
                }
                relative_cord.Y = 0;
            }
            relative_cord.X = 0;
        }

        public bool Trace(int[,] tileset,int x, int y,int type, Direction direction, int count = 1)
        {
            bool check = true;
            for (int i = 0; i <= count; i++)
            {
                if(direction == Direction.Left)
                {
                    if (x <= 0)
                    {
                        check = false;
                    }
                    else
                    {
                        if (tileset[y, x - i] != type)
                        {
                            check = false;
                        }
                    }
                }

                if(direction == Direction.Right)
                {
                    if (x > 238)
                    {
                        check = false;
                    }
                    else
                    {
                        if (tileset[y, x + i] != type)
                        {
                            check = false;
                        }
                    }
                }

                if(direction == Direction.Up)
                {
                    if (y > 0 && x > 0)
                    {
                        if (tileset[y - i, x] != type)
                        {
                            check = false;
                        }
                    }
                }
                if (direction == Direction.Down)
                {
                    if (y > 238)
                    {
                        check = false;
                    }
                    else
                    {
                        if (tileset[y + i, x] != type)
                        {
                            check = false;
                        }
                    }
                }
            }

            return check;
        }

        }
    }
