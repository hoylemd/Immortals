using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Summary for a static model that does not move.</summary>
    class StaticModel : BasicModel
    {
        /// <summary>
        /// Constructor</summary>
        /// <param name="m">
        /// The model to use.</param>,
        /// <param name="Position">
        /// The position this model begins at.</param>
        public StaticModel(
            ModelManager modelManager, Model model, Volume boundingVolume, 
            Vector3 Position)
            : base(modelManager, model, boundingVolume)
        {

            //create the world
            this.world = Matrix.CreateTranslation(Position);
        }
    }
}
