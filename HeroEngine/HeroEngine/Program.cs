using System;
using System.IO;
using HeroEngineShared;

namespace HeroEngine
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Check for the files otherwise the game wont run
            CheckForRequiredFiles();
            using (MainGame game = new MainGame(args))
            {
                game.Run();
            }
        }

        static void CheckForRequiredFiles()
        {
            CheckDirectorys();
            CheckFiles();
        }

        static void CheckDirectorys()
        {
            //Check for the directorys and if they dont exist then create them
            string[] dirs = new string[3];
            dirs[0] = Constants.HeroEngine_Folder_Config;
            dirs[1] = Constants.HeroEngine_Folder_Data;
            dirs[2] = Constants.HeroEngine_Folder_World;

            foreach (string dir in dirs)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + dir);
                }
            }

        }

        static void CheckFiles()
        {
            //If the files dont exsist let it create default ones
            string[] files = new string[6];
            files[0] = Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Binding + Constants.HeroEngine_Config_Extension;
            files[1] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Fonts + Constants.HeroEngine_Data_Extension;
            files[2] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Music + Constants.HeroEngine_Data_Extension;
            files[3] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Sounds + Constants.HeroEngine_Data_Extension;
            files[4] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Texture + Constants.HeroEngine_Data_Extension;
            files[5] = Constants.HeroEngine_Folder_Data + Constants.HeroEngine_Data_Tiles + Constants.HeroEngine_Data_Extension;

            foreach (string file in files)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + file))
                {
                    FileInfo ifo = new FileInfo(file);
                    string filename = ifo.Name.Replace(ifo.Extension, "");
                    string data = (string)Resources.ResourceManager.GetObject(filename);
                    File.WriteAllText(Directory.GetCurrentDirectory() + file, data);
                }
            }
        }
    }
#endif
}

