using EssenceOfWar.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar.States
{
    class PauseState:State
    {
        private List<Component> _components;
        List<Keys> _lijst;
        private Texture2D background;

        public PauseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, List<Keys> Lijst) : base(game, graphicsDevice, content)
        {
            _lijst = Lijst;
            var buttonTexture = _content.Load<Texture2D>("Buttons/2017718162714695");
            var buttonFont = _content.Load<SpriteFont>("Fonts");
            background = _content.Load<Texture2D>("BackGround");
        

            var resumeGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 400),
                Text = "Resume"

            };
            resumeGameButton.Click += ResumeGameButton_Click;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 550),
                Text = "New Game"
            };
            newGameButton.Click += NewGameButton_Click;

            

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 700),
                Text = "Quit Game"

            };
            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                resumeGameButton,
                newGameButton,
                
                quitGameButton
            };


        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _lijst));
        }

        

        private void ResumeGameButton_Click(object sender, EventArgs e)
        {
            
            _game.ChangeState(_game.tempGamestate);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1060), Color.AliceBlue);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);


            spriteBatch.End();
        }

        

        public override void Update(GameTime gameTime)
        {
            
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}

