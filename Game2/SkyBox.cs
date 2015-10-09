using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Game2
{
    class SkyBox : BasicModel

    {
        Matrix scale = Matrix.CreateScale(2000f);

        Matrix skyBox;

        public SkyBox (Model model) : base (model)
        {

        }




        public override void Update(GameTime gameTime)
        {
            
            
            base.Update(gameTime);
        }

        public override void Draw(GraphicsDevice device, Camera camera)
        {

            skyBox = scale * Matrix.CreateTranslation((camera.cameraPosition) - new Vector3 (0,camera.cameraPosition.Y,0));
            device.SamplerStates[0] = SamplerState.LinearClamp;
            base.Draw(device, camera);
           
        }

        protected override Matrix Getworld()
        {
            return skyBox;
        }
    }
}
