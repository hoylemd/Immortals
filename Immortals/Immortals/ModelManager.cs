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
    public class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // Model list
        List<BasicModel> models = new List<BasicModel>();

        // GameView pointer
        GameView gameView;

        // Random number generator
        Random rnd;

        public ModelManager(ImmortalsEngine game, GameView gv)
            : base(game)
        {
            // register parent
            this.gameView = gv;

            // get the randomizer
            this.rnd = game.rnd;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to
        /// before starting to run.  This is where it can query for any 
        /// required services and load content.</summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>Allows the module to load it's content.</summary>
        protected override void LoadContent()
        {
            // Load up the test model
            models.Add(new StaticModel(
                Game.Content.Load<Model>(@"Models/ammo"), Vector3.Zero));

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself. Updates all 
        /// models.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update models
            this.UpdateModels();

            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the component to draw itself. Draws each model.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            // loop through and draw each model
            foreach (BasicModel bm in this.models)
                bm.Draw(gameView.mainCamera);

            base.Draw(gameTime);
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
