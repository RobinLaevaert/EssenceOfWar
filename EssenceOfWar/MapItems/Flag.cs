using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar.MapItems
{
    class FlagRobin : Tile
    {
        Animation animatie;
        Texture2D _texture;

        public FlagRobin(int _id, Rectangle newRectangle)
        {
            this.id = _id;
            animatie = new Animation();
            animatie.AddFrame(new Rectangle(0, 0, 70, 70));
            animatie.AddFrame(new Rectangle(70, 0, 70, 70));
            animatie.AantalBewegingenPerSeconde = 10;
            _texture = Content.Load<Texture2D>("Tile24");
            Rectangle = newRectangle;

             
        }

        public void Update(GameTime gameTime)
        {
            animatie.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Rectangle,animatie.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
