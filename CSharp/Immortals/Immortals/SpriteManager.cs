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
        SpriteBatch spriteBatch;
        List<Sprite> spriteList = new List<Sprite>();

        // special sprite pointers

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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
           /*spritename = new Sprite(Game.Content.Load<Texture2D>(@"<asset path>"),
                    new Point(h, w),  // frame size
                    new Point(a, d),  // size of frame matrix
                    16,               // ms per frame
                    5,
                    new Point(10, 10));
            spriteList.Add(ring);*/
        
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
            GraphicsDevice.Clear(Color.Purple);

            // Draw Sprites
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            // Draw the sprite s
            foreach (Sprite sprite in spriteList)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddSprite(Sprite newSprite)
        {

        }
    }
}
