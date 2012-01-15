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

///////////////////////////////////////////////////////////////////////////////
namespace Immortals
{
    /// <summary>Manager class to handle models.</summary>
    public class ModelContainer : Container
    {
        // Model list
        List<BasicModel> models = new List<BasicModel>();

        // Camera pointer
        protected Camera camera;

        // Graphics Device pointer
        public GraphicsDevice graphicsDevice { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="area">
        /// The area this view occupies in the parent container</param>
        /// <param name="camera">The camera to use for drawing this view</param>
        public ModelContainer(Rectangle area, Camera camera)
            :base(area)
        {
            // save pointers
            this.camera = camera;

        }

        /// <summary>
        /// Function to add a model to be managed
        /// </summary>
        /// <param name="model"> The model to add.</param>
        public void addModel(BasicModel model)
        {
            this.models.Add(model);
        }

        /// <summary>
        /// Function to remove a model from the management list
        /// </summary>
        /// <param name="model"> The model to remove</param>
        public void removeModel(BasicModel model)
        {
            this.models.Remove(model);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to
        /// before starting to run.  This is where it can query for any 
        /// required services and load content.</summary>
        public void Initialize(GraphicsDevice gd)
        {
            this.graphicsDevice = gd;
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
            // Set suitable renderstates for drawing a 3D model
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            
            // loop through and draw each model
            foreach (BasicModel bm in this.models)
                bm.Draw(camera);
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
