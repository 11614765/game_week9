using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game2
{
    class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent

    {
        List<BasicModel> models = new List<BasicModel>();
        Tank tank;
        public ModelManager (Game game) :base (game)

        {
            
        }

        public override void Initialize()
        {
            tank = new Tank
                (Game.Content.Load<Model>(@"Tank/tank"),
                ((Game1)Game).device,
                ((Game1)Game).camera);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            models.Add(new Ground
                (Game.Content.Load<Model>(@"Ground/Ground")));
            models.Add(new SkyBox
                (Game.Content.Load<Model>(@"SkyBox/skybox")));
            models.Add(tank);
            models.Add(new NPC
                ((Game.Content.Load<Model>(@"Tank/tank")), new Vector3(-500, 0, -1700), tank));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            foreach (BasicModel model in models)
            {
                model.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel model in models)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
                
            }
            base.Draw(gameTime);
        }
    }
}
