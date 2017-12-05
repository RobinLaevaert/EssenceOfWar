using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EssenceOfWar
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _heroTextureLeft;
        Texture2D _heroTextureRight;
        Texture2D _heroTextureShootLeft;
        Texture2D _heroTextureShootRight;
        Texture2D _background;
        Texture2D castleCentre; 
        Texture2D castleSlopeLeft;
        Texture2D castleSlopeRight;
        Texture2D castleMid;

        Vector2 backgroundpostion;
        Hero _hero;
        Camera camera;
        Keys KeyLeft = Keys.Left;
        Keys KeyRight = Keys.Right;
        Keys KeyShoot = Keys.Space;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1050;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            camera = new Camera(GraphicsDevice.Viewport);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTextureLeft = Content.Load<Texture2D>("heroLeft");
            _heroTextureRight = Content.Load<Texture2D>("heroRight");
            _heroTextureShootRight = Content.Load<Texture2D>("ShootRight");
            _heroTextureShootLeft = Content.Load<Texture2D>("ShootLeft");
            _hero = new Hero(_heroTextureLeft, _heroTextureRight, _heroTextureShootLeft, _heroTextureShootRight, new Vector2(0, 500));
            _hero.bediening = new Bediening(KeyLeft, KeyRight, KeyShoot);
            _background = Content.Load<Texture2D>("backgroundTest");
            backgroundpostion = new Vector2(-200, 0);
            castleCentre = Content.Load<Texture2D>("castleCenter");
            castleMid = Content.Load<Texture2D>("castleMid");
            castleSlopeLeft = Content.Load<Texture2D>("castleHillLeft");
            castleSlopeRight = Content.Load<Texture2D>("castleHillRight");
            l = new Level();
            l.castleCentre = castleCentre;
            l.castleMid = castleMid;
            l.castleSlopeLeft = castleSlopeLeft;
            l.castleSlopeRight = castleSlopeRight;
            l.CreateWorld();



            // TODO: use this.Content to load your game content here
        }
        Level l;

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _hero.Update(gameTime);
            base.Update(gameTime);
            camera.Update(gameTime, _hero);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,camera.transform);
            spriteBatch.Draw(_background, backgroundpostion,Color.White);
            l.DrawLevel(spriteBatch);
            _hero.Draw(spriteBatch);

           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
