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
using Microsoft.Xna.Framework.Media;

namespace EssenceOfWar.States
{
    public class MenuState : State
    {
        
        private List<Component> _components;
        private Texture2D background;
        List<Keys> _lijst;
        private Song MenuSong;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, List<Keys> Lijst,bool playnew) : base(game, graphicsDevice, content )
        {
            _lijst = Lijst;
            var buttonTexture = _content.Load<Texture2D>("Buttons/2017718162714695");
            var buttonFont = _content.Load<SpriteFont>("Fonts");
            background = _content.Load<Texture2D>("BackGround");
            MenuSong = _content.Load<Song>("SoundFX/MenuTrack");
            MediaPlayer.IsRepeating = true;
            if(playnew)
            MediaPlayer.Play(MenuSong);



            var startGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 400),
                Text = "Start"
                
                
            };
            startGameButton.Click += StartGameButton_Click;

            var settingsGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 550),
                Text = "Settings"

            };
            settingsGameButton.Click += SettingsGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 700),
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
            MediaPlayer.Stop();
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _lijst));
        }

        private void SettingsGameButton_Click(object sender, EventArgs e)
        {
            MediaPlayer.Resume();
            _game.ChangeState(new SettingsState(_game, _graphicsDevice, _content, _lijst));
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
