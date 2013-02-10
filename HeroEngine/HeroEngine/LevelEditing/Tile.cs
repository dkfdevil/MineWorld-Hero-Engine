using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace HeroEngine.LevelEditing
{
    public class Tile
    {
        public int Id { get; set; }
        public bool CanWalkOn { get; set; }
        public string Type { get; set; }
    }

    public class TileDB
    {
        static List<Tile> Tiles;
        static Exception no_data = new Exception("Couldn't find tile data. Mission Abort");
        static Exception tile_invalid = new Exception("Tile data is wrong. Mission Abort");
        public static void LoadTileData()
        {
            Tiles = new List<Tile>(); //Creates a new tile db.
            string[] tile_data; //The file

            try 
	        {
                tile_data = File.ReadAllLines(Directory.GetCurrentDirectory() + EngineLimit.TileDBFileName); //Get the data.
	        }
	        catch (FileNotFoundException)
	        {
		    throw no_data; //We coudln't find the data, so its failed somehow.
	        }
           

            //Disect strings
            bool IsTile = false;
            List<String> Selection = new List<string>();
            foreach (string line in tile_data)
            {
                if (IsTile)
                {
                    if (line == "}")
                    {
                        //End of tile, send it off.
                        Tiles.Add(GetTileData(Selection.ToArray())); //Add the tile to the db.
                        Selection.Clear(); //Clear the selection for next tile.
                        IsTile = false; //Begin hunting.
                    }
                    else
                    {
                        Selection.Add(line);//Add this, should be a attribute.
                    }
                }

                //Hunting for a new tile.
                if (line == "{")
                {
                    IsTile = true;
                    continue;
                }
            }


        }

        private static Tile GetTileData(string[] tiledata)
        {
            Tile tile = new Tile();
            foreach (string line in tiledata)
            {
                if (line == "" || line == " " || line == "//")
                {
                    //Line is a comment, space or blank. Ignore
                    //We have the { for comort. Its not needed.
                    continue;
                }
                else
                {
                    //Can't do a case, too complicated.

                    //ID Check
                    if (line.ToLower().Substring(0, 2) == "id")
                    {
                        tile.Id = int.Parse(line.Substring(3, line.Length - 3));
                        continue;
                    }


                    //Type Check
                    if (line.ToLower().Substring(0, 4) == "type")
                    {
                        tile.Type = line.Substring(5, line.Length - 5);
                        continue;
                    }

                    //CanWalkOn Check

                    if (line.ToLower().Substring(0, 9) == "canwalkon")
                    {
                        string disection = line.Substring(10, line.Length - 10);
                        tile.CanWalkOn = bool.Parse(disection);
                        continue;
                    }
                }
            }
            return tile;
        }

        public static Tile GetTile(int ID)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.Id == ID)
                {
                    return tile;
                }
            }

            return null;
        }

        public static Tile GetTile(string Type)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.Type == Type)
                {
                    return tile;
                }
            }

            return null;
        }
    }
}
