using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    class BasicModel
    {
        public Model model { get; protected set; }   //Represent 3D model in memory

        public Matrix world = Matrix.Identity; 
        public BasicModel(Model model)
        {
            this.model = model;
        }


        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GraphicsDevice device, Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];

            model.CopyAbsoluteBoneTransformsTo(transforms);
            

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                   
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.TextureEnabled = true;
                    effect.Alpha = 1;
                    
                    //effect.World = mesh.ParentBone.Transform*Getworld();
                    effect.World = transforms[mesh.ParentBone.Index] * Getworld();
                    
                    

                }
                mesh.Draw();
            }
        }
        protected virtual Matrix Getworld()
        {

            return world;
        }
        public bool CollidesWith(Model othermodel, Matrix otherWorld)
        {
            foreach (ModelMesh myModelMeshes in model.Meshes)
            {
                foreach (ModelMesh hisModelMeshes in othermodel.Meshes)
                {
                    if (myModelMeshes.BoundingSphere.Transform(Getworld()).Intersects(hisModelMeshes.BoundingSphere.Transform(otherWorld)))

                        return true;
                }
            }
            return false;
        }
    }
    
    
}
