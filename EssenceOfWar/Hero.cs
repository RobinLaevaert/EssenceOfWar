using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class Hero
    {
        public Vector2 Positie { get; set; }
        private Texture2D Texture { get; set; }
        private Texture2D TextureLeft;
        private Texture2D TextureRight;
        private Texture2D TextureShootRight;
        private Texture2D TextureShootLeft;
        private Rectangle _showRect;
        public Rectangle CollisionRect;
        private Animation _animation;
        private Animation _shootAnimation;
        private bool Shoot = false;
        private int Shootanimationstep = 0;
        

        public Vector2 VelocityX = new Vector2(3, 0);
        public Bediening bediening { get; set; }

        public Hero(Texture2D _textureLeft, Texture2D _textureRight, Texture2D _textureShootLeft, Texture2D _textureShootRight, Vector2 _positie )
        {
            
            Texture = _textureRight;
            TextureRight = _textureRight;
            TextureLeft = _textureLeft;
            TextureShootRight = _textureShootRight;
            TextureShootLeft = _textureShootLeft;
            Positie = _positie;
            _showRect = new Rectangle(0, 0, 72, 86);
            CollisionRect = new Rectangle((int)Positie.X, (int)Positie.Y, 72, 86);

            _animation = new Animation();
            _animation.AddFrame(new Rectangle(0, 0, 72, 86));
            _animation.AddFrame(new Rectangle(72, 0, 72, 86));
            _animation.AddFrame(new Rectangle(144, 0, 72, 86));
            _animation.AddFrame(new Rectangle(216, 0, 72, 86));
            _animation.AddFrame(new Rectangle(288, 0, 72, 86));
            _animation.AddFrame(new Rectangle(360, 0, 72, 86));
            _animation.AddFrame(new Rectangle(432, 0, 72, 86));
            _animation.AddFrame(new Rectangle(504, 0, 72, 86));
            _animation.AddFrame(new Rectangle(576, 0, 72, 86));
            _animation.AddFrame(new Rectangle(648, 0, 72, 86));
            _animation.AddFrame(new Rectangle(720, 0, 72, 86));
            _animation.AddFrame(new Rectangle(792, 0, 72, 86));
            _animation.AantalBewegingenPerSeconde = 15;

            _shootAnimation = new Animation();
            _shootAnimation.AddFrame(new Rectangle(0, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(106, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(212, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(318, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(424, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(530, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(636, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(742, 0, 106, 86));
            _shootAnimation.AddFrame(new Rectangle(848, 0, 106, 86));
            _shootAnimation.AantalBewegingenPerSeconde = 50;
        }

        public void Update(GameTime gameTime)
        {
            bediening.Update();


            if (bediening.left && bediening.right) { }
            else
            {
                if (bediening.left || bediening.right)
                    _animation.Update(gameTime);

                if (bediening.left)
                {
                    Texture = TextureLeft;
                    Positie -= VelocityX;
                }
                if (bediening.right)
                {
                    Positie += VelocityX;
                    Texture = TextureRight;
                }
                if (bediening.Shoot)
                {
                    if (Shoot) { }
                    else { Shoot = true; }

                }


                if (Shoot)
                {
                    if (Shootanimationstep == 9) { Shoot = false; Shootanimationstep = 0; }
                    else
                    {
                        _shootAnimation.Update(gameTime);
                        Shootanimationstep++;
                    }
                }



            }

           

                CollisionRect.X = (int)Positie.X;
                CollisionRect.Y = (int)Positie.Y;
            
        }
        Rectangle _ShowRectangle = new Rectangle(0, 0, 72, 86);

        public void Draw(SpriteBatch spritebatch)
        {
            if(Shoot) {
                spritebatch.Draw(TextureShootRight, Positie, _shootAnimation.CurrentFrame.SourceRectangle, Color.White); }
            else { 
            spritebatch.Draw(Texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.AliceBlue);}
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRect;
        }

    }
}
