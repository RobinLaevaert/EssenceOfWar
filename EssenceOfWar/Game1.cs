using EssenceOfWar.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace EssenceOfWar
{

    
    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     * * *                                                                                                                                     * * *
     * * *      ########  ######   ######  ######## ##    ##  ######  ########     #######  ########    ##      ##    ###    ########          * * *
     * * *      ##       ##    ## ##    ## ##       ###   ## ##    ## ##          ##     ## ##          ##  ##  ##   ## ##   ##     ##         * * *
     * * *      ##       ##       ##       ##       ####  ## ##       ##          ##     ## ##          ##  ##  ##  ##   ##  ##     ##         * * *
     * * *      ######    ######   ######  ######   ## ## ## ##       ######      ##     ## ######      ##  ##  ## ##     ## ########          * * *
     * * *      ##             ##       ## ##       ##  #### ##       ##          ##     ## ##          ##  ##  ## ######### ##   ##           * * *
     * * *      ##       ##    ## ##    ## ##       ##   ### ##    ## ##          ##     ## ##          ##  ##  ## ##     ## ##    ##          * * *
     * * *      ########  ######   ######  ######## ##    ##  ######  ########     #######  ##           ###  ###  ##     ## ##     ##         * * *
     * * *                                                                                                                                     * * *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * * *                                                                                                                                     * * *
     * * *      Made By:                                                                                                                       * * *
     * * *          Robin Laevaert   2EA2                                                                                                      * * *
     * * *                                                                                                                                     * * *
     * * *      Special thanks to:                                                                                                             * * *
     * * *          Oyyou - https://www.youtube.com/user/oyyou91 - Tutorial for Collision, tilemap, camera, enemies, shooting                  * * *
     * * *          Stephan Smith - https://www.youtube.com/channel/UC7-OPCXfRVfTw3KvRxpMnNA - Tutorial for delay Timer                        * * *
     * * *          RB whitaker - http://rbwhitaker.wikidot.com/ - GameStates                                                                  * * *
     * * *          Tom Peeters - XNA basics                                                                                                   * * *
     * * *                                                                                                                                     * * *
     * * *      Assets used:                                                                                                                   * * *
     * * *          Texture Pack - Platformer Art Deluxe - Kenney - https://opengameart.org/content/platformer-art-deluxe                      * * *
     * * *          Menu image - Arma 3 wallpaper - https://arma3.com/media/wallpapers                                                         * * *
     * * *          Menu soundtrack - Light of the seven - Ramin Djawadi (Game of Thrones) - https://goo.gl/dUJ9Cm                             * * *
     * * *          In-Game soundtrack - Kill the boy - Ramin Djawadi (Game of Thrones) - https://goo.gl/m4MQNY                                * * *
     * * *          Gun shot sound - Five Seven - Counter Strike: Global Offensive                                                             * * *
     * * *                                                                                                                                     * * *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
     

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private State _currentState;
        private State _nextState;
        public List<Keys> Toetsen = new List<Keys>();
        Keys KeyLeft = Keys.Left;
        Keys KeyRight = Keys.Right;
        Keys KeyShoot = Keys.Space;
        Keys KeyUp = Keys.Up;
        Keys KeyWeapon1 = Keys.D1;
        Keys KeyWeapon2 = Keys.D2;
        Keys KeyWeapon3 = Keys.D3;
        Keys KeyDown = Keys.Down;
        GameOverState gameOverState;
        GameEndState gameEndState;
        public GameState tempGamestate;
        private Song menuSong;
        private Song gameSong;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public void gameOver(int score)
        {
            gameOverState.FinalScore = score;
            _nextState = gameOverState;
            gameOverState.HasPlayed = false;
        }
        public void gameEnd(int score)
        {
            gameEndState.FinalScore = score;
            _nextState = gameEndState;
            gameEndState.HasPlayed = false;
        }
        public void changeKeysGameOver(List<Keys> newLijst)
        {
            gameOverState.Lijst = newLijst;
        }

        Rectangle screen = new Rectangle(0, 0, 1920, 1050);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screen.Width;
            graphics.PreferredBackBufferHeight = screen.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Toetsen.Add(KeyLeft);
            Toetsen.Add(KeyRight);
            Toetsen.Add(KeyUp);
            Toetsen.Add(KeyDown);
            Toetsen.Add(KeyShoot);
            Toetsen.Add(KeyWeapon1);
            Toetsen.Add(KeyWeapon2);
            Toetsen.Add(KeyWeapon3);
            gameOverState = new GameOverState(this, graphics.GraphicsDevice, Content, Toetsen, 0);
            gameEndState = new GameEndState(this, graphics.GraphicsDevice, Content, Toetsen, 0);
            menuSong = Content.Load<Song>("SoundFX/MenuTrack");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(menuSong);
        }

        
        protected override void Initialize()
        { 
            IsMouseVisible = true;
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content, Toetsen,true);
            gameSong = Content.Load<Song> ("SoundFX/SoundTrack");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }
        
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {

            if (_nextState !=null)
            {
                if(_nextState is MenuState) { MediaPlayer.Resume(); }
                if (_nextState is PauseState) { MediaPlayer.Play(menuSong); }
                if(_nextState is GameState ) { MediaPlayer.Stop();MediaPlayer.Play(gameSong); }
                
                _currentState = _nextState;
                _nextState = null;
                
            }
            _currentState.Update(gameTime);
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if(_currentState is GameState)
                {
                    MediaPlayer.Resume();
                    tempGamestate = (GameState)_currentState;
                    this.ChangeState(new PauseState(this, graphics.GraphicsDevice, Content, Toetsen));
                }
            }
                

           
            base.Update(gameTime);
            
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
            
        }
    }
}
