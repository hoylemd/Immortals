using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immortals
{
    // Class to represent a rectangular Prism
    class Box : Volume
    {
        public float Width { get; protected set; }

        public float Depth { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="World">
        /// The matrix representing world transforms</param>
        /// <param name="Height">The height of the box (Y-axis)</param>
        /// <param name="Width">The width of the box (X-axis)</param>
        /// <param name="Depth">The depth of the box (Z-axis)</param>
        public Box(Matrix World, float Height, float Width, float Depth)
            :base(World, Height)
        {
            // Save data
            this.Width = Width;
            this.Depth = Depth;
        }
    }
}
