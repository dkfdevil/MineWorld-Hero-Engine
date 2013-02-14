using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HeroEngine.LevelEditing
{
    public class Map
    {
        public DateTime world_time = new DateTime(2012, 1, 1,5,0,0);
        public string WorldName;
        public Tile[,] ground = new Tile[EngineLimit.TotalMapAreaHeight, EngineLimit.TotalMapAreaHeight];
        public Tile[,] flora = new Tile[EngineLimit.TotalMapAreaHeight, EngineLimit.TotalMapAreaHeight];
        public Tile[,] objects = new Tile[EngineLimit.TotalMapAreaHeight, EngineLimit.TotalMapAreaHeight];
        public Tile[,] upper_objects = new Tile[EngineLimit.TotalMapAreaHeight, EngineLimit.TotalMapAreaHeight];

        public string[] tile_types = File.ReadAllLines(Directory.GetCurrentDirectory() + EngineLimit.TileDBFileName); 
        public TileMarker local_player_position;
        DateTime lastupdate = new DateTime(2000,1,01);
        public Map(string Name, DateTime time, Tile[,] Ground, Tile[,] Flora, Tile[,] Objects, Tile[,] Upper_objects, TileMarker lpp)
        {
            world_time = time;
            WorldName = Name;
            ground = Ground;
            flora = Flora;
            objects = Objects;
            upper_objects = Upper_objects;
            local_player_position = lpp;
        }

        public void WriteToFile()
        {
            string[] lines_g = new string[EngineLimit.TotalMapAreaHeight];
            string[] lines_f = new string[EngineLimit.TotalMapAreaHeight];
            string[] lines_o = new string[EngineLimit.TotalMapAreaHeight];
            string[] lines_u = new string[EngineLimit.TotalMapAreaHeight];

            for (int i = 0; i < EngineLimit.TotalMapAreaHeight; i++)
            {
                        for (int e = 0; e < EngineLimit.TotalMapAreaHeight; e++)
                        {
                            lines_g[i] += ground[i, e] + "=";
                            lines_f[i] += flora[i,e] + "=";
                            lines_o[i] += ground[i, e] + "=";
                            lines_u[i] += flora[i,e] + "=";
                        }
            }

            string text_doc = "";
            foreach (string item in lines_g)
            {
                text_doc += item + Environment.NewLine;
            }
                        foreach (string item in lines_f)
            {
                text_doc += item + Environment.NewLine;
            }
                        foreach (string item in lines_o)
            {
                text_doc += item + Environment.NewLine;
            }
                        foreach (string item in lines_u)
            {
                text_doc += item + Environment.NewLine;
            }

            if (!Directory.Exists(@"\\WorldFiles\"))
                Directory.CreateDirectory(@"WorldFiles\");

          File.WriteAllText(Environment.CurrentDirectory + @"\\WorldFiles\" + WorldName + ".wld",text_doc);
        }

        public void GetFromFile()
        {
            string[] raw_lines;
            string[] lines = new string[960];
            raw_lines = File.ReadAllLines(Environment.CurrentDirectory + @"\\WorldFiles\" + WorldName + ".wld");
            for (int i = 0; i < 480; i++)
            {
                lines[i] = raw_lines[i].Replace("=", "");
            }

            for (int y = 0; y < EngineLimit.TotalMapAreaHeight; y++)
			{
			    for (int x = 0; x < EngineLimit.TotalMapAreaHeight; x++)
			    {
			       ground[y, x].Type = tile_types[int.Parse(lines[y][x].ToString())];
			    }

			}

            for (int y = EngineLimit.TotalMapAreaHeight; y < 480; y++)
			{
			    for (int x = EngineLimit.TotalMapAreaHeight; x < 480; x++)
			    {
                    flora[y - EngineLimit.TotalMapAreaHeight, x - EngineLimit.TotalMapAreaHeight].Type = tile_types[int.Parse(lines[y][x - EngineLimit.TotalMapAreaHeight].ToString())];
			    }

			}

            if (lines[480] != null)
            {
            for (int y = 480; y < 720; y++)
			{
			    for (int x = 480; x < 720; x++)
			    {
                        objects[y - 480, x - 480].Type = tile_types[int.Parse(lines[y][x - 480].ToString())];
			    }

			}
        }
            if (lines[720] != null)
            {
                for (int y = 720; y < 960; y++)
                {
                    for (int x = 720; x < 960; x++)
                    {
                        upper_objects[y - 720, x - 720].Type = tile_types[int.Parse(lines[y][x - 720].ToString())];
                    }

                }
            }


        }

        public void AddToLayer(int z, int x, int y, string tile)
        {
            switch (z)
            {
                case 1:
                    ground[y, x].Type = tile;
                    break;

                case 2:
                    flora[y, x].Type = tile;
                    break;

                case 3:
                    objects[y, x].Type = tile;
                    break;

                case 4:
                    upper_objects[y, x].Type = tile;
                    break;
            }
        }

        public void PassTimeBy()
        {
            int now = ((int)DateTime.Now.TimeOfDay.TotalSeconds * 1000) + (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            int last = ((int)lastupdate.TimeOfDay.TotalSeconds * 1000) + (int)lastupdate.TimeOfDay.TotalMilliseconds;

            if (now - last > 600)
            {
                world_time = world_time.Add(new TimeSpan(0,1,0));
                lastupdate = DateTime.Now;
            }
        }
    }
}
