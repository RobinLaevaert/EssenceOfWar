using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using EssenceOfWar.Control;
using Microsoft.Xna.Framework.Media;

namespace EssenceOfWar.States
{


    class GameEndState : State
    {
        private List<Component> _components;
        private List<Keys> _lijst;
        public List<Keys> Lijst
        {
            get { return _lijst; }
            set { _lijst = value; }
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
        private SoundEffect soundGameEnd;
        private Song MenuSong;
        private bool hasPlayed = false;
        public bool HasPlayed
        {
            set { hasPlayed = value; }
        }
        SoundEffectInstance soundEffectInstance;
        public GameEndState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content,List<Keys> Lijst, int finalscore) : base(game, graphicsDevice, content)
        {
            _finalScore = finalscore;
            _lijst = Lijst;
            GraphicsDevice _graphicsDevice = graphicsDevice;
            ContentManager _content = content;
            var buttonTexture = _content.Load<Texture2D>("Buttons/2017718162714695");
            var buttonFont = _content.Load<SpriteFont>("Fonts");
            TextureGameOver = _content.Load<Texture2D>("Vicotry");
            soundGameEnd = _content.Load<SoundEffect>("SoundFX/Victory_Sound_Effect");
            MenuSong = _content.Load<Song>("SoundFX/MenuTrack");
            background = _content.Load<Texture2D>("BackGround");
            soundEffectInstance = soundGameEnd.CreateInstance();
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
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _lijst,true));
            MediaPlayer.Play(MenuSong);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (!hasPlayed)
            {
                soundEffectInstance.Play();
                hasPlayed = true;
            }
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1060), Color.AliceBlue);
            spriteBatch.Draw(TextureGameOver, new Rectangle(340, 0, 1239, 1019), Color.AliceBlue);
            spriteBatch.DrawString(font, Score, new Vector2(960 - (font.MeasureString(Score).X / 2), 750), Color.Red);
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
