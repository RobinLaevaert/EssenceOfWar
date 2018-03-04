using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport newView) {
            view = newView;
        }

        public void Update(GameTime gameTime, Hero2 Hero)
        {
          
            centre = new Vector2(Hero.Positie.X + (Hero.rectangle.Width / 2) -400 ,0);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0))* Matrix.CreateTranslation(new Vector3(-centre.X,-centre.Y,0));
        }


    }
}
