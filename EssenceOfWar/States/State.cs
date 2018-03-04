﻿using Microsoft.Xna.Framework;
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
    public abstract class State
    {
        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        

        public abstract void Update(GameTime gameTime);

        
    public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
            _game = game;
        }
    }
}
