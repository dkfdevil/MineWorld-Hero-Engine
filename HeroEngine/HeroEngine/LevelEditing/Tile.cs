using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeroEngine.Render;
using HeroEngine.CoreGame;
using HeroEngineShared;

namespace HeroEngine.LevelEditing
{
    public class Tile
    {
        public int Id { get; set; }
        public bool CanWalkOn { get; set; }
        public string Type { get; set; }
        public bool IsAnimated { get; set; }
        public AnimatedTexture tex = new AnimatedTexture(null,Rectangle.Empty,0,100);
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
                tile_data = File.ReadAllLines(Directory.GetCurrentDirectory() + Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Tiles + Constants.HeroEngine_Data_Extension); //Get the data.
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
            int width = 0;
            int height = 0;

            int frames = 0;
            int speed = 0;
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
                    //--Can't do a case, too complicated.--
                    //We can, do a 'every-thing-before-=' check
                    string[] attribute;
                    try
                    {
                        attribute = line.Split('=');
                    }
                    catch (Exception)
                    {
                        
                        throw new Exception("TileDB File contains an invalid attribute. I couldn't tell where to split :(");
                    }
                    attribute[0] = attribute[0].ToLower();

                    switch (attribute[0])
                    {
                        case "id":
                            tile.Id = int.Parse(attribute[1]);
                            continue;

                        case "type":
                            tile.Type = attribute[1];
                            continue;

                        case "canwalkon":
                            tile.CanWalkOn = bool.Parse(attribute[1]);
                            continue;

                        case "size":
                            int xpos = attribute[1].IndexOf('x');
                            width = int.Parse(attribute[1].Substring(0, xpos));
                            height = int.Parse(attribute[1].Substring(xpos + 1, attribute[1].Length - xpos - 1));
                            continue;

                        case "frames":
                            frames = int.Parse(attribute[1]);
                            continue;

                        case "speed":
                            speed = int.Parse(attribute[1]);
                            continue;

                        default:
                            break;
                    }

                }
            }
            tile.tex = new AnimatedTexture(GameResources.textures.GetResource(tile.Type), new Rectangle(0, 0, width, height), frames, speed);
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
