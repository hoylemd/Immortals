using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Immortals
{
    class VertexModel : BasicModel
    {
        // Vertex buffers
        VertexPositionTexture[] verts;
        VertexBuffer vertexBuffer;

        // Texture
        Texture2D texture;

        // Effect
        BasicEffect effect;

        /// <summary>Constructor with vert list</summary>
        /// <param name="effect">The effect to use to draw the model.</param>
        /// <param name="verts">The list of verts to use.</param>
        /// <param name="texture">The texture to use for the model.</param>
        public VertexModel(
            ModelManager modelManager, BasicEffect effect, VertexPositionTexture[] verts, Texture2D texture,
            Vector3 position)
            :base(modelManager)
        {
            // Save data
            this.verts = verts;
            this.texture = texture;
            this.effect = effect;

            // Create the world matrix
            this.world = Matrix.CreateTranslation(position);

            // Initialize vertices
            this.verts = new VertexPositionTexture[4];
            this.verts[0] = new VertexPositionTexture(
                new Vector3(-1, 1, 0), new Vector2(0, 0));
            this.verts[1] = new VertexPositionTexture(
                new Vector3(1, 1, 0), new Vector2(1, 0));
            this.verts[2] = new VertexPositionTexture(
                new Vector3(-1, -1, 0), new Vector2(0, 1));
            this.verts[3] = new VertexPositionTexture(
                new Vector3(1, -1, 0), new Vector2(1, 1));

            // Set vertex data in VertexBuffer
            vertexBuffer = new VertexBuffer(
                modelManager.graphicsDevice, typeof(VertexPositionTexture), 
                verts.Length, BufferUsage.None);
            vertexBuffer.SetData(verts);

        }
        
        /// <summary>Function to draw this model</summary>
        /// <param name="camera">The camera to draw this model to</param>
        public override void Draw(Camera camera)
        {
            modelManager.graphicsDevice.SetVertexBuffer(vertexBuffer);

            //Set object and camera info
            effect.World = world;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            effect.Texture = texture;
            effect.TextureEnabled = true;

            // Begin effect and draw for each pass
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                modelManager.graphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                    (PrimitiveType.TriangleStrip, verts, 0, 2);
            }
        }
    }
}
