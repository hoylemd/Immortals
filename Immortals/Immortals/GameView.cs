using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Immortals
{
    /// <summary>
    /// Class to represent where the view is centered and zoomed.
    /// </summary>
    public class GameView : Microsoft.Xna.Framework.DrawableGameComponent
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

        // Variables to represent zoom
        int zoom;
        double zoomRatio;
        //int zoomSpeed;
        int minZoom;
        int maxZoom;
        Boolean zoomed;

        // variables to represent pan
        Point pan;
        Point panOffset;
        Rectangle panBounds;
        int panSpeed;
        Boolean panned;

        // Main Camera
        Camera mainCamera;

        // Rectangle representing the game window
        Rectangle clientBounds;

        // Rectangle representing the game board
        Rectangle board;
        Rectangle boardDisplayed;

        // sprite manager
        SpriteManager spriteManager;

        // Rectangles representing the sidebar and board areas
        Rectangle sidebarView;
        Rectangle boardView;

        // game engine pointer
        ImmortalsEngine engine;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientBounds"> Rectangle representing the window size 
        /// the game is being run in.</param>
        /// <param name="spriteManager"></param>
        /// <param name="boardFrameSize"></param>
        public GameView(ImmortalsEngine game)
            : base(game)
        {
            // store and.or initlialize all data and pointers
            this.engine = game;

            // zoom data
            this.zoom = 0;
            //this.zoomSpeed = 1;
            this.minZoom = 0;
            this.maxZoom = 4;

            // display data
            this.clientBounds = game.Window.ClientBounds;
            this.boardView = new Rectangle(0, 0, this.clientBounds.Width - 300, 
                this.clientBounds.Height);
            this.sidebarView = new Rectangle(this.boardView.Width, 0, 300,
                this.clientBounds.Height);

            // sprite managment
            this.spriteManager = new SpriteManager(game);
            game.Components.Add(this.spriteManager);

        }

        protected override void LoadContent()
        {
            Point mapSize = new Point(3000, 3000);
            Rectangle bounds = new Rectangle(0, 0, 750, 750);

            // Load the sprite textures
            Texture2D immortalTexture = engine.Content.Load<Texture2D>(
                @"Images/immortalSmall");
            Texture2D selectedTexture = engine.Content.Load<Texture2D>(
                @"Images/selected");

            // use the void map
            spriteManager.RegisterMap(mapSize,
                new Sprite(engine.Content.Load<Texture2D>(
                        @"Images/Terrains/Void/void 40x40 board"),
                    mapSize,
                    new Point(1, 1),
                    0,
                    new Point(0, 0)));

            // make the sidebar
            spriteManager.MakeSidebar(
                engine.Content.Load<Texture2D>(@"Images/sidebarScroll"),
                new Point(300, 750),
                this.clientBounds);

            // Make a sprite
            Sprite thing = new Sprite(
                engine.Content.Load<Texture2D>(@"Images/rotatingThing"),
                new Point(75, 75),
                new Point(3, 4),
                41,
                new Point(10, 10));
            spriteManager.AddSprite(thing);

            base.LoadContent();
        }

        /// <summary>
        /// Function to update the board location
        /// </summarawey>
        public void Update()
        {
            int zoomedHeight;
            int zoomedWidth;
            int changeHeight;
            int changeWidth;

            // update the board's draw size if zooming occurred.
            if (this.zoomed)
            {
                //Console.Out.WriteLine("zoomed " + this.zoom);
                
                // recalculate zoom Ratio
                this.zoomRatio = GameView.ZoomRatios[this.zoom];

                // recalculate the draw size of the view
                zoomedHeight = (int)(this.zoomRatio * (double)this.board.Height);
                zoomedWidth = (int)(this.zoomRatio * (double)this.board.Width);

                // calculate the change in size
                changeWidth = (zoomedWidth - this.boardDisplayed.Width);
                changeHeight = (zoomedHeight - this.boardDisplayed.Height);

                // mark panned so the view location is readjusted
                this.panned = true;

                // pan half the change in hight/width so the view stays centered
                // this.PanExact(new Point(changeWidth / -2, changeHeight / -2));

                // change the draw size
                this.boardDisplayed.Width = zoomedWidth;
                this.boardDisplayed.Height = zoomedHeight;

                // Console.Out.WriteLine("zW:" + zoomedWidth + " zH: " +
                //    zoomedHeight + " dW:" + this.boardDisplayed.Width + " dH:" +
                //    this.boardDisplayed.Height);
            }

            // update the board's location.
            if (this.panned)
            {

                // Console.Out.WriteLine("Pan: " + this.pan.ToString() + ", boardsize: " + this.board.Width + ", " + this.board.Height);


                if (this.pan.X < this.panBounds.X)
                    this.pan.X = this.panBounds.X;
                if (this.pan.X > (this.panBounds.X + this.panBounds.Width))
                    this.pan.X = (this.panBounds.X + this.panBounds.Width);

                if (this.pan.Y < this.panBounds.Y)
                    this.pan.Y = this.panBounds.Y;
                if (this.pan.Y > (this.panBounds.Y + this.panBounds.Height))
                    this.pan.Y = (this.panBounds.Y + this.panBounds.Height);

                this.boardDisplayed.Location = new Point(
                    this.board.X - (this.pan.X - this.panOffset.X),
                    this.board.Y - (this.pan.Y - this.panOffset.Y));

                // Console.Out.WriteLine("drawing at " + this.boardDisplayed.Location.ToString() + " should be no more than " + ((this.zoomRatio * this.board.Height) * -1) + ", " + ((this.zoomRatio * this.board.Width) * -1));
                // Console.Out.WriteLine("draw size dW:"+ this.boardDisplayed.Width + " dH:" + this.boardDisplayed.Height);
                // Console.Out.WriteLine("should be " + this.zoomRatio * this.board.Width + ", " + this.zoomRatio * this.board.Height);
            
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
            int panX = (int)((double)displacement.X / this.zoomRatio) + this.pan.X;
            int panY = (int)((double)displacement.Y / this.zoomRatio) + this.pan.Y;

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
            if (zoomIn)
            {
                if (this.zoom > this.minZoom)
                    this.zoom -= 1;
            }
            else
            {
                if (this.zoom < this.maxZoom)
                    this.zoom += 1;
            }

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

        public override void Draw(GameTime gameTime)
        {
            // Clear the background
            // GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
