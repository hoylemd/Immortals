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
        public StaticModel(Model m, Vector3 Position)
            : base(m)
        {
            //create the world
            this.world = Matrix.CreateTranslation(Position);
        }

        /// <summary>
        /// Update the model
        /// </summary>
        public override void Update()
        {
            // Do nothing
        }

    }
}
