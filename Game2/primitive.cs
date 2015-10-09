using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    class Primitive : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        VertexPositionNormalTexture[] verts;
        short[] indices;
        VertexBuffer vertexBuffer;
        IndexBuffer indexbuffer;
        BasicEffect effect;
        Texture2D texture;
        Matrix translation = Matrix.CreateTranslation(0,0,0); 
        Matrix rotation = Matrix.Identity;
        Matrix scale = Matrix.CreateScale(0f);
        Camera camera;
    
        MousePick pick;

        public Primitive (Game game, Camera camera, GraphicsDevice device) :base(game)
            {
            this.camera = camera;
            pick = new MousePick(device, camera);


            }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //texture 
            Vector2 patternA_topleft = new Vector2(0, 0);
            Vector2 patternA_topright = new Vector2(0.5f, 0);
            Vector2 patternA_bottomright = new Vector2(0.5f, 0.5f);
            Vector2 patternA_bottomleft = new Vector2(0, 0.5f);

            Vector2 patternB_topleft = new Vector2(0.5f, 0);
            Vector2 patternB_topright = new Vector2(1, 0);
            Vector2 patternB_bottomright = new Vector2(1, 0.5f);
            Vector2 patternB_bottomleft = new Vector2(0.5f, 0.5f);

            Vector2 patternC_topleft = new Vector2(0, 0.5f);
            Vector2 patternC_topright = new Vector2(0.5f, 0.5f);
            Vector2 patternC_bottomright = new Vector2(0.5f, 1);
            Vector2 patternC_bottomleft = new Vector2(0, 1);



            //normal
            Vector3 frontNormal = new Vector3(0, 0, 1);
            Vector3 leftSideNormal = new Vector3(-1, 0, 0);
            Vector3 rightSideNormal = new Vector3(1, 0, 0);
            Vector3 backNormal = new Vector3(0, 0, -1);
            Vector3 topNormal = new Vector3(0, 1, 0);
            Vector3 bottomNormal = new Vector3(0, -1, 0);



            verts = new VertexPositionNormalTexture[24];
            //Front
            verts[0] = new VertexPositionNormalTexture(new Vector3(-1, 1, 1), frontNormal, patternA_topleft);
            verts[1] = new VertexPositionNormalTexture(new Vector3(1, 1, 1), frontNormal, patternA_topright);
            verts[2] = new VertexPositionNormalTexture(new Vector3(1, -1, 1), frontNormal, patternA_bottomright);
            verts[3] = new VertexPositionNormalTexture(new Vector3(-1, -1, 1), frontNormal, patternA_bottomleft);
            //Left
            verts[4] = new VertexPositionNormalTexture(new Vector3(-1, 1, -1), leftSideNormal, patternB_topleft);
            verts[5] = new VertexPositionNormalTexture(new Vector3(-1, 1, 1), leftSideNormal, patternB_topright);
            verts[6] = new VertexPositionNormalTexture(new Vector3(-1, -1, 1), leftSideNormal, patternB_bottomright);
            verts[7] = new VertexPositionNormalTexture(new Vector3(-1, -1, -1), leftSideNormal, patternB_bottomleft);
            //Back
            verts[8] = new VertexPositionNormalTexture(new Vector3(1, 1, -1), backNormal, patternA_topleft);
            verts[9] = new VertexPositionNormalTexture(new Vector3(-1, 1, -1), backNormal, patternA_topright);
            verts[10] = new VertexPositionNormalTexture(new Vector3(-1, -1, -1), backNormal, patternA_bottomright);
            verts[11] = new VertexPositionNormalTexture(new Vector3(1, -1, -1), backNormal, patternA_bottomleft);
            //Right
            verts[12] = new VertexPositionNormalTexture(new Vector3(1, 1, 1), rightSideNormal, patternB_bottomleft);
            verts[13] = new VertexPositionNormalTexture(new Vector3(1, 1, -1), rightSideNormal, patternB_topleft);
            verts[14] = new VertexPositionNormalTexture(new Vector3(1, -1, -1), rightSideNormal, patternB_topright);
            verts[15] = new VertexPositionNormalTexture(new Vector3(1, -1, 1), rightSideNormal, patternB_bottomright);


            //Top
            verts[16] = new VertexPositionNormalTexture(new Vector3(-1, 1, -1), topNormal, patternC_bottomleft);
            verts[17] = new VertexPositionNormalTexture(new Vector3(1, 1, -1), topNormal, patternC_topleft);
            verts[18] = new VertexPositionNormalTexture(new Vector3(1, 1, 1), topNormal, patternC_topright);
            verts[19] = new VertexPositionNormalTexture(new Vector3(-1, 1, 1), topNormal, patternC_bottomright);

            //Bottom
            verts[20] = new VertexPositionNormalTexture(new Vector3(-1, -1, 1), bottomNormal, patternC_bottomleft);
            verts[21] = new VertexPositionNormalTexture(new Vector3(1, -1, 1), bottomNormal, patternC_topleft);
            verts[22] = new VertexPositionNormalTexture(new Vector3(1, -1, -1), bottomNormal, patternC_topright);
            verts[23] = new VertexPositionNormalTexture(new Vector3(-1, -1, -1), bottomNormal, patternC_bottomright);



            indices = new short[36];

            for (short i = 0; i < 6; i++)
            {
                indices[0 + i * 6] = (short)(0 + i * 4);
                indices[1 + i * 6] = (short)(1 + i * 4);
                indices[2 + i * 6] = (short)(2 + i * 4);
                indices[3 + i * 6] = (short)(0 + i * 4);
                indices[4 + i * 6] = (short)(2 + i * 4);
                indices[5 + i * 6] = (short)(3 + i * 4);

            }




            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), verts.Length, BufferUsage.None);
            vertexBuffer.SetData(verts);

            indexbuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, sizeof(short) * indices.Length, BufferUsage.None);
            indexbuffer.SetData(indices);


            effect = new BasicEffect(GraphicsDevice);
            GraphicsDevice.Indices = indexbuffer;
            texture = Game.Content.Load<Texture2D>(@"Textures/crate");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Vector3? pickPoint;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                pickPoint = pick.GetCollisionPosition();
                if (pickPoint.HasValue)
                {

                    translation = Matrix.CreateTranslation(pickPoint.Value.X, pickPoint.Value.Y+20, pickPoint.Value.Z);
                    scale = Matrix.CreateScale(20f);
                }
                else
                {
                    scale = Matrix.CreateScale(0);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //effect.World = Matrix.Identity;
            effect.World = scale * rotation * translation;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            effect.VertexColorEnabled = false;
            effect.Texture = texture;
            effect.TextureEnabled = true;

            effect.EnableDefaultLighting();
            effect.LightingEnabled = true;


            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();


                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 36, 0, 24);


            }
            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
