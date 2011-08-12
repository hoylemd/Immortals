using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Class to hold information about a basic model</summary>
    class BasicModel
    {
        // Model drawing information
        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;

        /// <summary>
        /// Constructor</summary>
        /// <param name="m"> The model object to use</param>
        public BasicModel(Model m)
        {
            // Save data
            this.model = m;
        }

        /// <summary>
        /// Overrideable accessor for world</summary>
        /// <returns> 
        /// this model's World matrix</returns>
        public virtual Matrix GetWorld()
        {
            // return the world variable
            return this.world;
        }

        /// <summary>
        /// Empty Update function for overriding.</summary>
        public virtual void Update() 
        { 
        }

        /// <summary>
        /// Function to draw this model.</summary>
        /// <param name="camera"> 
        /// The camera to draw this model to.</param>
        public void Draw(Camera camera)
        {
            // grab the list of bone transformations
            Matrix[] transforms = new Matrix[model.Bones.Count];
            this.model.CopyAbsoluteBoneTransformsTo(transforms);

            // draw each mesh
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                // draw set up each effect
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();
                    be.Projection = camera.projection;
                    be.View = camera.view;
                    be.World = GetWorld() * mesh.ParentBone.Transform;
                }

                // draw the mesh
                mesh.Draw();
            }
        }

        /// <summary>
        /// Function to determine if two models have collided.</summary>
        /// <param name="otherModel"> 
        /// The other Model to check</param>
        /// <param name="otherWorld"> 
        /// The other model's world transformation</param>
        /// <returns>
        /// True is these models have collided. False if not.</returns>
        public bool CollidesWith(Model otherModel, Matrix otherWorld)
        {
            // Loop through each modelMesh in both objects and compare
            // all bounding spheres for collisions
            foreach (ModelMesh myModelMeshes in this.model.Meshes)
            {
                foreach (ModelMesh hisModelMeshes in otherModel.Meshes)
                {
                    if (
                        myModelMeshes.BoundingSphere.Transform( 
                            GetWorld()).Intersects(
                                hisModelMeshes.BoundingSphere.Transform(
                                    otherWorld)))
                        return true;
                }
            }
            return false;
        }

    }
}
