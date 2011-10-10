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
    /// Class for sidebar drawing</summary>
    public class Sidebar: Sprite
    {
        // Background texture
        Texture2D backgroundTexture;

        // Absolute rectangle
        Rectangle rectangle;

        /// <summary>
        /// Constructor</summary>
        /// <param name="backgroundTexture">
        /// The background texture for the sidebar</param>
        /// <param name="size"> 
        /// The size of the background in pixels </param>
        /// <param name="clientBounds">
        /// The rectangle representing the game window.</param>
        public Sidebar(
            Texture2D backgroundTexture, Point size, Rectangle rectangle)
            : base(backgroundTexture, size, new Point(1,1),0,Point.Zero)
        {
            // store textures
            this.backgroundTexture = backgroundTexture;

            // reposition
            this.rectangle = rectangle;
            this.MoveTo(new Vector2(rectangle.X, rectangle.Y));
        }

        /// <summary>
        /// Override for the Sprite draw method. Will eventually draw all 
        /// sub-components of the sidebar, but only does the background
        /// now.</summary>
        /// <param name="spriteBatch"> 
        /// The spritebatch to draw with.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Call the basic draw-to-rectangle of a sprite
            base.Draw(spriteBatch,rectangle);
        }

        /// <summary>
        /// Container behavior for clicking.  Decide on sub-member clicked and
        /// message.
        /// </summary>
        public override void Clicked()
        {
            MouseState mouseState = Mouse.GetState();

        }
    }
}
