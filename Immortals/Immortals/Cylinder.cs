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
        public float Height { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Radius">The radius of the cylinder</param>
        /// <param name="Height">The height of the cylinder</param>
        /// <param name="World">
        /// The transformation matrix of the cylinder</param>
        public Cylinder(float Radius, float Height, Matrix World)
            :base(World)
        {
            // Save data
            this.Height = Height;
            this.Radius = Radius;
        }
        
    }
}
