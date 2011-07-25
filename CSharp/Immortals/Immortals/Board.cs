using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Class to represent a game board
    /// </summary>
    class Board
    {
        // Tile dimensions
        Point boardSize;
        Point tileSize;

        // Sprites
        Sprite tile;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardSize"> The size in tiles of the board</param>
        /// <param name="tile"> The sprite to use for tiles</param>
        public Board(Point boardSize, Sprite tile)
        {
            // Save data
            this.boardSize = boardSize;
            this.tile = tile;

            // Get the tile size from the sprite
            this.tileSize = tile.GetFrameSize();
        }

        /// <summary>
        /// Function to draw a board
        /// </summary>
        /// <param name="spriteBatch"> The Spritebatch to draw with. Must be begun.</param>
        /// <param name="offset"> The offset in pixels from the top-left of the window to draw from.</param>
        /// <param name="ClientBounds"> The boundaries of the window in relation to the top-left of the window.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 offset, Rectangle ClientBounds)
        {
            int i, j;

            // Iterate over the board and draw each tile
            for (i= 0; i < boardSize.X; i++)
            {
                for (j= 0; j < boardSize.Y; j++)
                {
                    tile.Draw(spriteBatch,
                        new Vector2(offset.X + (i * this.tileSize.X),
                            offset.Y + (j * this.tileSize.Y)));
                }
            }
        }

    }
}
