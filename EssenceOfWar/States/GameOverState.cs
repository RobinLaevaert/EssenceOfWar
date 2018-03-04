using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EssenceOfWar.Control;
using Microsoft.Xna.Framework.Audio;

namespace EssenceOfWar.States
{
    class GameOverState : State
    {

        private List<Component> _components;
        private List<Keys> _lijst;
        public List<Keys> Lijst
        {
            get{ return _lijst; }
            set { _lijst = value;}
        }
        private int _finalScore;
        public int FinalScore
        {
            set { _finalScore = value; }
        }
        private Texture2D TextureGameOver;
        private Texture2D background;
        private String Score;
        private SpriteFont font;
        private SoundEffect soundGameOver;
        private bool hasPlayed = false;
        public bool HasPlayed
        {
            set { hasPlayed = value; }
        }
        SoundEffectInstance soundEffectInstance;

        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, List<Keys> Lijst, int finalscore) : base(game, graphicsDevice, content)
        {
            _finalScore = finalscore;
            _lijst = Lijst;
            GraphicsDevice _graphicsDevice = graphicsDevice;
            ContentManager _content = content;
            var buttonTexture = _content.Load<Texture2D>("Buttons/2017718162714695");
            var buttonFont = _content.Load<SpriteFont>("Fonts");
            TextureGameOver = _content.Load<Texture2D>("GameOver");
            soundGameOver = _content.Load<SoundEffect>("SoundFX/Losing_Horn_Sound");
            background = _content.Load<Texture2D>("BackGround");
            soundEffectInstance = soundGameOver.CreateInstance();
            soundEffectInstance.IsLooped = false;
            

            font = buttonFont;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(455, 750),
                Text = "Restart"
            };
            newGameButton.Click += NewGameButton_Click;



            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1165, 750),
                Text = "Quit Game"

            };
            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                
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
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _lijst, true));
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1060), Color.AliceBlue);
            if (!hasPlayed)
            {
                soundEffectInstance.Play();
                hasPlayed = true;
            }
            spriteBatch.Draw(TextureGameOver, new Rectangle(455, 200, 1010, 122), Color.Red);
            spriteBatch.DrawString(font, Score, new Vector2(960 - (font.MeasureString(Score).X / 2), 500), Color.Red);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

       

        public override void Update(GameTime gameTime)
        {
            Score = "Score: " + _finalScore.ToString();
            
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
