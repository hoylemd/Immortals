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
    /// <summary>Manager class to handle sprites</summary>
    public class SpriteManager
    {
        // SpriteBatch for drawing
        SpriteBatch spriteBatch;

        // List of sprites to draw
        List<Sprite> spriteList;

        // Sidebar pointer
        Sidebar sidebar;

        // Gameview pointer
        GameView gameView;

        // Engine pointer
        ImmortalsEngine engine;

        /// <summary>Constuctor.</summary>
        /// <param name="game">The top-level Game object.</param>
        /// <param name="gv">The GameView manager.</param>
        public SpriteManager(ImmortalsEngine game, GameView gv)
        {
            // register parentd
            this.gameView = gv;
            this.engine = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs 
        /// to before starting to run.  This is where it can query for any 
        /// required services and load content.</summary>
        public void Initialize()
        {
            // Set up the SpriteBatch.
            this.spriteBatch = new SpriteBatch(gameView.GraphicsDevice);

            // Set up the sprite list
            spriteList = new List<Sprite>();

        }

        /// <summary>Allows the game component to load content.</summary>
        public void LoadContent()
        {
            // Make the sidebar
            MakeSidebar(
                engine.Content.Load<Texture2D>(@"Images/sidebarPurpleFrame"),
                new Point(300, 800), new Point (0, 0));
        }

        /// <summary>Allows the game component to update itself.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Call each sprite's update
            foreach (Sprite sprite in spriteList)
            {
                sprite.Update(gameTime);
            }
        }

        /// <summary>Draws all the sprites in the list.</summary>
        /// <param name="gameTime">
        /// Provides a nampshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the sidebar
            if (sidebar != null)
                sidebar.Draw(spriteBatch);

            // Draw the sprites
            foreach (Sprite sprite in spriteList)
                sprite.Draw(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Function to make and register the sidebar.</summary>
        /// <param name="background">
        /// The background image of the sidebar.</param>
        /// <param name="size"> 
        /// The size of the background image in pixels.</param>
        /// <param name="rectangle"> 
        /// The rectangle represeting the client window.</param>
        public void MakeSidebar(
            Texture2D background, Point size, Point location)
        {
            sidebar = new Sidebar(background, size, Point.Zero, 0, location);
        }

        /// <summary>
        /// Function to register a sprite with this spriteManager.</summary>
        /// <param name="newSprite">The sprite to register</param>
        public void AddSprite(Sprite newSprite)
        {
            this.spriteList.Add(newSprite);
        }
    }
}
