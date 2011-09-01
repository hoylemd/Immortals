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
        public float Height { get; protected set; }
        public float Depth { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Width">The width of the box (X-axis)</param>
        /// <param name="Height">The height of the box (Y-axis)</param>
        /// <param name="Depth">The depth of the box (Z-axis)</param>
        /// <param name="World">
        /// The matrix representing world transforms</param>
        public Box(float Width, float Height, float Depth, Matrix World)
            :base(World)
        {
            // Save data
            this.Width = Width;
            this.Height = Height;
            this.Depth = Depth;
        }
    }
}
