using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Class to represent a game Board.
    /// </summary>
    public class Board
    {
        // Game object information
        public Point size { get; private set; }


        // Display information
        public Sprite sprite { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engine">The game engine.</param>
        /// <param name="modelManager">The Model manager</param>
        /// <param name="size">
        /// A Point object describing the board size in DU.</param>
        /// <param name="texture">
        /// A Texture2D object holding the image to paint the board with.
        /// </param>
        public Board( Point size, Sprite sprite)
        {
            // Register pointers and objects
            this.sprite = sprite;
            this.size = size;
        }
    }
}
