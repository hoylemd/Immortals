using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Class to represent a clickable button.
    /// </summary>
    public abstract class Button: Sprite
    {
        // Parent Objects
        Sidebar sidebar;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite"> 
        /// The sprite to associate with the button.</param>
        /// <param name="positionBox"> 
        /// The position the box occupies within it's parent 
        /// container.</param>
        public Button(Texture2D texture, Point frameSize, Point sheetSize,
            Sidebar sidebar)
            : base(texture, frameSize, sheetSize, 0, Point.Zero)
        {
            this.sidebar = sidebar;
        }
    }
}
