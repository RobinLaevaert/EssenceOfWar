using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using EssenceOfWar.Control;

namespace EssenceOfWar.States
{
    public class SettingsState : State
    {
        Keys KeyLeft;
        Keys KeyRight;
        Keys KeyShoot;
        Keys KeyUp;
        Keys KeyDown;
        Keys KeyWeapon1;
        Keys KeyWeapon2;
        Keys KeyWeapon3;
        List<Keys> Toetsen = new List<Keys>();
        private SpriteFont Font;
        //private GraphicsDevice _graphicsDevice;
        //private ContentManager _content;
        
        Delay delay = new Delay(500);
        private bool ChangeJump = false;
        private bool ChangeRight = false;
        private bool ChangeLeft = false;
        private bool ChangeShoot = false;
        private bool ChangeWeapon1 = false;
        private bool ChangeWeapon2 = false;
        private bool ChangeWeapon3 = false;
        private bool ChangeDown = false;
        private List<Component> _components;
        
        public SettingsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, List<Keys> Lijst) : base(game, graphicsDevice, content)
        {

            KeyLeft = Lijst[0];
            KeyRight = Lijst[1];
            KeyUp = Lijst[2];
            KeyDown = Lijst[3];
            KeyShoot = Lijst[4];
            KeyWeapon1 = Lijst[5];
            KeyWeapon2 = Lijst[6];
            KeyWeapon3 = Lijst[7];
            var buttonTexture = _content.Load<Texture2D>("Buttons/2017718162714695");
            var buttonFont = _content.Load<SpriteFont>("Fonts");
            Font = buttonFont;
            var jumpButton = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Up"

            };
            jumpButton.Click += JumpButton_Click;

            var rightButton = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Rechts"

            };
            rightButton.Click += RightButton_Click;

            var leftButton = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 500),
                Text = "Links"

            };
            leftButton.Click += LeftButton_Click;

            var downButton = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 650),
                Text = "Down"

            };
            downButton.Click += DownButton_Click;

            var shootButton = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 650),
                Text = "Schiet"

            };
            shootButton.Click += ShootButton_Click;

            var weapon1Button = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 200),
                Text = "Wapen 1"

            };
            weapon1Button.Click += weapon1Button_Click;

            var weapon2Button = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 350),
                Text = "Wapen 2"

            };
            weapon2Button.Click += Weapon2Button_Click;

            var weapon3Button = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(700, 500),
                Text = "Wapen 3"

            };
            weapon3Button.Click += Weapon3Button_Click;

            var confirmButton = new SettingsButton(buttonTexture, buttonFont)
            {
                Position = new Vector2(1000,800),
                Text = "Confirm"
            };
            confirmButton.Click += ConfirmButton_Click;


            _components = new List<Component>()
            {
                jumpButton,
                weapon1Button,
                weapon2Button,
                weapon3Button,
                rightButton,
                leftButton,
                shootButton,
                confirmButton,
                downButton
            };

            
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            ChangeDown = true;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, this.Confirm()));
        }

        private void Weapon3Button_Click(object sender, EventArgs e)
        {
            ChangeWeapon3 = true;
            

        }

        private void Weapon2Button_Click(object sender, EventArgs e)
        {
            ChangeWeapon2 = true;
        }

        private void weapon1Button_Click(object sender, EventArgs e)
        {
            ChangeWeapon1 = true;
        }

        private void ShootButton_Click(object sender, EventArgs e)
        {
            ChangeShoot = true;
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            ChangeLeft = true;
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            ChangeRight = true;
        }

        private void JumpButton_Click(object sender, EventArgs e)
        {
            ChangeJump = true;
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(Font, KeyShoot.ToString(), new Vector2(1050, 700), Color.Black);
            spriteBatch.DrawString(Font, KeyDown.ToString(), new Vector2(650, 700), Color.Black);
            spriteBatch.DrawString(Font, KeyUp.ToString(), new Vector2(650, 250), Color.Black);
            spriteBatch.DrawString(Font, KeyRight.ToString(), new Vector2(650, 400), Color.Black);
            spriteBatch.DrawString(Font, KeyLeft.ToString(), new Vector2(650, 550), Color.Black);
            spriteBatch.DrawString(Font, KeyWeapon1.ToString(), new Vector2(1050, 250), Color.Black);
            spriteBatch.DrawString(Font, KeyWeapon2.ToString(), new Vector2(1050, 400), Color.Black);
            spriteBatch.DrawString(Font, KeyWeapon3.ToString(), new Vector2(1050, 550), Color.Black);
            

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);

           
            
                
            
            
            if (ChangeJump)
                handleInput("Jump");
            if (ChangeLeft)
                handleInput("Left");
            if (ChangeRight)
                handleInput("Right");
            if (ChangeShoot)
                handleInput("Shoot");
            if (ChangeDown)
                handleInput("Down");
            if (ChangeWeapon1)
                handleInput("Weapon1");
            if (ChangeWeapon2)
                handleInput("Weapon2");
            if (ChangeWeapon3)
                handleInput("Weapon3");

            
        }

        KeyboardState currentKeyboardState = new KeyboardState();
        KeyboardState previousKeyboardState = new KeyboardState();



        public void handleInput(String Key)
        {

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            bool waitingForKey = true;
            if (waitingForKey == true)
            {
                //currentKeyboardState.GetPressedKeys() returns a list of pressed keys,
                //So, currentKeyboardState.GetPressedKeys()[0] returns the first pressed key

                if (currentKeyboardState.GetPressedKeys().Count() > 0)
                {
                    switch (Key)
                    {
                        case "Jump":
                            KeyUp = currentKeyboardState.GetPressedKeys()[0];
                            ChangeJump = false;
                            
                            break;
                        case "Right":
                            KeyRight = currentKeyboardState.GetPressedKeys()[0];
                            ChangeRight = false;
                            
                            break;
                        case "Left":
                            KeyLeft = currentKeyboardState.GetPressedKeys()[0];
                            ChangeLeft = false;
                            break;
                        case "Shoot":
                            KeyShoot = currentKeyboardState.GetPressedKeys()[0];
                            ChangeShoot = false;
                            break;
                        case "Weapon1":
                            KeyWeapon1 = currentKeyboardState.GetPressedKeys()[0];
                            ChangeWeapon1 = false;
                            break;
                        case "Weapon2":
                            KeyWeapon2 = currentKeyboardState.GetPressedKeys()[0];
                            ChangeWeapon2 = false;
                            break;
                        case "Weapon3":
                            KeyWeapon3 = currentKeyboardState.GetPressedKeys()[0];
                            ChangeWeapon3 = false;
                            break;
                        case "Down":
                            KeyDown = currentKeyboardState.GetPressedKeys()[0];
                            ChangeDown = false;
                            break;
                    }
                    waitingForKey = false;
                    this.clearButtons(_components);

                }
            }
        }

        public void clearButtons(List<Component> lijst)
        {
            foreach (SettingsButton knop in lijst)
            {
                knop._isClicked = false;
            }
        }

        public List<Keys> Confirm() {
            Toetsen.Clear();
            Toetsen.Add(KeyLeft);
            Toetsen.Add(KeyRight);
            Toetsen.Add(KeyUp);
            Toetsen.Add(KeyDown);
            Toetsen.Add(KeyShoot);
            Toetsen.Add(KeyWeapon1);
            Toetsen.Add(KeyWeapon2);
            Toetsen.Add(KeyWeapon3);
            return Toetsen;
        }
    }
}
