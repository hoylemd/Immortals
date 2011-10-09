using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    /// <summary>
    /// Class to represent a game Board.
    /// </summary>
    public class Board
    {
        // Game engine pointers
        ImmortalsEngine engine;
        ModelManager modelManager;

        // Game object information
        public Point size { get; protected set; }


        // Display information
        Texture2D texture;
        VertexModel model;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engine">The game engine.</param>
        /// <param name="modelManager">The Model manager</param>
        /// <param name="size">
        /// A Point object describing the board size in DU.</param>
        /// <param name="texture">
        /// A Texture2D object holding the image to paint the board with.
        /// </param>
        public Board(
            ImmortalsEngine engine, ModelManager modelManager, Point size,
            Texture2D texture)
        {
            // Local variables
            VertexPositionTexture[] verts = new VertexPositionTexture[4];
            Vector2 offset = new Vector2((float)size.X / 2, (float)size.Y / 2);
            Console.WriteLine("Offset: " + offset.ToString());

            // Register pointers and objects
            this.engine = engine;
            this.modelManager = modelManager;
            this.size = size;
            this.texture = texture;

            // Calculate verts
            verts[0] = new VertexPositionTexture(
                new Vector3(-offset.X, offset.Y, 0), new Vector2(0, 0));
            verts[1] = new VertexPositionTexture(
                new Vector3(-offset.X, -offset.Y, 0), new Vector2(1, 0));
            verts[2] = new VertexPositionTexture(
                new Vector3(offset.X, offset.Y, 0), new Vector2(0, 1));
            verts[3] = new VertexPositionTexture(
                new Vector3(offset.X, -offset.Y, 0), new Vector2(1, 1));

            // Generate game model
            this.model = new VertexModel(
                modelManager, new BasicEffect(engine.GraphicsDevice), verts,
                texture, Vector3.Zero);

            // Register game model with modelManager
            modelManager.AddModel(this.model);
        }
    }
}
