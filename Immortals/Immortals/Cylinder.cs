using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immortals
{
    /// <summary>
    /// Class to represent a cylinder shape.
    /// </summary>
    class Cylinder :Volume
    {
        public float Radius { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="World">
        /// The transformation matrix of the cylinder</param>
        /// <param name="Height">The height of the cylinder</param>
        /// <param name="Radius">The radius of the cylinder</param>
        public Cylinder(Matrix World, float Height, float Radius)
            :base(World, Height)
        {
            // Save data
            this.Radius = Radius;
        }
        
    }
}
