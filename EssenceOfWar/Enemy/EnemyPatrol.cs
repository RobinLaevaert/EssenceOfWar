using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace EssenceOfWar.Enemy
{
    class EnemyPatrol 
    {
        private Animation _walkAnimation;
        private Animation _shootAnimation;
        Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }
        Vector2 position;
        Vector2 origin;
        Vector2 velocity;
        Texture2D texture;
        Texture2D textureWalkLeft;
        Texture2D textureWalkRight;
        Texture2D textureShootLeft;
        Texture2D textureShootRight;
        SoundEffect Shooteffect;
        SoundEffect Scream;
        bool isRemoved;
        
        public bool IsRemoved
        {
            get { return isRemoved; }
        }
        float heroDistance;
        float heroHeight;
        int ShootAnimationStep = 0;
        Delay ShootDelay = new Delay(.33f);
        int richting;

        
        private ContentManager _content;

        bool right;
        bool shoot;
        float maxX;
        float minX;

        float distance;
       

        public EnemyPatrol(ContentManager content,Vector2 newPosition, float newDistance)
        {
            _content = content;
            position = newPosition;
            maxX = position.X + (int)newDistance;
            minX = position.X;
            textureWalkLeft = _content.Load<Texture2D>("Enemy/EnemyLeft");
            textureWalkRight = _content.Load<Texture2D>("Enemy/EnemyRight");
            textureShootLeft = _content.Load<Texture2D>("Enemy/EnemyShootLeft");
            textureShootRight = _content.Load<Texture2D>("Enemy/EnemyShootRight");
            Shooteffect = _content.Load<SoundEffect>("SoundFX/Shoot");
            Scream = _content.Load<SoundEffect>("SoundFX/0477");
            texture = textureWalkLeft;

            _walkAnimation = new Animation();
            _walkAnimation.AddFrame(new Rectangle(0, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(78, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(156, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(234, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(312, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(390, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(468, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(546, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(624, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(702, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(780, 0, 78, 88));
            _walkAnimation.AddFrame(new Rectangle(858, 0, 78, 88));
            _walkAnimation.AantalBewegingenPerSeconde = 15;

            _shootAnimation = new Animation();
            _shootAnimation.AddFrame(new Rectangle(0, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(108, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(216, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(324, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(432, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(540, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(648, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(756, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(864, 0, 108, 88));
            _shootAnimation.AddFrame(new Rectangle(972, 0, 108, 88));
            _shootAnimation.AantalBewegingenPerSeconde = 50;

        }

        public void update(GameTime gameTime, List<Bullet> enemykogels, Hero2 Held)
        {
            position += velocity;
            origin = new Vector2(78/ 2, 88 / 2);
            if (position.X <= minX)
            {
                right = true;
                velocity.X = 1f;
            }
            else if (position.X >= maxX)
            {
                right = false;
                velocity.X = -1f;
            }

            if (right)
            {
                texture = textureWalkRight;
                distance += 1;
            }
            else
            {
                texture = textureWalkLeft;
                distance -= 1;
            }
            if (!shoot)
                _walkAnimation.Update(gameTime);

            if (shoot)
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, 108, 88);
                if (ShootAnimationStep == 15) { ShootAnimationStep = 0; shoot = false; }
                else
                {
                    switch (right)
                    {
                        case true:
                            richting = 3;
                            break;
                        case false:
                            richting = -3;
                            break;
                        
                    }

                    if (ShootDelay.timerDone(gameTime))
                    {
                        this.SchietKogel(enemykogels);
                    }

                    _shootAnimation.Update(gameTime);
                    ShootAnimationStep++;
                }
               
            }
            else
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, 78, 88);
            }
            

            if (velocity.Y < 10)
                velocity.Y += 0.4f;

            heroDistance = Held.Positie.X - position.X;
            heroHeight = Held.Positie.Y;

            if (heroDistance >= -400 && heroDistance <= 400 &&(this.position.Y < heroHeight +15 && this.position.Y > heroHeight -15))
            {
                if (heroDistance < -1)
                { if (!right) {velocity.X = 0; right = false; richting = -3; shoot = true; } }

                else if (heroDistance > 1)
                { if (right) { velocity.X = 0; right = true; richting = 3; shoot = true; } }
                else if (heroDistance == 0)
                    velocity.X = 0;
            }
            else
            {
                
                shoot = false;
                switch (right)
                {
                    case true:
                        velocity.X = 1f;
                        break;
                    case false:
                        velocity.X = -1f;
                        break;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (shoot)
            {
                if (right)
                    spriteBatch.Draw(textureShootRight, position, _shootAnimation.CurrentFrame.SourceRectangle, Color.White);
                else
                    spriteBatch.Draw(textureShootLeft, position, _shootAnimation.CurrentFrame.SourceRectangle, Color.White);

            }
            else
            {
                spriteBatch.Draw(texture, position, _walkAnimation.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
        }

        public void SchietKogel(List<Bullet> kogels)
        {
            kogels.Add(new Bullet((float)richting, new Vector2(position.X + this.rectangle.Width + 5, position.Y + (this.rectangle.Height / 2)), 5, new Rectangle((int)(position.X + this.rectangle.Width + 5), (int)(position.Y + (this.rectangle.Height / 2)), 10, 6)));
            Shooteffect.Play();
            
        }

        public void Collision(Tile tile, int xOffset, int yOffset)
        {
            Rectangle newRectangle = tile.Rectangle;
            if (rectangle.TouchTopOf(newRectangle))
            {
                newRectangle.Y = newRectangle.Y * rectangle.Height + 20;
                velocity.Y = 0f;
                
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 5;
                right = false;
                velocity.X = -1f;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 5;
                right = true;
                velocity.X = 1f;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        public void CollisionBullet(Bullet Kogel ,Hero2 hero)
        {
            Rectangle newRectangle = Kogel.Rectangle;

            if (rectangle.TouchCentreOf(newRectangle))
            {
                this.Dies();
                hero.Score += 5;
                Kogel.IsRemoved = true;
            }

        }

        public void Dies()
        {
            isRemoved = true;
            Scream.Play();
        }
    }
}
