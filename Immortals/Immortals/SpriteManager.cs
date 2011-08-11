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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // SpriteBatch for drawing
        SpriteBatch spriteBatch;

        // List of sprites to draw
        List<Sprite> spriteList = new List<Sprite>();


        // Board pointer
        Board board;

        // Map Pointer
        Sprite map;
        Boolean useMap = false;
        Rectangle boardView;

        // Sidebar pointer
        Sidebar sidebar;

        // Gameview pointer
        GameView gameView;

        public SpriteManager(Game game, GameView gv)
            : base(game)
        {
            // register parent
            this.gameView = gv;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            this.spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to load content
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in spriteList)
            {
                sprite.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the game component to draw itself
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {

            /*base.Draw(gameTime);

            // Draw Stuff
            spriteBatch.Begin();

            // Draw the Board
            if (useMap)
            {
                if (map != null)
                    map.Draw(this.spriteBatch, this.boardView);
            }
            else
            {
                if (board != null)
                    board.Draw(this.spriteBatch, Vector2.Zero, Game.Window.ClientBounds);
            }

            // Draw the sidebar
            if (sidebar != null)
                sidebar.Draw(spriteBatch);

            // Draw the sprites
            foreach (Sprite sprite in spriteList)
                sprite.Draw(spriteBatch);

            spriteBatch.End();*/

        }


        /// <summary>
        /// Function to set up a gameboard
        /// </summary>
        /// <param name="boardSize"> Point object containing the board dimensions in tiles.</param>
        /// <param name="tile"> Sprite to use for the tiles.</param>
        public void GenerateBoard(Point boardSize, Sprite tile)
        {
            this.board = new Board(boardSize, tile);
            this.useMap = false;
        }

        /// <summary>
        /// Function to register a map to the spritemanager
        /// </summary>
        /// <param name="mapSize"> Point object containing the map dimensions in pixels.</param>
        /// <param name="map"> Sprite to use for the map. </param>
        public void RegisterMap(Point mapSize, Sprite map)
        {
            this.map = map;
            this.useMap = true;
        }

        /// <summary>
        /// Function to make and register the sidebar.
        /// </summary>
        /// <param name="background"> The background image of the sidebar.</param>
        /// <param name="size"> The size of the background image in pixels.</param>
        /// <param name="clientBounds"> The rectangle represeting the client window.</param>
        public void MakeSidebar(Texture2D background, Point size, Rectangle clientBounds)
        {
            sidebar = new Sidebar(background, size, clientBounds);
        }

        /// <summary>
        /// Function to register a sprite with this spriteManager
        /// </summary>
        /// <param name="newSprite">The sprite to register</param>
        /// <returns></returns>
        public Sprite AddSprite(Sprite newSprite)
        {
            this.spriteList.Add(newSprite);
            return newSprite;
        }

        public void SetBoardView(Rectangle boardView)
        {
            this.boardView = boardView;
        }
    }
}
