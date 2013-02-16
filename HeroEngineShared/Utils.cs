using System;

namespace HeroEngineShared
{
    public static class Utils
    {
        public static Random RandGen = new Random();
        public static MersenneTwister RandGenMT = new MersenneTwister(Constants.MersenneTwister_Seed);
    }
}
