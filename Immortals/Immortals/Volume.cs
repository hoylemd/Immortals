using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immortals
{
    /// <summary>
    /// Abstract class to represent 3d volumes.
    /// </summary>
    abstract class Volume
    {
        public Matrix World { get; protected set; }
        public float Height { get; protected set; }

        /// <summary>
        /// Consructor
        /// </summary>
        /// <param name="World">
        /// The Matrix representing world transforms.</param>
        /// <param name="Height">The height of the volume.</param>
        public Volume(Matrix World, float Height)
        {
            // Save Data
            this.World = World;
            this.Height = Height;
        }
    }
}
