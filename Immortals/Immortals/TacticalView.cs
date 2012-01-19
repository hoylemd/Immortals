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
    /// Class to represent a normal tactical view
    /// </summary>
    class TacticalView : SpriteContainer
    {
        // panning factors
        private double cameraRestrictionFactor; // Affects the reduction in 
                                            // space the camera may move in.

        // board pointer
        Board board;
        Sprite boardSprite;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="area">
        /// The area this view occupies in the parent container</param>
        /// <param name="camera"> The camera to use for drawing this view</param>
        public TacticalView(Rectangle area)
            : base(area) 
        {
            // This restricts the camera from moving out more than 35% of the
            // board's width from the origin. This prevents the camera from
            // overlooking too much of the board when panned maximally.
            this.cameraRestrictionFactor = 0.35;
        }

        /// <summary>
        /// Function to register a game board with the view.  
        /// Also sets up panning constraints
        /// </summary>
        /// <param name="board">The board to register</param>
        public void registerBoard(Board board)
        {
            // save the board
            this.board = board;
            this.boardSprite = board.sprite;

            // calculate and set maximum panning distance
            Vector2 maxPan = new Vector2(
                (float)(cameraRestrictionFactor * (double)board.size.X * 100),
                (float)(cameraRestrictionFactor * (double)board.size.Y * 100));
            this.setMaxPan(maxPan);
        }

        /// <summary>
        /// Function to set the view's maximum panning distance
        /// </summary>
        /// <param name="displacement">
        /// The maximum distance in x and y the camera may pan</param>
        public void setMaxPan(Vector2 displacement)
        {
            // set the camera's maximum pan
            //camera.maxPan = displacement;
        }

        /// <summary>
        /// Function to pan the view.
        /// </summary>
        /// <param name="direction">The direction to pan</param>
        public void Pan(Point direction)
        {
            // pan the camera
            //camera.Pan(direction);
        }

        public override void Draw(Rectangle drawArea, SpriteBatch spriteBatch)
        {
            boardSprite.Draw(spriteBatch);
        }
    }
}
