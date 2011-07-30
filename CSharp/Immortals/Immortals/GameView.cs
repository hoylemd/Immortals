﻿using System;
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

        /*Zoom levels 
            1: maximum zoom.1 DU = 75 pixels	(1.000 ratio)
            2: close zoom.	1 DU = 63 pixels	(0.833 ratio)	
            3: medium zoom. 1 DU = 50 pixels	(0.666 ratio)
            4: far zoom.	1 DU = 38 pixels	(0.500 ratio)
            5: minimum zoom.1 DU = 25 pixels	(0.333 ratio)
         * */
        // static zoom ratios
        static double[] ZoomRatios = {1.000, 0.833, 0.666, 0.500, 0.333};

        // Variables to represent zoom and pan
        int zoom;
        double zoomRatio;
        int zoomSpeed;
        int minZoom;
        int maxZoom;
        Point pan;
        int panSpeed;
        Boolean panned;
        Boolean zoomed;

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
        /// <param name="clientBounds"> Rectangle representing the window size 
        /// the game is being run in.</param>
        /// <param name="spriteManager"></param>
        /// <param name="boardFrameSize"></param>
        public GameView(Rectangle clientBounds, SpriteManager spriteManager, 
            Point boardFrameSize)
        {
            // store all data and pointers
            this.zoom = 2;
            this.zoomSpeed = 1;
            this.minZoom = 0;
            this.maxZoom = 4;
            this.pan = new Point(0, 0);
            this.panSpeed = 25;
            this.clientBounds = clientBounds;
            this.spriteManager = spriteManager;

            // calculate the board rectangles
            this.board = new Rectangle(0, 0, boardFrameSize.X, 
                boardFrameSize.Y);
            this.boardDisplayed = new Rectangle(0, 0, boardFrameSize.X, 
                boardFrameSize.Y);

            // flag as zoomed in and panned so it gets recalculated
            this.zoomed = true;
            this.panned = true;
        }

        /// <summary>
        /// Function to update the board location
        /// </summary>
        public void Update()
        {
            int zoomedHeight;
            int zoomedWidth;
            int zoomDeltaX = 0;
            int zoomDeltaY = 0;

            // update the board's draw size if zooming occurred.
            if (this.zoomed)
            {
                Console.Out.WriteLine("zoomed " + this.zoom);
                
                // recalculate zoom Ratio
                this.zoomRatio = GameView.ZoomRatios[this.zoom];

                // recalculate the draw size of the view
                zoomedHeight = (int)(this.zoomRatio * (double)this.board.Height);
                zoomedWidth = (int)(this.zoomRatio * (double)this.board.Width);
                
                Console.Out.WriteLine("zW:" + zoomedWidth + "zH: " + 
                    zoomedHeight + "dW:"+ this.boardDisplayed.Width + "dH:" + 
                    this.boardDisplayed.Height);

                // mark panned so the view location is readjusted
                this.panned = true;

                // change the draw size
                this.boardDisplayed.Width = zoomedWidth;
                this.boardDisplayed.Height = zoomedHeight;
            }

            // update the board's location.
            if (this.panned)
            {

                this.boardDisplayed.Location = new Point(
                    (int)(this.zoomRatio * (double)(this.board.X + pan.X)),
                    (int)(this.zoomRatio * (double)(this.board.Y + pan.Y)));

            }

            // reset panning and zooming flags
            this.panned = false;
            this.zoomed = false;

            // update the sprite manager with the drawing information
            this.spriteManager.SetBoardView(this.boardDisplayed);
        }

        /// <summary>
        /// Function to pan the game view around
        /// </summary>
        /// <param name="direction">Point object representing in which 
        /// directions panning is happening.
        /// both dimensions should be 1, 0 or -1</param>
        public void Pan(Point direction)
        {
            // calculate the pan distances
            int panX = direction.X * this.panSpeed;
            int panY = direction.Y * this.panSpeed;

            this.PanExact(new Point(panX, panY));

        }

        void PanExact(Point displacement)
        {
            // calculate the pan distances
            int panX = displacement.X + this.pan.X;
            int panY = displacement.Y + this.pan.Y;

            // validate them
            if (panX > 0)
                panX = 0;
            else if (panX < (this.clientBounds.Width - 
                this.boardDisplayed.Width))
                panX = this.clientBounds.Width - this.boardDisplayed.Width;

            if (panY > 0)
                panY = 0;
            else if (panY < (this.clientBounds.Height - 
                this.boardDisplayed.Height))
                panY = this.clientBounds.Height - this.boardDisplayed.Height;

            // update them
            this.pan.X = panX;
            this.pan.Y = panY;

            this.panned = true;
        }

        /// <summary>
        /// Function to zoom the game view in or out
        /// </summary>
        /// <param name="zoomValue">boolean representing representing zooming
        /// in or out.</param>
        public void Zoom(Boolean zoomIn)
        {
            // validate for, and apply for indicated direction
            if (zoomIn && (this.zoom > this.minZoom))
                this.zoom -= 1;
            else if (this.zoom < this.maxZoom)
                this.zoom += 1;

            // raise the zoomed flag
            this.zoomed = true;

        }

        /// <summary>
        /// Accessor for the zoom level
        /// </summary>
        /// <returns>int representing the zoom level.</returns>
        public int GetZoom()
        {
            return this.zoom;
        }

        /// <summary>
        /// Accessor for the board rectangle to be viewed.
        /// </summary>
        /// <returns> a rectangle object representing where the view of the 
        /// board is.</returns>
        public Rectangle GetBoardView()
        {
            return this.boardDisplayed;
        }

        /// <summary>
        /// Static method to translate a zom level into a zoom ratio
        /// </summary>
        /// <param name="zoomLevel"> the zoom level to translate</param>
        /// <returns> a double representing the zoom ratio for that zoom
        /// level</returns>
        private static double ZoomLevel(int zoomLevel)
        {
            return GameView.ZoomRatios[zoomLevel];  
        }
    }
}
