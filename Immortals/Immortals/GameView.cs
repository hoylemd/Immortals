
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

///////////////////////////////////////////////////////////////////////////////
namespace Immortals
{
    /// <summary>
    /// Class to Handle all graphics in the game
    /// </summary>
    public class GameView : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // Panning variables
        private Boolean panningAllowed; // Flag to allow or disallow panning

        // Rectangle representing the game window
        Rectangle clientBounds;

        // Input settings
        int panBuffer;  // represents how close to the edge of the window the
                        // cursor must be to trigger a pan.

        // sprite mangement
        //SpriteManager spriteManager;
        Sidebar sidebar;
        SpriteBatch spriteBatch;

        // model manager
        TacticalView tacticalView;

        // Rectangles and viewports for sidebar and board views
        Rectangle sidebarView;
        Rectangle boardView;

        // Input Records
        private MouseState prevMouseState;

        // Game engine pointer
        public ImmortalsEngine engine { get; private set; }

        // graphics device
        public GraphicsDevice graphics { get; private set; }

        // Drawing Constraints
        private int targetFPS = 60;
        private int msBetweenFrames;
        int timeSinceLastDraw = 0;

        /// <summary>Constructor.</summary>
        /// <param name="game">The top-level game object.</param>
        public GameView(ImmortalsEngine game)
            : base(game)
        {
            // store and/or initialize all data and pointers
            this.engine = game;
        }

        /// <summary>Function to initialize local members.</summary>
        public override void Initialize()
        {
            // Set up displays
            clientBounds = engine.Window.ClientBounds;
            boardView = new Rectangle(
                0, 0, clientBounds.Width - 300, clientBounds.Height);
            sidebarView = new Rectangle(
                boardView.Width, 0, 300, clientBounds.Height);

            // Create subcomponents

            // Model management
            boardView.X = 0;
            this.tacticalView = new TacticalView(boardView);

            // Sprite/sidebar managment
            this.sidebar = new Sidebar(new Rectangle(0, 0, 300, 1080));

            // set up panning
            panningAllowed = true;

            // set up input settings
            panBuffer = 25;

            // set up drawing constraints
            targetFPS = 60;
            msBetweenFrames = 1000 / targetFPS;
            timeSinceLastDraw = 0;

            base.Initialize();
        }

        /// <summary>
        /// Function to load up content.
        /// </summary>
        protected override void LoadContent()
        {
            // initialize children
            //gameWindow.Initialize(this.GraphicsDevice);

            // temporary variables
            Point boardSize;
    
            // Generate some terrain
            boardSize = new Point(30, 30);
            tacticalView.registerBoard(
                new Board(boardSize, 
                    new Sprite(engine.Content.Load<Texture2D>(
                               @"Images/Terrains/Grass/grass 40x40 board"),
                               new Point(3000,3000), new Point(1,1), 0, Point.Zero)));

            // set the sidebar's background
            sidebar.setBackground(
                engine.Content.Load<Texture2D>(@"Images/sidebarred1080"),
                new Point(300, 1080), new Point(1, 1), 0);

            // set up the spriteBatch
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>Function to update the view.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            Point panDirection = new Point(0,0);
            Vector2 panDisplacement = Vector2.Zero;

            timeSinceLastDraw += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastDraw > msBetweenFrames)
            {
                timeSinceLastDraw = 0;

                // Poll input
                MouseState mouseState = Mouse.GetState();
                int mouseX = mouseState.X;
                int mouseY = mouseState.Y;

                // check for clicking
                if (prevMouseState.LeftButton == ButtonState.Pressed && 
                    mouseState.LeftButton == ButtonState.Released)
                {
                    Vector3 nearSource = new Vector3((float)mouseX, (float)mouseY, 0f);
                    Vector3 farSource = new Vector3((float)mouseX, (float)mouseY, 1f);
                }

                // check for panning
                if (panningAllowed)
                {
                    // determine direction
                    if (mouseState.X <= panBuffer)
                    {
                        panDirection.X = 1;
                    }
                    if (mouseState.X >= (clientBounds.Width - panBuffer))
                    {
                        panDirection.X = -1;
                    }
                    if (mouseState.Y <= panBuffer)
                    {
                        panDirection.Y = 1;
                    }
                    if (mouseState.Y >= (clientBounds.Height - panBuffer))
                    {
                        panDirection.Y = -1;

                    }

                    // execute pan
                    if (!panDirection.Equals(Point.Zero))
                        tacticalView.Pan(panDirection);
                }



                // update children
                //gameWindow.Update(gameTime);

                // record old input state
                prevMouseState = mouseState;
            }
        }

        /// <summary>
        /// Function to pan the game view around. also ensures the camera 
        /// cannot be panned to far as to lose the game board.
        /// </summary>
        /// <param name="direction">Point object representing in which 
        /// directions panning is happening. Both dimensions should be 1, 0 
        /// or -1</param>
        /*public void Pan(Point direction)
        {
            // temporary variables
            Vector3 camPosition = mainCamera.cameraPosition;
            Vector3 newPosition = new Vector3(
                camPosition.X, camPosition.Y, camPosition.Z);

            // validate and calculate X & Y
            if (Math.Abs(camPosition.X + direction.X) <= maxPan.X)
            {
                newPosition.X += direction.X;
            }
            if (Math.Abs(camPosition.Y + direction.Y) <= maxPan.Y)
            {
                newPosition.Y += direction.Y;
            }

            // move the camera
            this.mainCamera.MoveCamera(newPosition);
                
        }*/

        /// <summary>
        /// Function to draw any general components.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // draw children in their viewports
            //GraphicsDevice.Viewport = boardViewport;
            spriteBatch.Begin();
            tacticalView.Draw(boardView, spriteBatch);

            //engine.GraphicsDevice.Viewport = sidebarViewport;

            //System.Console.WriteLine("sidebar view: " + sidebarView.ToString());
            sidebar.Draw(sidebarView, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}