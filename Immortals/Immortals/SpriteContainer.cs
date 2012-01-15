using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Immortals
{
    /// <summary>
    /// Class to represent container-type sprites/sprite trees
    /// </summary>
    public class SpriteContainer : Container
    {
        // sprite members
        Sprite background;
        List<Sprite> spriteList;

        // visilibilty members
        Boolean hidden;



        /// <summary>
        /// Constructor</summary>
        /// <param name="area">The area of the parent container this sprite occupies</param>
        public SpriteContainer(Rectangle area)
            : base(area)
        {
            // initialize members
            spriteList = new List<Sprite>();
            hidden = false;
            background = null;

        }

        /// <summary>
        /// Function to set up a sprite for the background
        /// </summary>
        /// <param name="texture">The texture to display.</param>
        /// <param name="frameSize">The size of each frame</param>
        /// <param name="sheetSize">The dimensions in frames of the sprite sheet</param>
        /// <param name="frameDuration">The time to display each frame</param>
        public void setBackground(Texture2D texture, Point frameSize, Point sheetSize,
            int frameDuration)
        {
            // set up background sprite
            background = new Sprite(texture, frameSize, sheetSize, frameDuration, Point.Zero);
        }

        /// <summary>
        /// Function to add a sprite or container to this container.
        /// </summary>
        /// <param name="sprite">the Sprite object to add.</param>
        public void addSprite(Sprite sprite)
        {
            spriteList.Add(sprite);
        }

        /// <summary>
        /// Function to draw a container and all of its children
        /// </summary>
        /// <param name="view">
        /// The Rectangle to use as a frame of reference.</param>
        /// <param name="spriteBatch"> 
        /// The spriteBatch that will draw the sprite.</param>
        public void Draw(Rectangle drawArea, SpriteBatch spriteBatch )
        {
            // calculate the location to draw with
            Rectangle normalizedRectangle = new Rectangle(drawArea.X + area.X, drawArea.Y + area.Y,
                                                          area.Width, area.Height);

            // Draw everything if the container isn't hidden.
            if (!hidden)
            {
                try
                {
                    //Draw the background.
                    if (background != null)
                    {
                        background.Draw(spriteBatch, normalizedRectangle);
                    }
                }
                // Handle unbegun spriteBatches
                catch (InvalidOperationException e)
                {
                    System.Console.WriteLine(
                        "error trying to draw SpriteContainer background.\n" +
                        "SpriteBatch.begin() not called.\n" +
                        "error message:\n" +
                        e.Message);
                }

                // Draw each child sprite/container
                foreach (Sprite s in spriteList)
                {
                    s.Draw(spriteBatch, normalizedRectangle);
                }
            }
        }


    }
}
