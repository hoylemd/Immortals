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
        int millisecondsPerFrame;

        // drawing variables
        Texture2D texture;
        Vector2 position;

        // collision variables
        Point boundingOffset;
        Rectangle boundingBox;

        // constructor
        public Sprite(Texture2D texture, Point frameSize, Point sheetSize, int msPerFrame, Point boundingOffset)
        {
            this.texture = texture;
            this.frameSize = frameSize;
            this.currentFrame = new Point(0, 0);
            this.sheetSize = sheetSize;
            this.timeSinceLastFrame = 0;
            this.millisecondsPerFrame = msPerFrame;

            this.boundingBox = new Rectangle(boundingOffset.X, boundingOffset.Y, frameSize.X - (2 * boundingOffset.X), frameSize.Y - (2 * boundingOffset.Y));
            this.boundingOffset = boundingOffset;
        }

        /// <summary>
        /// Function to increment the frame variable
        /// </summary>
        public void IncrementFrame(int elapsedTime)
        {
            // Update timing
            this.timeSinceLastFrame += elapsedTime;

            // Update sprite animation, if time has past
            if (this.timeSinceLastFrame > this.millisecondsPerFrame)
            {
                this.timeSinceLastFrame -= this.millisecondsPerFrame;
                this.currentFrame.X++;
                if (this.currentFrame.X >= this.sheetSize.X)
                {
                    this.currentFrame.X = 0;
                    this.currentFrame.Y++;

                    if (this.currentFrame.Y >= this.sheetSize.Y)
                        this.currentFrame.Y = 0;
                }

            }
        }

        /// <summary>
        /// Function to calculate the rectangle of the next frame on the sprite sheet
        /// </summary>
        /// <returns>
        /// next frame's rectangle
        /// </returns>
        Rectangle NextFrame()
        {
            return new Rectangle(this.currentFrame.X * this.frameSize.X,
                this.currentFrame.Y * this.frameSize.Y,
                this.frameSize.X,
                this.frameSize.Y);
        }

        public void Update(GameTime gameTime)
        {
            this.IncrementFrame(gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// Function to tell a sprite to draw itself.
        /// </summary>
        /// <param name="spriteBatch"> The spriteBatch that will draw the sprite.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                //Draw the sprite.
                spriteBatch.Draw(this.texture,
                   this.position,
                   this.NextFrame(),
                   Color.White,
                   0,
                   Vector2.Zero,
                   1,
                   SpriteEffects.None,
                   1);
            }
            // Handle unbegun spriteBatches
            catch (InvalidOperationException e)
            {
                Console.Out.WriteLine("Sprite.Draw call outside of SpriteBatch.Begin() and End() calls. Error type: " + e.GetType().ToString());
            }

        }

        /// <summary>
        /// Function to update the bouding box. to be called whenever the sprite moves.
        /// </summary>
        void updateBoundingBox()
        {
            this.boundingBox.X = (int)(this.position.X + boundingOffset.X);
            this.boundingBox.Y = (int)(this.position.Y + boundingOffset.Y);
        }

        /// <summary>
        /// Function to move a sprite to the given location
        /// </summary>
        /// <param name="targetLocation"> the location to move the sprite to</param>
        public void MoveTo(Vector2 targetLocation)
        {
            this.position = targetLocation;

            updateBoundingBox();
        }

        /// <summary>
        /// Function to check if this sprite resides fully within the given Rectangle.
        /// </summary>
        /// <param name="rect"> The Rectangle object to check against</param>
        /// <returns> True if this sprite is bound by this rectangle, false if not.</returns>
        public Boolean IsBoundBy(Rectangle rect)
        {
            // check x axis
            if ((this.boundingBox.X >= rect.X && this.boundingBox.X <= (rect.X + rect.Width + this.boundingBox.Width)) &&
                // check y axis
                (this.boundingBox.Y >= rect.Y && this.boundingBox.Y <= (rect.Y + rect.Height + this.boundingBox.Height)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Function to ensure a sprite does not leave the game window.
        /// </summary>
        /// <param name="clientBounds"> The rectangle describing the game window.</param>
        public void ClipWithWindow(Rectangle clientBounds)
        {
            // Debug message
            // Console.Out.WriteLine("bounding box: " + this.boundingBox.ToString());

            // check for left edge
            if (this.boundingBox.X < 0)
                this.position.X = 0;
            // check for top edge
            if (this.boundingBox.Y < 0)
                this.position.Y = 0;
            // check for right edge
            if (this.boundingBox.X > (clientBounds.Width - this.boundingBox.Width))
                this.position.X = (clientBounds.Width - this.boundingBox.Width);
            // check for left edge
            if (this.boundingBox.Y > (clientBounds.Height - this.boundingBox.Height))
                this.position.Y = (clientBounds.Height - this.boundingBox.Height);

            updateBoundingBox();
        }

        public Boolean CollidesWith(Sprite other)
        {
            return this.boundingBox.Intersects(other.boundingBox);
        }
    }
}
