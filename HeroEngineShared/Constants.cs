using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroEngineShared
{
    public static class Constants
    {
        //This is only for internal use
        //To keep record of the version installed
        static string Lidgren_Version = "Lidgren REV 332";
        static string C3XNA_Version = "C3 Xna Primitives REV 12";

        public static string HeroEngine_Version = "HeroEngine DEV Build 001";
        public static string HeroEngineShared_Version = "HeroEngineShared DEV Build 001";
        public static string HeroEngineServer_Version = "HeroEngineServer DEV Build 001";

        public static string HeroEngine_Folder_Data = "/data";
        public static string HeroEngine_Folder_Config = "/config";
        public static string HeroEngine_Folder_World = "/world";

        public static string HeroEngine_Data_Fonts = "/fonts";
        public static string HeroEngine_Data_Music = "/music";
        public static string HeroEngine_Data_Sounds = "/sounds";
        public static string HeroEngine_Data_Texture = "/texture";
        public static string HeroEngine_Data_Tiles = "/tiles";
        public static string HeroEngine_Data_Custom = "/custom";
        public static string HeroEngine_Data_Videos = "/videos";

        public static string HeroEngine_Config_Binding = "/binding";
        public static string HeroEngine_Config_Settings = "/settings";

        // HeroEngineData = hed
        public static string HeroEngine_Data_Extension = ".hed";
        public static string HeroEngine_Config_Extension = ".txt";

        public static uint MersenneTwister_Seed = 3245;
    }
}
