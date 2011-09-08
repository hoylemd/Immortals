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
    /// <summary>This is a game component that represents a camera.</summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        // view and projection matricies
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }

        // Rectangle to be drawn to
        Rectangle drawRectangle;

        // Camera Vectors
        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;
        Vector3 cameraStrafe;

        // Orbit angles
        // float tau = 6.2918f;
        Vector3 orbit;

        // Mouse state
        MouseState prevMouseState;

        // Game View
        GameView gameView;

        /// <summary>Constructor</summary>
        /// <param name="game">
        /// Game object.  The top-level game entity.</param>
        /// <param name="gv">The game view manager.</param>
        /// <param name="pos">
        /// Matrix representing the position of the camera.</param>
        /// <param name="target">
        /// Matrix representing the position the camera is looking at.</param>
        /// <param name="up">
        /// Matrix representing the orientation of the camera.</param>
        public Camera(
            Game game, GameView gv, Vector3 pos, Vector3 target, Vector3 up,
            Rectangle drawRectangle)
            : base(game)
        {
            // Store data
            this.cameraPosition = pos;
            this.cameraUp = up;
            this.gameView = gv;
            this.drawRectangle = drawRectangle;

            // Normalize and calculate direction vectors
            this.cameraDirection = target - pos;
            this.cameraDirection.Normalize();
            this.cameraStrafe = Vector3.Cross(up, this.cameraDirection);

            // Build camera view matrix
            this.CreateLookAt();

            // build the projection
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)drawRectangle.Width /
                (float)drawRectangle.Height,
                1, 3000);

            // set up the orbit
            this.orbit = new Vector3(0, 0, 0);
        }

        /// <summary>Accessor for camera's direction</summary>
        public Vector3 GetCameraDirection
        {
            get { return cameraDirection; }
        }

        /// <summary>
        /// Function to move a camera.
        /// </summary>
        /// <param name="position">
        /// The XYZ coordinates to move the camera to.</param>
        public void MoveCamera(Vector3 position)
        {
            this.cameraPosition = position;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs 
        /// to before starting to run.  This is where it can query for any 
        /// required services and load content.</summary>
        public override void Initialize()
        {
            // get the initial mouse state
            prevMouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>Allows the game component to update itself.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Reconstruct the view matrix
            CreateLookAt();

            // Recalculate the strafe vector
            cameraStrafe = Vector3.Cross(
                cameraUp, cameraDirection);

            base.Update(gameTime);
        }

        /// <summary>
        /// Function to calculate this camera's view matrix.
        /// </summary>
        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(this.cameraPosition,
                this.cameraPosition + this.cameraDirection,
                this.cameraUp);
        }
    }
}
