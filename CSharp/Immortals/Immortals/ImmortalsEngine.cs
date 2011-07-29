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

namespace Immortals
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ImmortalsEngine : Microsoft.Xna.Framework.Game
    {
        // graphics managers
        GraphicsDeviceManager graphics;
        SpriteManager spriteManager;
        GameView gameView;

        // Texture variables
        Texture2D immortalTexture;
        Texture2D selectedTexture;
        Texture2D pixel;

        // Mouse Variables
        MouseState prevMouseState;

        // Game state variables
        Boolean selectingState;
        Rectangle selectionRect;
        Vector2 selectionOrigin;

        // Sprite Pointers
        Sprite thing;

        /// <summary>
        /// Function to get the least of 2 ints
        /// </summary>
        /// <param name="a">The first int to compare</param>
        /// <param name="b">The second int to compare</param>
        /// <returns>The least of the 2 ints</returns>
        public int leastInt(int a, int b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }

        /// <summary>
        /// Function to get the greatest of 2 ints
        /// </summary>
        /// <param name="a">The first int to compare</param>
        /// <param name="b">The second int to compare</param>
        /// <returns>The greatest of the 2 ints</returns>
        public int greaterInt(int a, int b)
        {
            if (a >= b)
                return a;
            else
                return b;
        }

        /// <summary>
        /// Constructor for the main engine class
        /// </summary>
        public ImmortalsEngine()
        {
            // Set up the game window
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 1250;

            // set up the Content path
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // make visible the mouse
            this.IsMouseVisible = true;

            // initialize game state variables
            selectingState = false;

            // Create the "pixel" object
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            // Create a new SpriteManager, which handles all sprites
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            // Initialize base class
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Point mapSize = new Point(3000,3000);

            // Load the sprite textures
            immortalTexture = Content.Load<Texture2D>(@"Images/immortalSmall");
            selectedTexture = Content.Load<Texture2D>(@"Images/selected");

            // make the background
            spriteManager.GenerateBoard(new Point(15, 15),
                new Sprite(Content.Load<Texture2D>(@"Images/tile"),
                    new Point(75, 75),
                    new Point(1, 1),
                    0,
                    new Point(0, 0)));

            // use the void map
            spriteManager.RegisterMap(mapSize,
                new Sprite(Content.Load<Texture2D>(@"Images/Terrains/Void/void 40x40 board"),
                    mapSize,
                    new Point(1, 1),
                    0,
                    new Point(0,0)));


            // make the sidebar
            spriteManager.MakeSidebar(
                Content.Load<Texture2D>(@"Images/sidebarScroll"),
                new Point(300, 750),
                this.Window.ClientBounds);

            // Make a sprite
            thing = new Sprite(Content.Load<Texture2D>(@"Images/rotatingThing"),
                new Point(75, 75), 
                new Point(3, 4), 
                41, 
                new Point(10,10));
            spriteManager.AddSprite(thing);

            this.gameView = new GameView(this.Window.ClientBounds, this.spriteManager, mapSize);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            // Poll that mouse
            mouseState = Mouse.GetState();

            // Mouse left button down.
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //Console.Out.WriteLine("down.");
                if (prevMouseState.LeftButton == ButtonState.Released)
                {
                    selectingState = true;
                    selectionOrigin.X = mouseState.X;
                    selectionOrigin.Y = mouseState.Y;
                    selectionRect = new Rectangle(mouseState.X, mouseState.Y, 0, 0);
                }
                else
                {
                   // generate the selection rect
                    selectionRect.X = leastInt((int)selectionOrigin.X, (int)mouseState.X);
                    selectionRect.Y = leastInt((int)selectionOrigin.Y, (int)mouseState.Y);
                    selectionRect.Width = greaterInt((int)selectionOrigin.X, (int)mouseState.X) -
                        leastInt((int)selectionOrigin.X, (int)mouseState.X);
                    selectionRect.Height = greaterInt((int)selectionOrigin.Y, (int)mouseState.Y) -
                        leastInt((int)selectionOrigin.Y, (int)mouseState.Y);
                }
            }
            // Mouse left button up.
            if (mouseState.LeftButton == ButtonState.Released 
                && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                selectingState = false;
            }

            // Mouse scrolling
            if (mouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue)
            {
                Console.Out.WriteLine("Scroll wheel moved: " + mouseState.ScrollWheelValue);
                gameView.Zoom(mouseState.ScrollWheelValue / 100);
            }

            // Update the view
            this.gameView.Update();

            // save the mouse state for next cyle
            prevMouseState = mouseState;

            // update the base class
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself. This shouldn't be drawing anything.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
                
                /*
            // draw the tile board
            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    spriteBatch.Draw(tileTexture, new Vector2(i * 75, j * 75), Color.White);
                }
            }

            // Draw the immortal
            spriteBatch.Draw(immortalTexture, Vector2.Zero, Color.White);

            // Draw the selection box
            if (selectingState == true)
            {
                // draw the top
                spriteBatch.Draw(pixel, 
                    new Rectangle(selectionRect.X, selectionRect.Y, selectionRect.Width, 1), Color.LightGreen);  
                // draw the left
                spriteBatch.Draw(pixel,
                     new Rectangle(selectionRect.X + selectionRect.Width, selectionRect.Y, 1, selectionRect.Height), Color.LightGreen);
                // draw the bottom
                spriteBatch.Draw(pixel,
                     new Rectangle(selectionRect.X, selectionRect.Y + selectionRect.Height, selectionRect.Width, 1), Color.LightGreen);
                // draw the right
                spriteBatch.Draw(pixel,
                    new Rectangle(selectionRect.X, selectionRect.Y, 1, selectionRect.Height), Color.LightGreen);
                
            }
            //spriteBatch.Draw(selectedTexture, Vector2.Zero, Color.White);

            spriteBatch.End();*/

            base.Draw(gameTime);
        }
    }
}