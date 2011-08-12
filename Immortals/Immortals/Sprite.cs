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

        // drawing variables
        Texture2D texture;
        Vector2 position;

        // collision variables
        Point boundingOffset;
        Rectangle boundingBox;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture"> The texture to associate with this sprite</param>
        /// <param name="frameSize"> The size of each frame in pixels</param>
        /// <param name="sheetSize"> The dimensions of the sheet in frames</param>
        /// <param name="frameDuration"> The number of milliseconds to display each frame</param>
        /// <param name="boundingOffset"> The number of pixels the bounding box is offset from the frame edges</param>
        public Sprite(Texture2D texture, Point frameSize, 
            Point sheetSize, int frameDuration, Point boundingOffset)
        {
            // Save data
            this.texture = texture;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.frameDuration = frameDuration;
            this.boundingOffset = boundingOffset;

            // Initialize variables
            this.timeSinceLastFrame = 0;
            this.currentFrame = new Point(0, 0);

            // Calculate the bounding box
            this.boundingBox = new Rectangle(
                boundingOffset.X, 
                boundingOffset.Y, 
                frameSize.X - (2 * boundingOffset.X), 
                frameSize.Y - (2 * boundingOffset.Y));
        }

        /// <summary>
        /// Accessor for the frameSize variable.
        /// </summary>
        /// <returns> Returns a copy of the Point object representing the framesize.</returns>
        public Point GetFrameSize()
        {
            return new Point(this.frameSize.X, this.frameSize.Y);
        }

        /// <summary>
        /// Function to increment the frame variable
        /// </summary>
        public void IncrementFrame(int elapsedTime)
        {
            // Update timing
            this.timeSinceLastFrame += elapsedTime;

            // Update sprite animation, if time has past
            if (this.timeSinceLastFrame > this.frameDuration)
            {
                this.timeSinceLastFrame -= this.frameDuration;
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

        /// <summary>
        /// Function to update a sprite. Currently only increments the 
        /// frame if the sprite is animated
        /// </summary>
        /// <param name="gameTime"> Time object to indicate the amount of
        /// time that's past.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (this.frameDuration > 0)
                this.IncrementFrame(gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// Function to tell a sprite to draw itself.
        /// </summary>
        /// <param name="spriteBatch"> The spriteBatch that will draw the sprite.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
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
        /// Function to tell a sprite to draw itself at a specified location.
        /// </summary>
        /// <param name="spriteBatch"> The spriteBatch that will draw the sprite.</param>
        /// <param name="location"> The location to draw the sprite at.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            try
            {
                //Draw the sprite.
                spriteBatch.Draw(this.texture,
                   location,
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
        /// overload of draw to draw into a specific rectangle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="view"></param>
        public void Draw(SpriteBatch spriteBatch, Rectangle view)
        {
            //Console.Out.WriteLine("Drawing in " + view.ToString());
            try
            {
                //Draw the sprite.
                spriteBatch.Draw(this.texture,
                    view,
                    this.NextFrame(),
                    Color.White,
                    0,
                    Vector2.Zero,
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

        /// <summary>
        /// Function to check if this sprite collides with another
        /// </summary>
        /// <param name="other"> The sprite to check for collision with</param>
        /// <returns> True if there is a collision, false if not.</returns>
        public Boolean CollidesWith(Sprite other)
        {
            return this.boundingBox.Intersects(other.boundingBox);
        }

        /// <summary>
        /// Function to check if this sprite collides with a point
        /// </summary>
        /// <param name="point"> The point to check for collision with</param>
        /// <returns> True if there is a collision, false if not.</returns>
        public Boolean CollidePoint(Point point)
        {
            return this.boundingBox.Contains(point);
        }
    }
}
