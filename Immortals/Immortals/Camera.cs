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
        Vector3 cameraAngle;
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
        /// <param name="angle">
        /// Matrix representing the angle of the camera. This also sets the 
        /// initial zoom level of the camera.</param>
        /// <param name="target">
        /// Matrix representing the position the camera is looking at.</param>
        /// <param name="up">
        /// Matrix representing the orientation of the camera.</param>
        public Camera(
            Game game, GameView gv, Vector3 angle, Vector3 target, Vector3 up,
            Rectangle drawRectangle)
            : base(game)
        {
            // Store data
            this.cameraPosition = new Vector3(0, 0, angle.Z);
            this.cameraAngle = new Vector3(angle.X, angle.Y, 0);
            this.cameraUp = up;
            this.gameView = gv;
            this.drawRectangle = drawRectangle;

            // Normalize and calculate direction vectors
            this.cameraDirection = target - angle;
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
            cameraPosition = position;
        }


        /// <summary>
        /// Function to zoom a camera towards or away from it's target
        /// </summary>
        /// <param name="distance">
        /// The distance to move towards or away from the target.</param>
        public void ZoomCamera(float distance)
        {
            cameraPosition = cameraPosition + (cameraDirection * distance);
        }

        /// <summary>
        /// Function to slide a camera on the XY plane
        /// </summary>
        /// <param name="displacement">
        /// The displacement on the XY plane to apply.</param>
        public void SlideCamera(Vector2 displacement)
        {
            Console.WriteLine("sliding from " + cameraPosition.ToString() + " to " + displacement.ToString());
            cameraPosition = new Vector3(
                cameraPosition.X + displacement.X,
                cameraPosition.Y + displacement.Y,
                cameraPosition.Z);
            /*cameraDirection = new Vector3(
                cameraDirection.X + displacement.X,
                cameraDirection.Y + displacement.Y,
                cameraDirection.Z);*/
            Console.WriteLine("Slid to " + cameraPosition.ToString());
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
            view = Matrix.CreateLookAt(cameraPosition + cameraAngle,
                cameraPosition + cameraAngle + cameraDirection,
                cameraUp);
        }
    }
}
