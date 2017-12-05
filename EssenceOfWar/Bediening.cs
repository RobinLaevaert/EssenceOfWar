using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace EssenceOfWar
{
    

    public class Bediening
    {
        private Keys _keyLeft = Keys.Left;
        private Keys _keyRight = Keys.Right;
        private Keys _keyShoot = Keys.Space;
        public Bediening(Keys KeyLeft, Keys KeyRight, Keys KeyShoot) {
            _keyLeft = KeyLeft;
            _keyRight = KeyRight;
            _keyShoot = KeyShoot;

        }

        public bool left { get; set; }
        public bool right { get; set; }
        public bool Shoot { get; set; }
        public void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(_keyLeft))
            {
                left = true;
            }
            if (stateKey.IsKeyUp(_keyLeft))
            {
                left = false;
            }

            if (stateKey.IsKeyDown(_keyRight))
            {
                right = true;
            }
            if (stateKey.IsKeyUp(_keyRight))
            {
                right = false;
            }

            if (stateKey.IsKeyDown(_keyShoot)) {
                Shoot = true;
            }
            if (stateKey.IsKeyUp(_keyShoot))
            {
                Shoot = false;
            }

        }

    }






}
