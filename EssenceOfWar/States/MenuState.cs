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
    public class MenuState : State
    {
        private List<Component> _components;
        List<Keys> _lijst;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, List<Keys> Lijst) : base(game, graphicsDevice, content)
        {
            _lijst = Lijst;
            var buttonTexture = _content.Load<Texture2D>("Buttons/2017718162714695");
            var buttonFont = _content.Load<SpriteFont>("Fonts");
            var startGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Start"
                
            };
            startGameButton.Click += StartGameButton_Click;

            var settingsGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Settings"

            };
            settingsGameButton.Click += SettingsGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 500),
                Text = "Quit Game"

            };
            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                startGameButton,
                settingsGameButton,
                quitGameButton
            };
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _lijst));
        }

        private void SettingsGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SettingsState(_game, _graphicsDevice, _content, _lijst));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);


            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
