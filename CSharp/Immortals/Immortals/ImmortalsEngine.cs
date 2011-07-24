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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;

        // Texture variables
        Texture2D immortalTexture;
        Texture2D tileTexture;
        Texture2D selectedTexture;
        Texture2D pixel;
        Texture2D rotatingThing;

        // Mouse Variables
        MouseState prevMouseState;

        // Game state variables
        Boolean selectingState;
        Rectangle selectionRect;
        Vector2 selectionOrigin;

        // Sprite Pointers
        Sprite thing;

        // get the least of 2 ints
        public int leastInt(int a, int b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }

        // get the greater of 2 ints
        public int greaterInt(int a, int b)
        {
            if (a >= b)
                return a;
            else
                return b;
        }

        public ImmortalsEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 1250;
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
            this.IsMouseVisible = true;

            selectingState = false;

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load the sprite textures
            immortalTexture = Content.Load<Texture2D>(@"Images/immortalSmall");
            tileTexture = Content.Load<Texture2D>(@"Images/tile");
            selectedTexture = Content.Load<Texture2D>(@"Images/selected");
            rotatingThing = Content.Load<Texture2D>(@"Images/rotatingThing");

            // Make a sprite
            thing = new Sprite(rotatingThing, new Point(75, 75), new Point(3, 4), 16, new Point(10,10));
            spriteManager.AddSprite(thing);

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
            
            // Poll dat mouse
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
                    selectionRect.Width = greaterInt((int)selectionOrigin.X, (int)mouseState.X)
                        - leastInt((int)selectionOrigin.X, (int)mouseState.X);
                    selectionRect.Height = greaterInt((int)selectionOrigin.Y, (int)mouseState.Y)
                        - leastInt((int)selectionOrigin.Y, (int)mouseState.Y);
                }
            }
            // Mouse left button up.
            if (mouseState.LeftButton == ButtonState.Released 
                && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                selectingState = false;
            }

            prevMouseState = mouseState;

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            /*int i, j;

            // Draw the sprites
            spriteBatch.Begin();

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