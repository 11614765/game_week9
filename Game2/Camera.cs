using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Game2
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {


        public Matrix view { get; protected set; }

        public Matrix projection { get; protected set; }

        //camera vectors 
        public Vector3 cameraPosition { get; protected set; }

        Vector3 cameraDirection; //relative direction which the camera is facing
        Vector3 cameraUp;
        float speed = 3f;

        MouseState prevMouseState;


        public Camera (Game game, Vector3 pos, Vector3 target, Vector3 up) :base(game)
        {
            //view = Matrix.CreateLookAt(pos, target, up);

            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, (float)game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1, 3000);
        }


            
        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }


        public override void Initialize()
        {
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);

            prevMouseState = Mouse.GetState();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {


            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle
                (Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 150) * (Mouse.GetState().Y - prevMouseState.Y)));





            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle
                (cameraUp, (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - prevMouseState.X)));




            prevMouseState = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraPosition += cameraDirection * speed;
                //cameraPosition = new Vector3(cameraPosition.X, 25, cameraPosition.Z);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraPosition -= cameraDirection * speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;
                //cameraPosition = new Vector3(cameraPosition.X, 25, cameraPosition.Z);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && cameraPosition.Y < 35  )
            {
                
                cameraPosition += Vector3.UnitY;
            }
            //else if (cameraPosition.Y>25  )
            //{

            //    cameraPosition -= Vector3.UnitY;
            //}
            
         
            
            CreateLookAt();
            base.Update(gameTime);
        }

    }
}
