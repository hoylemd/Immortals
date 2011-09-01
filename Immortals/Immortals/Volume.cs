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

        /// <summary>
        /// Consructor
        /// </summary>
        /// <param name="World">
        /// The Matrix representing world transforms.</param>
        public Volume(Matrix World)
        {
            // Save Data
            this.World = World;
        }
    }
}
