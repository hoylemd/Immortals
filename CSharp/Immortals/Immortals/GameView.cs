using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Immortals
{
    /// <summary>
    /// Class to represent where the view is centered and zoomed.
    /// </summary>
    class GameView
    {
        // Variables to represent zoom and pan
        int zoom;
        int zoomSpeed;
        int minZoom;
        int maxZoom;
        Point pan;
        int panSpeed;

        // Rectangle representing the game window
        Rectangle clientBounds;

        // Rectangle representing the game board
        Rectangle board;
        Rectangle boardDisplayed;

        // sprite manager
        SpriteManager spriteManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientBounds"> Rectangle representing the window size the game is being run in.</param>
        /// <param name="spriteManager"></param>
        /// <param name="boardFrameSize"></param>
        public GameView(Rectangle clientBounds, SpriteManager spriteManager, Point boardFrameSize)
        {
            // store all data and pointers
            this.zoom = 100;
            this.zoomSpeed = 1;
            this.minZoom = 0;
            this.maxZoom = 100;
            this.pan = new Point(0, 0);
            this.panSpeed = 25;
            this.clientBounds = clientBounds;
            this.spriteManager = spriteManager;

            // calculate the board rectangles
            this.board = new Rectangle(0, 0, boardFrameSize.X, boardFrameSize.Y);
            this.boardDisplayed = new Rectangle(0, 0, boardFrameSize.X, boardFrameSize.Y);

            // recalculate minimum zoom
            this.minZoom = (int)((float)clientBounds.Height / (float)boardFrameSize.Y * 100);
        }

        /// <summary>
        /// Function to update the board location
        /// </summary>
        public void Update()
        {
            float zoomRatio = 0;
            // update the board's location.
            this.boardDisplayed.Location = new Point(this.board.X + pan.X, this.board.Y + pan.Y);

            // update the board's draw size.
            zoomRatio = (float)this.zoom / (float)this.maxZoom;
            this.boardDisplayed.Width = (int)(zoomRatio * (float)this.board.Width);
            this.boardDisplayed.Height = (int)(zoomRatio * (float)this.board.Height);

            // update the sprite manager with the drawing information
            this.spriteManager.SetBoardView(this.boardDisplayed);
        }

        /// <summary>
        /// Function to pan the game view around
        /// </summary>
        /// <param name="direction">Point object representing in which directions panning is happening.
        /// both dimensions should be 1, 0 or -1</param>
        public void Pan(Point direction)
        {
            // calculate the pan distances
            int panX = direction.X * panSpeed + this.pan.X;
            int panY = direction.Y * panSpeed + this.pan.Y;

            // validate them
            
            // update the pan
            this.pan.X += panX;
            this.pan.Y += panY;
        }

        /// <summary>
        /// Function to zoom the game view in or out
        /// </summary>
        /// <param name="zoomValue">int representing the zoom value of the mouse wheel.</param>
        public void Zoom(int zoomValue)
        {
            // calculate the new zoom value
            int newZoom = (zoomValue * this.zoomSpeed) + this.zoom;

            // apply it, but respect max and min zooms.
            if (newZoom >= this.maxZoom)
                this.zoom = this.maxZoom;
            else if (newZoom <= this.minZoom)
                this.zoom = this.minZoom;
            else
                this.zoom = newZoom;

            Console.Out.WriteLine("gameview zoom: " + this.zoom + ", min: " + this.minZoom);
        }

        /// <summary>
        /// Accessor for the zoom variable
        /// </summary>
        /// <returns>int representing how far in the view is zoomed.</returns>
        public int GetZoom()
        {
            return this.zoom;
        }

        /// <summary>
        /// Accessor for the board rectangle to be viewed.
        /// </summary>
        /// <returns> a rectangle object representing where the view of the board is.</returns>
        public Rectangle GetBoardView()
        {
            return this.boardDisplayed;
        }
    }
}
