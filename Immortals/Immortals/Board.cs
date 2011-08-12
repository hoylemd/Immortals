using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Class to represent a game board</summary>
    class Board
    {
        // board dimensions
        Point boardSize;

        /// <summary>
        /// Constructor</summary>
        /// <param name="boardSize"> 
        /// The size in tiles of the board</param>
        /// <param name="tile"> 
        /// The sprite to use for tiles</param>
        public Board(Point boardSize)
        {
            // Save data
            this.boardSize = boardSize;
        }
    }
}
