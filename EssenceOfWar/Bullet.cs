using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class Bullet
    {
        Texture2D _Texture;
        private Vector2 Position;
        private Vector2 Origin;
        private float Direction;
        private float linearVelocity = 4f;
        private float lifespan = 50f;

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        public bool IsRemoved = false;

        private float _timer;
        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Bullet(float newDirection, Vector2 newPosition, float newLinearVelocity, Rectangle newrectangle)
        {
            Rectangle = newrectangle;
            Direction = newDirection;
            Position = newPosition;
            linearVelocity = newLinearVelocity;
            lifespan = 50;
            _Texture = content.Load<Texture2D>("Bullet");
            Origin = new Vector2(_Texture.Width / 2, _Texture.Height / 2);

        }

       

        public void Update(GameTime gametime)
        {
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (_timer>lifespan)
            {
                IsRemoved = true;
            }

            Position.X += Direction * linearVelocity;
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, 10, 6);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture, Position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
        }

        public void Collision(Tile tile)
        {
            Rectangle newRectangle = tile.Rectangle;

            if (rectangle.TouchLeftOf(newRectangle) || rectangle.TouchRightOf(newRectangle) || rectangle.TouchBottomOf(newRectangle) || rectangle.TouchTopOf(newRectangle))
            {
                this.IsRemoved = true;
            }

        }
        
    }
}
