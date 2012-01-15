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
    /// Class for sidebars.
    /// class.</summary>
    public class Sidebar: SpriteContainer
    {
        /// <summary>
        /// Constructor</summary>
        /// <param name="area">
        /// The area this container occupies in the parent's frame of reference</param>
        public Sidebar(Rectangle area)
            : base(area)
        {
        }
    }
}
