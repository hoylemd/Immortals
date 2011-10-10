using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immortals
{
    /// <summary>
    /// Class to represent a clickable button.
    /// </summary>
    public abstract class Button
    {
        // Presence on screen
        Rectangle positionBox;
        Sprite sprite;

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
        public Button(Sidebar sidebar, Sprite sprite, Rectangle positionBox)
        {
            this.sidebar = sidebar;
            this.sprite = sprite;
            this.positionBox = positionBox;
        }

        /// <summary>
        /// Function to move a button to a specific position within it's 
        /// parent container
        /// </summary>
        /// <param name="position"> The position relative to thw top-left
        /// of the parent container to move the button to.</param>
        public void Move(Point position)
        {
            this.positionBox.X = position.X;
            this.positionBox.Y = position.Y;
        }

        /// <summary>
        /// Class to be overridden for actual button implementations.
        /// </summary>
        public virtual void Clicked();
    }
}
