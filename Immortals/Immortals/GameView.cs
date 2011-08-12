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

        // Rectangles representing the sidebar and board areas
        Rectangle sidebarView;
        Rectangle boardView;

        // game engine pointer
        ImmortalsEngine engine;

        /// <summary>Constructor.</summary>
        /// <param name="game">The top-level game object.</param>
        public GameView(ImmortalsEngine game)
            : base(game)
        {
            // store and/or initlialize all data and pointers
            this.engine = game;

            // Display data
            this.clientBounds = game.Window.ClientBounds;
            this.boardView = new Rectangle(
                0, 0, this.clientBounds.Width - 300, this.clientBounds.Height);
            this.sidebarView = new Rectangle(
                this.boardView.Width, 0, 300,this.clientBounds.Height);

            // Create subcomponents
            // Model management
            this.modelManager = new ModelManager(game, this);
            game.Components.Add(this.modelManager);

            // Sprite managment
            this.spriteManager = new SpriteManager(game, this);
            game.Components.Add(this.spriteManager);

        }

        /// <summary>Function to initialize local members.</summary>
        public override void Initialize()
        {
            // Set up the main camera;
            this.mainCamera = new Camera(Game, new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);
            engine.Components.Add(this.mainCamera);

            base.Initialize();
        }

        /// <summary>
        /// Function to load up content.
        /// </summary>
        protected override void LoadContent()
        {
            Rectangle bounds = new Rectangle(0, 0, 750, 750);

            // Make the sidebar
            spriteManager.MakeSidebar(
                engine.Content.Load<Texture2D>(@"Images/sidebarScroll"),
                new Point(300, 750),
                this.clientBounds);

            base.LoadContent();
        }

        /// <summary>Function to update the view.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
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

            base.Draw(gameTime);
        }
    }
}
