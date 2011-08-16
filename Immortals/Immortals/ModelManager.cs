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
    /// <summary>Manager class to handle models.</summary>
    public class ModelManager
    {
        // Model list
        List<BasicModel> models = new List<BasicModel>();

        // GameView pointer
        GameView gameView;

        // Random number generator
        Random rnd;

        // Engine pointer
        ImmortalsEngine engine;

        // Graphics Device pointer
        public GraphicsDevice graphicsDevice { get; private set; }

        public ModelManager(ImmortalsEngine game, GameView gv)
        {
            // register parents
            this.gameView = gv;
            this.engine = game;

            // get the randomizer
            this.rnd = game.rnd;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to
        /// before starting to run.  This is where it can query for any 
        /// required services and load content.</summary>
        public void Initialize()
        {
            this.graphicsDevice = engine.GraphicsDevice;
        }

        /// <summary>Allows the module to load it's content.</summary>
        public void LoadContent()
        {
            // Load up the test model
            models.Add(new StaticModel(this, 
                engine.Content.Load<Model>(@"Models/ammo"), Vector3.Zero));

            // Generate some terrain

        }

        /// <summary>
        /// Allows the game component to update itself. Updates all 
        /// models.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Update models
            this.UpdateModels();
        }

        /// <summary>
        /// Allows the component to draw itself. Draws each model.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            // loop through and draw each model
            foreach (BasicModel bm in this.models)
                bm.Draw(gameView.mainCamera);
        }

        /// <summary>
        /// Function to iterate through all models and update them.</summary>
        protected void UpdateModels()
        {
            // loop through all models and call Update
            for (int i = 0; i < this.models.Count; i++)
            {
                // Update each model
                this.models[i].Update();
            }
        }
    }
}
