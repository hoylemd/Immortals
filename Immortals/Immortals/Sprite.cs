using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /*abstract*/ public class Sprite
    {
        // Animation variables
        Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame;
        int frameDuration;
        Boolean animated;

        // drawing variables
        Texture2D texture;
        Vector2 position;

        // collision variables
        Point boundingOffset;
        Rectangle boundingBox;

        /// <summary>
        /// Constructor</summary>
        /// <param name="texture"> 
        /// The texture to associate with this sprite. </param>
        /// <param name="frameSize"> 
        /// The size of each frame in pixels. </param>
        /// <param name="sheetSize">
        /// The dimensions of the sheet in frames. </param>
        /// <param name="frameDuration"> 
        /// The number of milliseconds to display each frame</param>
        /// <param name="boundingOffset"> 
        /// The number of pixels the bounding box is offset from the frame 
        /// edges</param>
        public Sprite(
            Texture2D texture, Point frameSize, Point sheetSize, 
            int frameDuration, Point boundingOffset)
        {
            // Save data
            this.texture = texture;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            if (sheetSize == new Point(1, 1) || this.frameDuration == 0)
                this.animated = false;
            else
                this.animated = true;
            this.frameDuration = frameDuration;
            this.boundingOffset = boundingOffset;

            // Initialize variables
            this.timeSinceLastFrame = 0;
            this.currentFrame = new Point(0, 0);

            // Calculate the bounding box
            this.boundingBox = new Rectangle( 
                boundingOffset.X, boundingOffset.Y, 
                frameSize.X - (2 * boundingOffset.X), 
                frameSize.Y - (2 * boundingOffset.Y));
        }

        /// <summary>
        /// Accessor for the frameSize variable.
        /// </summary>
        /// <returns> Returns a copy of the Point object representing the 
        /// framesize.</returns>
        public Point GetFrameSize()
        {
            return new Point(frameSize.X, frameSize.Y);
        }

        /// <summary>
        /// Function to increment the frame variable
        /// </summary>
        public void IncrementFrame(int elapsedTime)
        {
            // Update timing
            timeSinceLastFrame += elapsedTime;

            // Update sprite animation, if time has past
            if (timeSinceLastFrame > frameDuration)
            {
                timeSinceLastFrame -= frameDuration;
                currentFrame.X++;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;

                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        /// <summary>
        /// Function to calculate the rectangle of the next frame on the sprite
        /// sheet </summary>
        /// <returns>
        /// next frame's rectangle </returns>
        Rectangle NextFrame()
        {
            return new Rectangle(currentFrame.X * frameSize.X,
                currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
        }

        /// <summary>
        /// Function to update a sprite. Currently only increments the frame if
        /// the sprite is animated. </summary>
        /// <param name="gameTime"> Time object to indicate the amount of time 
        /// that's past.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (animated)
                if (frameDuration > 0)
                    IncrementFrame(gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// Function to tell a sprite to draw itself.</summary>
        /// <param name="spriteBatch">
        /// The spriteBatch that will draw the sprite.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                //Draw the sprite.
                spriteBatch.Draw(
                   texture, position, NextFrame(),Color.White, 0,Vector2.Zero,
                   1, SpriteEffects.None, 1);
            }
            // Handle unbegun spriteBatches
            catch (InvalidOperationException e)
            {
                Console.Out.WriteLine(
                    "Sprite.Draw call outside of SpriteBatch.Begin() and\n" +
                    "End() calls. Error type: " + e.GetType().ToString());
            }
        }

        /// <summary>
        /// Function to tell a sprite to draw itself at a specified 
        /// location.</summary>
        /// <param name="spriteBatch">
        /// The spriteBatch that will draw the sprite.</param>
        /// <param name="location">
        /// The location to draw the sprite at.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            try
            {
                //Draw the sprite.
                spriteBatch.Draw(
                    texture, location, NextFrame(), Color.White, 0,
                    Vector2.Zero, 1, SpriteEffects.None, 1);
            }
            // Handle unbegun spriteBatches
            catch (InvalidOperationException e)
            {
                Console.Out.WriteLine(
                    "Sprite.Draw call outside of SpriteBatch.Begin() and\n" +
                    "End() calls. Error type: " + e.GetType().ToString());
            }
        }

        /// <summary>
        /// overload of draw to draw into a specific rectangle </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the sprite with.</param>
        /// <param name="view">
        /// The Rectangle to draw into.</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle view)
        {
            try
            {
                //Draw the sprite.
                spriteBatch.Draw(
                    texture, view, NextFrame(), Color.White, 0, Vector2.Zero,
                    SpriteEffects.None, 1);
            }
            // Handle unbegun spriteBatches
            catch (InvalidOperationException e)
            {
                Console.Out.WriteLine(
                    "Sprite.Draw call outside of SpriteBatch.Begin() and\n" +
                    "End() calls. Error type: " + e.GetType().ToString());
            }
        }

        /// <summary>
        /// Function to update the bouding box. to be called whenever the 
        /// sprite moves.</summary>
        void updateBoundingBox()
        {
            boundingBox.X = (int)(position.X + boundingOffset.X);
            boundingBox.Y = (int)(position.Y + boundingOffset.Y);
        }

        /// <summary>
        /// Function to move a sprite to the given location.</summary>
        /// <param name="targetLocation">
        /// The location to move the sprite to.</param>
        public void MoveTo(Vector2 targetLocation)
        {
            // Change the position
            position = targetLocation;

            // Update the bounding Box
            updateBoundingBox();
        }

        /// <summary>
        /// Function to check if this sprite resides fully within the given 
        /// Rectangle.</summary>
        /// <param name="rect"> 
        /// The Rectangle object to check against</param>
        /// <returns> True if this sprite is bound by this rectangle, false if 
        /// not.</returns>
        public Boolean IsBoundBy(Rectangle rect)
        {
            // use existing bounding algorithm
            return rect.Contains(this.boundingBox);
        }

        /// <summary>
        /// Function to ensure a sprite does not leave the game 
        /// window.</summary>
        /// <param name="clientBounds"> 
        /// The rectangle describing the game window.</param>
        public void ContainWithin(Rectangle bounds)
        {
            // check for left edge
            if (boundingBox.X < 0)
                position.X = 0;
            // check for top edge
            if (boundingBox.Y < 0)
                position.Y = 0;
            // check for right edge
            if (boundingBox.X > (bounds.Width - boundingBox.Width))
                position.X = (bounds.Width - boundingBox.Width);
            // check for left edge
            if (boundingBox.Y > (bounds.Height - boundingBox.Height))
                position.Y = (bounds.Height - boundingBox.Height);

            // update the bounding box
            updateBoundingBox();
        }

        /// <summary>
        /// Function to check if this sprite collides with another.</summary>
        /// <param name="other">
        /// The sprite to check for collision with</param>
        /// <returns> 
        /// True if there is a collision, false if not.</returns>
        public Boolean CollidesWith(Sprite other)
        {
            // use existing intersection check
            return boundingBox.Intersects(other.boundingBox);
        }

        /// <summary>
        /// Function to check if this sprite collides with a point.</summary>
        /// <param name="point"> 
        /// The point to check for collision with</param>
        /// <returns> 
        /// True if there is a collision, false if not.</returns>
        public Boolean CollidePoint(Point point)
        {
            // use existing containment check
            return boundingBox.Contains(point);
        }
    }
}
