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
    /// Class to represent container-type sprites/sprite trees
    /// </summary>
    public class Container: Sprite
    {
        List<Sprite> spriteList;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="texture"> 
        /// The texture to associate with this sprite. </param>
        /// <param name="frameSize"> 
        /// The size of each frame in pixels. </param>
        /// <param name="sheetSize">
        /// The dimensions of the sheet in frames. </param>
        /// <param name="frameDuration">
        /// The time in milliseconds to display each frame. </param>
        /// <param name="position">
        /// The position relative to the parent's frame of reference to draw 
        /// this container in.</param>
        public Container(
            Texture2D texture, Point frameSize, Point sheetSize,
            int frameDuration, Point position)
            : base(texture, frameSize, sheetSize, frameDuration, Point.Zero)
        {
            // initialize members
            spriteList = new List<Sprite>();

            // Move to position
            this.MoveTo(position);
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
        /// <param name="spriteBatch"> 
        /// The spriteBatch that will draw the sprite.</param>
        /// <param name="view">
        /// The Rectangle to use as a frame of reference.</param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle view)
        {
            // calculate the location to draw with
            Rectangle drawLoc = new Rectangle(
                view.X + (int)position.X, view.Y + (int)position.Y, frameSize.X, 
                frameSize.Y);

            // Draw everything if the container isn't hidden.
            if (!hidden)
            {
                try
                {
                    //Draw the sprite.
                    if (texture != null)
                        spriteBatch.Draw(
                            texture, drawLoc, NextFrame(), Color.White, 0, 
                            Vector2.Zero, SpriteEffects.None, 1);
                }
                // Handle unbegun spriteBatches
                catch (InvalidOperationException e)
                {
                    Console.Out.WriteLine(
                        "Sprite.Draw call outside of SpriteBatch.Begin() and\n" +
                        "End() calls. Error type: " + e.GetType().ToString());
                }
           
                // Draw each child sprite/container
                foreach (Sprite s in spriteList)
                {
                    s.Draw(spriteBatch, drawLoc);
                }

                // draw parent?
                base.Draw(spriteBatch);
            }
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
