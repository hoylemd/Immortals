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

            // display data
            this.clientBounds = game.Window.ClientBounds;
            this.boardView = new Rectangle(0, 0, this.clientBounds.Width - 300, 
                this.clientBounds.Height);
            this.sidebarView = new Rectangle(this.boardView.Width, 0, 300,
                this.clientBounds.Height);

            // sprite managment
            this.spriteManager = new SpriteManager(game, this);
            game.Components.Add(this.spriteManager);

            // Model management
            this.modelManager = new ModelManager(game, this);
            game.Components.Add(this.modelManager);

        }

        public override void Initialize()
        {
            this.mainCamera = new Camera(Game, new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);
            engine.Components.Add(this.mainCamera);

            base.Initialize();
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
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Function to pan the game view around
        /// </summary>
        /// <param name="direction">Point object representing in which 
        /// directions panning is happening.
        /// both dimensions should be 1, 0 or -1</param>
        public void Pan(Point direction)
        {

        }

        void PanExact(Point displacement)
        {
            
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
               
            }
            else
            {
                
            }


        }

        /// <summary>
        /// Accessor for the zoom level
        /// </summary>
        /// <returns>int representing the zoom level.</returns>
        public int GetZoom()
        {
            return 0;
        }

        /// <summary>
        /// Accessor for the board rectangle to be viewed.
        /// </summary>
        /// <returns> a rectangle object representing where the view of the 
        /// board is.</returns>
        public Rectangle GetBoardView()
        {
            return this.boardView;
        }

        public override void Draw(GameTime gameTime)
        {
            // Clear the background

            base.Draw(gameTime);
        }
    }
}
