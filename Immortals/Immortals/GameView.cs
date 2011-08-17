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
    /// Class to Handle all graphics in the game
    /// </summary>
    public class GameView : Microsoft.Xna.Framework.DrawableGameComponent       
    {
        // Main Camera
        public Camera mainCamera { get; protected set; }

        // Rectangle representing the game window
        Rectangle clientBounds;

        // sprite manager
        SpriteManager spriteManager;

        // model manager
        ModelManager modelManager;

        // Rectangles and viewports for sidebar and board views
        Rectangle sidebarView;
        Viewport sidebarViewport;
        Rectangle boardView;
        Viewport boardViewport;

        // Game engine pointer
        public ImmortalsEngine engine { get; private set; }

        // graphics device
        public GraphicsDevice graphics { get; private set; }

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

            // Set up the main camera;
            mainCamera = new Camera(
                Game, this, new Vector3(0, 0, 25), Vector3.Zero, Vector3.Up,
                boardView);
            engine.Components.Add(mainCamera);


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

        /// <summary>Function to update the view.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            // update children
            modelManager.Update(gameTime);
            spriteManager.Update(gameTime);
        }

        /// <summary>
        /// Function to pan the game view around
        /// </summary>
        /// <param name="direction">Point object representing in which 
        /// directions panning is happening. Both dimensions should be 1, 0 
        /// or -1</param>
        public void Pan(Point direction)
        {

        }

        /// <summary>
        /// Function to pan to a specific location.
        /// </summary>
        /// <param name="target">Coordinates of where to pan to.</param>
        void PanExact(Point target)
        {
            
        }

        /// <summary>Function to zoom the game view in or out.</summary>
        /// <param name="zoomValue">
        /// Boolean representing representing zooming in or out. True for in, 
        /// false for out.</param>
        public void Zoom(Boolean zoomIn)
        {
            // validate for, and apply for indicated direction
            if (zoomIn)
            {
            }
            else
            {
            }
        }

        /// <summary>Accessor for the zoom level.</summary>
        /// <returns>int representing the zoom level.</returns>
        public int GetZoom()
        {
            return 0;
        }

        /// <summary>
        /// Function to draw any general components.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // draw children in their viewports
            GraphicsDevice.BlendState = BlendState.Opaque; 
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.Viewport = boardViewport;
            modelManager.Draw(gameTime);


            engine.GraphicsDevice.Viewport = sidebarViewport;
            spriteManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
