using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HeroEngine._3rdParty;
namespace HeroEngine.LevelEditing
{
    class LevelGenerator
    {
        static float percentdone = 0;
        static MersenneTwister random = new MersenneTwister(uint.Parse(DateTime.Now.Second.ToString()));
        public static Map CreateLevel(int size = EngineLimit.TotalMapAreaHeight, string name = "NoName")
        {
            Tile[,] tiles = new Tile[EngineLimit.TotalMapAreaWidth, EngineLimit.TotalMapAreaHeight];
            Tile[,] flora = tiles;
            Tile[,] objects = tiles;
            Tile[,] upper_objects = tiles;

            tiles = CreateSea(tiles, EngineLimit.MaxFetch);
            tiles = CreateSand(tiles, EngineLimit.MinFetch, EngineLimit.MaxFetch, EngineLimit.MinBeachCoast, EngineLimit.MaxBeachCoast);
            tiles = CreateSandGrass(tiles, EngineLimit.MaxFetch);
            flora = CreateRockFormsSea(flora, 15, 15);
            //flora = CreateTreeTop(tiles, 15);
            TileMarker player = new TileMarker(25,25);
            return new Map(name,new DateTime(2012,12,12,7,0,0) ,tiles ,flora,objects,upper_objects,player);
        }

        private static Tile[,] CreateSea(Tile[,] tiles, int maxfetch)
        {
            Tile[,] tile_layout = tiles;

            for (int y = 0; y < EngineLimit.TotalMapAreaHeight; y++) //Up to Down
            {
                percentdone += (EngineLimit.TotalMapAreaHeight / 100) / 20;
                for (int x = 0; x < EngineLimit.TotalMapAreaWidth; x++) //Left To Right
                {

                    if (x <= maxfetch || y <= maxfetch || EngineLimit.TotalMapAreaHeight - maxfetch <= x || EngineLimit.TotalMapAreaHeight - maxfetch <= y) //It is the Sea Border, so make the tiles all 1
                    {
                        tile_layout[y, x] = TileDB.GetTile(1);
                    }
                }
            }
            
            return tile_layout;
            
        }

        private static Tile[,] CreateSand(Tile[,] tiles, int minfetch, int maxfetch, int minlen, int maxlen) 
        {
          int indentALength = 0;
          int indentAWidth = random.Next(minlen, maxlen);
          int lastindentA = 0;

          Tile[,] tile_layout = tiles;
          # region LeftDown
          lastindentA = indentAWidth;
             for (int y = minfetch; y < EngineLimit.TotalMapAreaHeight - minfetch; y++) //Up to Down
             {
                   
                    if (indentALength == 0 ) //Do i need to randomise another coastline
                    {
                        indentALength = random.Next(minlen, maxlen); //Random the coastlines length between the minimum len and the max len.
                        indentAWidth = random.Next(-2, 2) + lastindentA; //Do the same for the width but even it out with a minor random ajustment to the last one.
                        if (indentAWidth < minfetch) //If it sinks too low
                        {
                            indentAWidth = random.Next(0, 2) + lastindentA;
                        }

                        if (indentAWidth > maxfetch) //If it grows too high
                        {
                            indentAWidth = random.Next(-2, 0) + lastindentA;
                        }
                        lastindentA = indentAWidth;
                    }

                    for (int x = maxfetch; x > indentAWidth; x--) //Left To Right
                    {
                        tile_layout[y, x] = TileDB.GetTile(2); //Put sand down
                    }
                    indentALength--; //Take one off the length
                    //THESE  COMMENTS STAND FOR THE NEXT 3 REGIONS
                }
          # endregion
          # region DownAcross
          

              for (int x = maxfetch; x < EngineLimit.TotalMapAreaHeight - minfetch; x++) //Left To Right
              {
                  if (indentALength == 0 )
                    {
                        indentALength = random.Next(minfetch , maxfetch);
                        indentAWidth = random.Next(-2, 2) + lastindentA;
                        if (indentAWidth < minfetch)
                        {
                            indentAWidth = random.Next(0, 2) + lastindentA;
                        }
                        if (indentAWidth > maxfetch) //If it grows too high
                        {
                            indentAWidth = random.Next(-2, 0) + lastindentA;
                        }
                        lastindentA = indentAWidth;
                    }
                  for (int y = EngineLimit.TotalMapAreaHeight - maxfetch ; y < EngineLimit.TotalMapAreaHeight - indentAWidth; y++) //Up to Down
                  {
                      tile_layout[y, x] = TileDB.GetTile(2);
                  }
                indentALength--;
              }
          # endregion
          # region RightUp
              for (int y = EngineLimit.TotalMapAreaHeight - minfetch; y > maxfetch; y--) //Left To Right
              {
                  if (indentALength == 0)
                  {
                      indentALength = random.Next(minfetch, maxfetch);
                      indentAWidth = random.Next(-2, 2) + lastindentA;
                      if (indentAWidth < minfetch)
                      {
                          indentAWidth = random.Next(0, 2) + lastindentA;
                      }
                      if (indentAWidth > maxfetch) //If it grows too high
                      {
                          indentAWidth = random.Next(-2, 0) + lastindentA;
                      }
                      lastindentA = indentAWidth;
                  }
                  for (int x = EngineLimit.TotalMapAreaHeight - maxfetch; x < EngineLimit.TotalMapAreaHeight - indentAWidth; x++) //Up to Down
                  {
                      tile_layout[y, x] = TileDB.GetTile(2);
                  }
                  indentALength--;
              }
          # endregion
          # region UpAcross
              for (int x = EngineLimit.TotalMapAreaHeight - minfetch; x > maxfetch ; x--) //Left To Right
              {
                  if (indentALength == 0)
                  {
                      indentALength = random.Next(minfetch, maxfetch);
                      indentAWidth = random.Next(-2, 2) + lastindentA;
                      if (indentAWidth < minfetch)
                      {
                          indentAWidth = random.Next(0, 2) + lastindentA;
                      }
                      if (indentAWidth > maxfetch) //If it grows too high
                      {
                          indentAWidth = random.Next(-2, 0) + lastindentA;
                      }
                      lastindentA = indentAWidth;
                  }
                  for (int y = maxfetch; y > indentAWidth; y--) //Up to Down
                  {
                      tile_layout[y, x] = TileDB.GetTile(2);
                  }
                  indentALength--;
              }
          # endregion
              percentdone += (EngineLimit.TotalMapAreaHeight / 100) / 20;
            return tile_layout;
            
        }

        private static Tile[,] CreateSandGrass(Tile[,] tiles, int maxfetch)
        {
            Tile[,] tile_layout = tiles;

            for (int y = maxfetch + 1; y < EngineLimit.TotalMapAreaHeight - maxfetch + 1; y++) //Up to Down
            {
                percentdone += (EngineLimit.TotalMapAreaHeight / 100) / 20;
                for (int x = maxfetch + 1; x < EngineLimit.TotalMapAreaHeight - maxfetch + 1; x++) //Left To Right
                {
                    tile_layout[y, x] = TileDB.GetTile(3);
                    if (y < (maxfetch + maxfetch) || x < (maxfetch + maxfetch) || y > EngineLimit.TotalMapAreaHeight - (maxfetch + maxfetch) || x > EngineLimit.TotalMapAreaHeight - (maxfetch + maxfetch))
                    {
                        int rand = random.Next(1, (y / 8) + (x / 8));
                        if (rand == 1)
                        {
                            tile_layout[y, x] = TileDB.GetTile(2);
                        }
                    }
                }
            }
            return tiles;
        }

        private static Tile[,] CreateRockFormsSea(Tile[,] tiles, int maxfetch, int rocknum)
        {
            Tile[,] tile_layout = tiles;
            int randomy = 0;
            int randomx = 0;
            int selection = 0;

            for (int i = 0; i < rocknum; i++)
            {
                percentdone += (EngineLimit.TotalMapAreaHeight / 100) / 20;
                selection = random.Next(-1, 2); 

                if (selection == 2)
                {
                    randomx = random.Next(EngineLimit.TotalMapAreaHeight - maxfetch, EngineLimit.TotalMapAreaHeight);
                }
                else
                {
                    randomx = random.Next(0, maxfetch);
                }

                selection = random.Next(0, 1);

                if (selection == 2)
                {
                    randomy = random.Next(EngineLimit.TotalMapAreaHeight - maxfetch, EngineLimit.TotalMapAreaHeight);
                }
                else
                {
                    randomy = random.Next(0, maxfetch);
                }

                tile_layout[randomx, randomy] = TileDB.GetTile(4);
            }
            return tile_layout;
        }

        private static Tile[,] CreateTreeTop(Tile[,] tiles, int maxfetch)
        {
            Tile[,] tile_layout = tiles;
            Tile[,] flora = new Tile[EngineLimit.TotalMapAreaHeight,EngineLimit.TotalMapAreaHeight];
            for (int y = maxfetch; y < EngineLimit.TotalMapAreaHeight - maxfetch; y++)
            {
                percentdone += (EngineLimit.TotalMapAreaHeight / 100) / 20;
                for (int x = maxfetch; x < EngineLimit.TotalMapAreaHeight - maxfetch; x++)
                {
                    if (tile_layout[y, x] == TileDB.GetTile(4))
                    {
                        int dice = random.Next(0, 20);
                        if (dice == 15)
                        {
                            flora[y, x].Type = "TREZ"; //TODO: Change Me
                        }
                        else if( dice == 5)
                        {
                            flora[y, x].Type = "TREZ"; //TODO: Change Me
                        }
                    }
                }
            }
            return flora;
        }

        public static float LevelGenProgress()
        {
            return percentdone;
        }
     }

}
