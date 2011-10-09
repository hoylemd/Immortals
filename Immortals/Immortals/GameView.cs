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
        // Main Camera
        public Camera mainCamera { get; protected set; }
        private Vector3 CameraAngle;

        // Panning variables
        private Boolean panningAllowed;
        private Vector2 maxPan;

        // Rectangle representing the game window
        Rectangle clientBounds;

        // Input settings
        int panBuffer;  // represents how close to the edge of the window the
                        // cursor must be to trigger a pan.

        // sprite manager
        SpriteManager spriteManager;

        // model manager
        ModelManager modelManager;

        // Rectangles and viewports for sidebar and board views
        Rectangle sidebarView;
        Viewport sidebarViewport;
        Rectangle boardView;
        Viewport boardViewport;

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
            // store and/or initlialize all data and pointers
            this.engine = game;

            // Create subcomponents
            // Model management
            this.modelManager = new ModelManager(game, this);

            // Sprite managment
            this.spriteManager = new SpriteManager(game, this);

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
            boardViewport = new Viewport(boardView);
            sidebarViewport = new Viewport(sidebarView);

            // Set up the main camera
            CameraAngle = new Vector3(0, -10, -20);
            CameraAngle.Normalize();
            mainCamera = new Camera(
                Game, this, CameraAngle * 20, Vector3.Zero, Vector3.Up,
                boardView);
            engine.Components.Add(mainCamera);

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
            modelManager.Initialize();
            spriteManager.Initialize();

            // Load children's content
            modelManager.LoadContent();
            spriteManager.LoadContent();

            base.LoadContent();
        }

        /// <summary>
        /// Function to register a board size so panning can be restricted.
        /// </summary>
        /// <param name="boardSize"> The dimensions of the board in DU</param>
        public void RegisterBoardSize(Point boardSize)
        {
            // calculate the maximum panning displacement
            maxPan = new Vector2(
                (float)((double)boardSize.X / 2.0),
                (float)((double)boardSize.Y / 2.0));
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

                // check for panning
                // determine direction
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                }
                else
                {
                    //panningAllowed = false;
                }
                if (panningAllowed)
                {
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
                    Pan(panDirection);
                }
                // update children
                modelManager.Update(gameTime);
                spriteManager.Update(gameTime);

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
        public void Pan(Point direction)
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
                
        }

        /// <summary>
        /// Function to draw any general components.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // draw children in their viewports
            GraphicsDevice.Viewport = boardViewport;
            modelManager.Draw(gameTime);

            engine.GraphicsDevice.Viewport = sidebarViewport;
            spriteManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}