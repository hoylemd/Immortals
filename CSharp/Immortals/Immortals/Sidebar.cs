using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    ///  Class for sidebar drawing
    /// </summary>
    class Sidebar: Sprite
    {
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="backgroundTexture"> The background texture for the sidebar</param>
        /// <param name="size"> The size of the background in pixels </param>
        /// <param name="clientBounds"> The rectangle representing the game window.</param>
        public Sidebar(Texture2D backgroundTexture, Point size, Rectangle clientBounds)
            : base(backgroundTexture, size, new Point(1,1),0,Point.Zero)
        {
            this.MoveTo(new Vector2(clientBounds.Width - size.X, 0));
        }

        /// <summary>
        /// Override for the sprite Update method. does nothing.
        /// </summary>
        /// <param name="gameTime"> Artifact from base class.</param>
        public override void Update(GameTime gameTime)
        {
            // Do nothing.
        }
    }
}
