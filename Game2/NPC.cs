using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    /// <summary>
    /// meaningless commit
    /// </summary>
    class NPC : BasicModel
    {
        Matrix rotation = Matrix.Identity;
        Vector3 direction;
        Vector3 tankEnemyPosition { get; set; }

        Tank tank1;
 
        Vector3 enemyPursueMove;

        public NPC(Model model, Vector3 Position, Tank tank1) : base(model)
        {
           
            world = Matrix.CreateTranslation(Position);
            this.tank1 = tank1;

        }
        public override void Update(GameTime gameTime)
        {
            Matrix collisionworld = Matrix.Identity;
            collisionworld *= tank1.world;

            if (this.CollidesWith(tank1.model, collisionworld))
            {

            }
            else
            {
                float distanceTotank1Position;
                float speed = 2;
                tankEnemyPosition = world.Translation;
                direction = tank1.world.Translation - tankEnemyPosition;
                direction.Normalize();
                Vector3 tankVelocity = speed * direction;
                distanceTotank1Position = Vector3.Distance(tank1.world.Translation, tankEnemyPosition);
                float timeTotank1Position = distanceTotank1Position / speed;
                Vector3 target = tank1.world.Translation;
                Vector3 targeDirection = target - tankEnemyPosition;
                targeDirection.Normalize();
                enemyPursueMove = targeDirection * speed;
                world *= Matrix.CreateTranslation(enemyPursueMove);
            }
        }
        protected override Matrix Getworld()
        {
           
            return rotation * Matrix.CreateScale(.2f) * world; 
        }
    }
}
