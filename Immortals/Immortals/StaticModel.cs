using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Summary for a model that rotates
    /// </summary>
    class StaticModel : BasicModel
    {
        // Rotation matrix
        Matrix rotation = Matrix.Identity;

        // Rotation angles
        float yawAngle = 0;
        float pitchAngle = 0;
        float rollAngle = 0;
        Vector3 direction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m">The model to use</param>
        public StaticModel(Model m, Vector3 Position,
            Vector3 direction, float yaw, float pitch, float roll)
            : base(m)
        {
            //Console.Out.WriteLine("make at" + Position);

            this.world = Matrix.CreateTranslation(Position);
            this.yawAngle = yaw;
            this.pitchAngle = pitch;
            this.rollAngle = roll;
            this.direction = direction;
        }

        /// <summary>
        /// Update the model by rotating it
        /// </summary>
        public override void Update()
        {
            // Rotate model
            this.rotation *= Matrix.CreateFromYawPitchRoll(this.yawAngle,
                this.pitchAngle, this.rollAngle);

            // Move model
            world *= Matrix.CreateTranslation(direction);
        }

        /// <summary>
        /// overridden accessor for world
        /// </summary>
        /// <returns>world matrix with rotation applied</returns>
        public override Matrix GetWorld()
        {
            return this.rotation * this.world;
        }

    }
}
