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
    class Cylinder
    {
        public float Height { get; protected set; }
        public float Radius { get; protected set; }
        public Matrix World { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Height">The height of the cylinder</param>
        /// <param name="Radius">The Radius of the cylinder</param>
        /// <param name="World">
        /// The transformation matrix of the cylinder</param>
        public Cylinder(float Height, float Radius, Matrix World)
        {
            // Save data
            this.Height = Height;
            this.Radius = Radius;
            this.World = World;
        }
        
    }
}
