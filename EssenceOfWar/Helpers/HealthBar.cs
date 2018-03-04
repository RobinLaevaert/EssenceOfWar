using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class HealthBar
    {
        
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        public Rectangle current;
        public HealthBar(Texture2D newTexture, Vector2 newPosition, Rectangle newRectangle)
        {
            texture = newTexture;
            position = newPosition;
            rectangle = newRectangle;
            current = newRectangle;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, current, Color.White);
        }

       
    }
}
