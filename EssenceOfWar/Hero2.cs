﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EssenceOfWar
{
    class Hero2
    {
        public Vector2 Positie;
        private Texture2D Texture;
        private Texture2D TextureLeft;
        private Texture2D TextureRight;
        private Texture2D TextureShootRight;
        private Texture2D TextureShootLeft;
        private Texture2D TextureBullet;
        private Texture2D TextureHP;
        private Texture2D TextureArmor;
        private Texture2D TextureHPArmor;
        private SoundEffect Shooteffect;
        private SoundEffect ShootEmptyEffect;
        public Rectangle rectangle;
        private Animation _animation;
        private Animation _shootAnimation;
        private Vector2 hpBarPosition;
        private bool Shoot = false;
        static public bool onLadder;
        private bool Left = false;
        private bool Right = true;
        private int Shootanimationstep = 0;
        public int kogelsaantal = 5000;
        public int Armor = 0;
        public int HP = 50;
        private int richting;
        public int selectiewapen = 1;
        private bool KeyRedgevonden = false;
        public bool Kamer1Gevonden = false;
        private bool HasJumped = false;
        public HealthBar HPBar;
        public HealthBar ArmorBar;
        public HealthBar OverlayBar;
        
        
        
        
        Delay ShootDelay = new Delay(.33f);
        




        public Vector2 Velocity;
        public Bediening bediening { get; set; }

        public Hero2(Vector2 _positie)
        {
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 72, 86);
            Texture = TextureRight;
            Positie = _positie;
            hpBarPosition = new Vector2(500, 100);

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

        public void Load(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("heroRight"); ;
            TextureLeft = Content.Load<Texture2D>("heroLeft");
            TextureRight = Content.Load<Texture2D>("heroRight");
            TextureShootRight = Content.Load<Texture2D>("ShootRight");
            TextureShootLeft = Content.Load<Texture2D>("ShootLeft");
            TextureBullet = Content.Load<Texture2D>("Bullet");
            Shooteffect = Content.Load<SoundEffect>("SoundFX/Shoot");
            ShootEmptyEffect = Content.Load<SoundEffect>("SoundFX/ShootEmpty");
            TextureHP = Content.Load<Texture2D>("HealthBar");
            TextureArmor = Content.Load<Texture2D>("ArmorBar");
            TextureHPArmor = Content.Load<Texture2D>("HealthArmor");

            OverlayBar = new HealthBar(TextureHPArmor, hpBarPosition, new Rectangle(0, 0, TextureHPArmor.Width, TextureHPArmor.Height));
            HPBar = new HealthBar(TextureHP, hpBarPosition, new Rectangle(0, 0, TextureHP.Width, TextureHP.Height));
            ArmorBar = new HealthBar(TextureArmor, hpBarPosition, new Rectangle(0, 0, 0, TextureArmor.Height));

        }

        public void Update(GameTime gameTime, List<Bullet> kogels)
        {
            Positie += Velocity;

            bediening.Update();

#region Switch Wapens
            if (bediening.Weapon1)
            {
                selectiewapen = 1;
            }
            if (bediening.Weapon2)
            {
                selectiewapen = 2;
            }
            if (bediening.Weapon3)
            {
                selectiewapen = 3;
            }
            #endregion

            if (bediening.left || bediening.right)
                    _animation.Update(gameTime);

            if (Shoot == false)
            {
               
                if (bediening.left)
                {
                    Velocity.X = -3;
                    Texture = TextureLeft;
                    Left = true;
                    Right = false;
                } // links wandelen
                else if (bediening.right)
                {
                    Velocity.X = 3;
                    Texture = TextureRight;
                    Left = false;
                    Right = true;
                } //rechts wandelen
                else Velocity.X = 0f; //reset snelheid wanneer niks ingedrukt

                if (bediening.Jump)
                {
                    if (!HasJumped)
                    {
                        Positie.Y -= 5f;
                        Velocity.Y = -13f;
                        HasJumped = true;
                    }
                } //springen
                if (bediening.Shoot)
                {
                    if (Shoot) { }
                    else
                    {
                        if (kogelsaantal > 0)
                        {
                            Shoot = true;
                            ShootDelay.setDelay(.33f);

                            switch (Right)
                            {
                                default:
                                    richting = 3;
                                    break;
                                case false:
                                    richting = -3;
                                    break;
                            }       
                        }
                        else
                        {
                            if (ShootDelay.timerDone(gameTime))
                                ShootEmptyEffect.Play();
                        } //play sound no bullet
                    }
                } //schieten
                if (bediening.Down && onLadder)
                {
                    Positie.Y += 3;

                } //dalen op ladder
            }

                if (Shoot)
                {
                    if (Shootanimationstep == 15) {  Shootanimationstep = 0; Shoot = false; }
                    else
                    {
                    switch (selectiewapen)
                    {
                        case 1:
                            if (ShootDelay.timerDone(gameTime))
                            {
                                this.SchietKogel(kogels);
                            }
                            break;
                        case 2:
                            if (ShootDelay.timerDone(gameTime))
                            {
                                this.SchietKogel(kogels);
                                ShootDelay.setDelay(.09f);
                                if (ShootDelay.timerDone(gameTime))
                                {
                                    this.SchietKogel(kogels);
                                }
                            }
                            break;
                        case 3:
                            ShootDelay.setDelay(.08f);
                            if (ShootDelay.timerDone(gameTime))
                            {
                                this.SchietKogel(kogels);  
                            }
                            break;

                    } //1 = single, 2 = Burst, 3 = full Auto 
                    
                    

                    Velocity.X = 0;
                        _shootAnimation.Update(gameTime);
                        Shootanimationstep++;
                    
                    }
                }

            if (Velocity.Y < 10 && onLadder == false)
                Velocity.Y += 0.4f;

            onLadder = false;
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 50, 86);

            HPBar.current.Width = HP * 6;
            ArmorBar.current.Width = Armor * 12;
        }

        public void Collision(Tile tile, int xOffset, int yOffset)
        {
            Rectangle newRectangle = tile.Rectangle;
            if(tile.id == 18 || tile.id == 19 || tile.id == 20 || tile.id == 23)
            {
                if (rectangle.TouchCentreOf(newRectangle)) 
                {
                    Pickup pickup = (Pickup)tile;
                    string _type = pickup.pak();

                    switch (_type)
                    {
                        case "Ammo":
                            if (pickup.isPicked == false)
                            {
                                kogelsaantal += 5;
                                pickup.isPicked = true;
                            }
                            break;
                        case "Armor":
                            if(Armor == 50) { }
                            else if (pickup.isPicked == false)
                            {
                                Armor += 5;
                                if (Armor > 50)
                                    Armor = 50;
                                pickup.isPicked = true;
                            }
                            break;
                        case "Health":
                            if(HP == 100) { }
                            else if (pickup.isPicked == false)
                            {
                                HP += 5;
                                if (HP > 100)
                                    HP = 100;
                                pickup.isPicked = true;
                            }
                            break;
                        case "KeyRed":
                            if(pickup.isPicked == false)
                            {
                                KeyRedgevonden = true;
                                pickup.isPicked = true;
                            }
                            break;

                    }
                }
            } //powerups collision
            else if (tile.id==16 || tile.id ==17)
                {
                    if (rectangle.TouchCentreOf(newRectangle))
                    {
                    Velocity.Y = 0;
                    onLadder = true;
                    HasJumped = false;
                    }
                
                } //ladder collision
            else if(tile.id == 21|| tile.id == 22)
            {
                if (rectangle.TouchCentreOf(newRectangle))
                {
                    if (KeyRedgevonden)
                        Kamer1Gevonden = true;
                    
                        
                }
            }
            else
            {
                if (rectangle.TouchTopOf(newRectangle))
                {
                    newRectangle.Y = newRectangle.Y * rectangle.Height + 20;
                    Velocity.Y = 0f;
                    onLadder = false;
                    HasJumped = false;
                }

                if (rectangle.TouchLeftOf(newRectangle))
                {
                    Positie.X = newRectangle.X - rectangle.Width - 5;
                }

                if (rectangle.TouchRightOf(newRectangle))
                {
                    Positie.X = newRectangle.X + newRectangle.Width + 5;
                }
                if (rectangle.TouchBottomOf(newRectangle))
                {
                    Velocity.Y = 1f;                    
                }

                if (Positie.X < 0) Positie.X = 0;
                if (Positie.X > xOffset - rectangle.Width) Positie.X = xOffset - rectangle.Width;
                if (Positie.Y < 0) Velocity.Y = 1f;
                if (Positie.Y > yOffset - rectangle.Height) Positie.Y = yOffset - rectangle.Height;
            } //map collision
        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            if (Shoot)
            {
                if(Left)
                    spritebatch.Draw(TextureShootLeft, Positie, _shootAnimation.CurrentFrame.SourceRectangle, Color.White);

                if(Right)
                    spritebatch.Draw(TextureShootRight, Positie, _shootAnimation.CurrentFrame.SourceRectangle, Color.White);
            }
            else
            {
                spritebatch.Draw(Texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
        }

        public void SchietKogel(List<Bullet> kogels)
        {
            kogels.Add(new Bullet((float)richting, new Vector2(Positie.X + this.rectangle.Width + 5, Positie.Y + (this.rectangle.Height / 2)), 5, new Rectangle((int)(Positie.X + this.rectangle.Width + 5), (int)(Positie.Y + (this.rectangle.Height / 2)), 10, 6)));
            Shooteffect.Play();
            kogelsaantal--;
        }

    }

}

