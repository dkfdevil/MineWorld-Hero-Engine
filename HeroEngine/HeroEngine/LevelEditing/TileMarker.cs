using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroEngine.LevelEditing
{
    /// <summary>
    /// This is a marker which allows operations to be made on objects that are placed in the game. E.g. the player.
    /// </summary>
    public class TileMarker
    {
        public TileMarker(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// The X position of the tile.
        /// </summary>
        public int x;
        /// <summary>
        /// The Y position of the tile.
        /// </summary>
        public int y;
        /// <summary>
        /// The layer of the tile. Layer 1 is drawn on top of layer 0 and so on.
        /// </summary>
        public int z = 0;
    }
}
