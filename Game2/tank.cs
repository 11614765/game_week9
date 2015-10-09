using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Game2
{

    /// <summary>
    /// commit1212122121
    /// </summary>
    class Tank : BasicModel

    {

        Matrix translation = Matrix.Identity;
        Matrix rotation = Matrix.Identity;
        ModelBone turretBone;
        ModelBone[] wheels;
        MousePick mousePick;
        Vector3 position;
        Vector3 destination;
        double newOrientation = 0;
        double currentOrientation = 0;
        public Vector3 velocity = new Vector3(0,0,0);
        public float speed;
        float slowRadius = 1000;
        public float maxspeed = 1500;


        public Tank(Model model, GraphicsDevice device, Camera camera): base (model)

        {
            
            mousePick = new MousePick(device, camera);
            turretBone = model.Bones["turret_geo"];
            wheels = new ModelBone[4];
            wheels[0] = model.Bones["r_front_wheel_geo"];
            wheels[1] = model.Bones["r_back_wheel_geo"];
            wheels[2] = model.Bones["l_front_wheel_geo"];
            wheels[3] = model.Bones["l_back_wheel_geo"];

            speed = 10f;


        }

        
        public override void Update(GameTime gameTime)
        {
            Vector3? pickPosition;
            float time = (gameTime.ElapsedGameTime.Milliseconds) / 1000f;

            position = world.Translation;
          
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                pickPosition = mousePick.GetCollisionPosition();
                if (pickPosition.HasValue == true)
                {
                    destination = pickPosition.Value;
                    velocity = pickPosition.Value - position;
                    velocity.Normalize();

                    newOrientation = Math.Atan2(velocity.X,velocity.Z);

                   
                }
            }

            if (Math.Pow((newOrientation - currentOrientation),2) > MathHelper.PiOver4/45)
            {
                if (newOrientation > currentOrientation)
                {
                    rotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 100 * gameTime.ElapsedGameTime.Milliseconds / 10);
                 currentOrientation += MathHelper.PiOver4 / 100 * gameTime.ElapsedGameTime.Milliseconds / 10;
                 
                  
                }
             else
                 {
                     rotation *= Matrix.CreateRotationY(-MathHelper.PiOver4 / 100 * gameTime.ElapsedGameTime.Milliseconds / 10);
                  currentOrientation -= MathHelper.PiOver4 / 100 * gameTime.ElapsedGameTime.Milliseconds / 10;
                 }
            }
            else if (Math.Pow((destination.X - position.X ),2)>1f &&  Math.Pow((destination.Z - position.Z),2) >1f)
            {
                float distance = Vector3.Distance(destination, position);
                if (distance > slowRadius)
                {
                    speed = maxspeed;
                    position += maxspeed * velocity * time;
                    translation = Matrix.CreateTranslation(position);
                }
                else
                {
                    speed = maxspeed * distance / slowRadius;
                    position += speed * velocity * time;
                    translation = Matrix.CreateTranslation(position);

                }
                foreach (ModelBone wheel in wheels)
                {


                    wheel.Transform = Matrix.CreateRotationX(MathHelper.PiOver4 / 10) * wheel.Transform;

                }
            }



            turretBone.Transform *= Matrix.CreateRotationY(MathHelper.PiOver4/100);
     

            base.Update(gameTime);
        }

        public override void Draw(GraphicsDevice device, Camera camera)
        {
        
            base.Draw(device, camera);


        }

        protected override Matrix Getworld()
        {
            world = Matrix.CreateScale(0.3f) * rotation * translation;
           
            return world; 

        }

    }
}
