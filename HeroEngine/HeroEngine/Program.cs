using System;
using System.IO;
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
            CheckForConfigs();
            using (MainGame game = new MainGame())
            {
                game.Run();
            }
        }

        static void CheckForConfigs()
        {
            string[] dirs = new string[2];
            dirs[0] = "cfg";
            dirs[1] = "reslist";

            string[] files = new string[6];
            files[0] = "cfg/binding.cfg";
            files[1] = "reslist/font.res";
            files[2] = "reslist/music.res";
            files[3] = "reslist/sound.res";
            files[4] = "reslist/texture.res";
            files[5] = "reslist/TileLookup.db";

            foreach (var dir in dirs)
            {
                CheckDir(dir);
            }

            foreach (var file in files)
            {
                CheckFile(file);
            }

        }

        static void CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        static void CheckFile(string file)
        {
            if (!File.Exists(file))
            {
                FileInfo ifo = new FileInfo(file);
                string filename = ifo.Name.Replace(ifo.Extension,"");
                string data = (string)Default.ResourceManager.GetObject(filename);
                File.WriteAllText(file, data);
            }
        }
    }
#endif
}

