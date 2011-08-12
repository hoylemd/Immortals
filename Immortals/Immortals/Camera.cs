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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Immortals
{
    /// <summary>
    /// This is a game component that represents a camera 
    /// </summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        // view and projection matricies
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }

        // Camera Vectors
        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;
        Vector3 cameraStrafe;

        // Max yaw/pitch variables
        float totalYaw = MathHelper.PiOver4 / 2;
        float currentYaw = 0;
        float totalPitch = MathHelper.PiOver4 / 2;
        float currentPitch = 0;

        // Mouse state
        MouseState prevMouseState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"> Game object.  The top-level game entity.</param>
        /// <param name="pos"> Matrix representing the position of the camera.</param>
        /// <param name="target"> Matrix representing the position the camera is looking at.</param>
        /// <param name="up"> Matrix representing the orientation of the camera.</param>
        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            // Build camera view matrix
            this.cameraPosition = pos;
            this.cameraDirection = target - pos;
            this.cameraDirection.Normalize();
            this.cameraUp = up;
            this.cameraStrafe = Vector3.Cross(this.cameraUp, this.cameraDirection);
            this.CreateLookAt();

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)Game.Window.ClientBounds.Width /
                (float)Game.Window.ClientBounds.Height,
                1, 3000);
        }

        /// <summary>
        /// Accessor for camera's direction
        /// </summary>
        public Vector3 GetCameraDirection
        {
            get { return cameraDirection; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // Set mouse position and do initial get state
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2,
                Game.Window.ClientBounds.Height / 2);
            this.prevMouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Poll input
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            // local angles
            float yawAngle;
            float pitchAngle;

            // Yaw
            /* yawAngle = (-MathHelper.PiOver4 / 150) *
                (mouse.X - this.prevMouseState.X);

            if (Math.Abs(this.currentYaw + yawAngle) < this.totalYaw)
            {
                this.cameraDirection = Vector3.Transform(this.cameraDirection,
                    Matrix.CreateFromAxisAngle(this.cameraUp, yawAngle));
                this.currentYaw += yawAngle;
            }

            // Pitch
            pitchAngle = (-MathHelper.PiOver4 / 150) *
                (mouse.Y - this.prevMouseState.Y);

            if (Math.Abs(this.currentPitch + pitchAngle) < this.totalPitch)
            {
                this.cameraDirection = Vector3.Transform(this.cameraDirection,
                    Matrix.CreateFromAxisAngle(this.cameraStrafe, pitchAngle));
                this.currentPitch += pitchAngle;
            }*/


            // Reset prevMouseState
            this.prevMouseState = mouse;

            // Reconstruct the view matrix
            CreateLookAt();

            // Recalculate the strafe vector
            this.cameraStrafe = Vector3.Cross(this.cameraUp, this.cameraDirection);

            base.Update(gameTime);
        }

        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(this.cameraPosition,
                this.cameraPosition + this.cameraDirection,
                this.cameraUp);
        }
    }
}
