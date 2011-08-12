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
        Texture2D background;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="backgroundTexture"> The background texture for the sidebar</param>
        /// <param name="size"> The size of the background in pixels </param>
        /// <param name="clientBounds"> The rectangle representing the game window.</param>
        public Sidebar(Texture2D backgroundTexture, Point size, Rectangle clientBounds)
            : base(backgroundTexture, size, new Point(1,1),0,Point.Zero)
        {
            this.background = backgroundTexture;
            this.MoveTo(new Vector2(clientBounds.Width - size.X, 0));

        }

        /// <summary>
        /// Override for the Sprite draw method. Will eventually draw all sub-components of
        /// the sidebar, but only does the background now.
        /// </summary>
        /// <param name="spriteBatch"> The spritebatch to draw with.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
