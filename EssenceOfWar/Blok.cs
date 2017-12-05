using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EssenceOfWar
{
    public class Blok
    {
        private Texture2D _texture;
        public Vector2 Position { get; set; }

        public Blok(Texture2D Texture, Vector2 position)
        {
            _texture = Texture;
            Position = position;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, Position, Color.White);
        }

    }
}
