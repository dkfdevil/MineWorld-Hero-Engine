using System;
using System.IO;
using HeroEngineShared;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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
            //Process the parameters here
            ProcessParameters(args);
            using (MainGame game = new MainGame())
            {
                if (Debugger.IsAttached)
                {
                    game.Run();
                }
                else
                {
                    try
                    {
                        game.Run();
                    }
                    catch (Exception e)
                    {
                        if (!Directory.Exists("Crashlogs"))
                        {
                            Directory.CreateDirectory("Crashlogs");
                        }
                        File.WriteAllText("Crashlogs/" + DateTime.Now.ToString("hh-mm-ss-dd-mm-yyyy") + ".log", e.Message + "\r\n\r\n" + e.StackTrace);
                        MessageBox.Show("The game has crashed. The crash info has been written to the crashlog.",
                                        "Crash and Burn", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }

        static void ProcessParameters(string[] parameters)
        {
            foreach (string parameter in parameters)
            {
                switch (parameter.ToLower())
                {
                    case "-debug":
                        {
                            Parameters.Debug = true;
                            break;
                        }
                }
            }
        }
    }
#endif
}

