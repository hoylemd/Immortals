using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Immortals
{
    /// <summary>
    /// Class for sidebars. Currently only implements the Container 
    /// class.</summary>
    public class Sidebar: Container
    {
        /// <summary>
        /// Constructor</summary>
        /// <param name="texture">
        /// The background texture for the sidebar</param>
        /// <param name="frameSize">
        /// The size in pixels of each frame of the animation.
        /// </param>
        /// <param name="sheetSize">
        /// The dimenions in frames of the framesheet.</param>
        /// <param name="frameDuration">
        /// The time in milliseconds to display each frame.</param>
        /// <param name="position">
        /// The position relative to the parent's frame of reference in which 
        /// to draw the sidebar.</param>
        public Sidebar(
            Texture2D texture, Point frameSize, Point sheetSize,
            int frameDuration, Point position)
            : base(texture, frameSize, sheetSize,0, position)
        {
        }
    }
}
