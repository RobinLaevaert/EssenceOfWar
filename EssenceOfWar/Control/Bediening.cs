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
        private Keys _keyJump = Keys.Up;
        private Keys _keyDown = Keys.Down;
        private Keys _keyShoot = Keys.Space;
        private Keys _keyWeapon1 = Keys.NumPad1;
        private Keys _keyWeapon2 = Keys.NumPad2;
        private Keys _keyWeapon3 = Keys.NumPad3;
        public Bediening(Keys KeyLeft, Keys KeyRight,Keys KeyUp,Keys KeyDown, Keys KeyShoot, Keys KeyWeapon1, Keys KeyWeapon2, Keys KeyWeapon3) {
            _keyLeft = KeyLeft;
            _keyRight = KeyRight;
            _keyJump = KeyUp;
            _keyDown = KeyDown;
            _keyShoot = KeyShoot;
            _keyWeapon1 = KeyWeapon1;
            _keyWeapon2 = KeyWeapon2;
            _keyWeapon3 = KeyWeapon3;
        }

        public bool left { get; set; }
        public bool right { get; set; }
        public bool Shoot { get; set; }
        public bool Jump { get; set; }
        public bool Down { get; set; }
        public bool Weapon1 { get; set; }
        public bool Weapon2 { get; set; }
        public bool Weapon3 { get; set; }
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

            if (stateKey.IsKeyDown(_keyJump))
            {
                Jump = true;
            }
            if (stateKey.IsKeyUp(_keyJump))
            {
                Jump = false;
            }

            if (stateKey.IsKeyDown(_keyDown))
            {
                Down = true;
            }
            if (stateKey.IsKeyUp(_keyDown))
            {
                Down = false;
            }

            if (stateKey.IsKeyDown(_keyWeapon1))
            {
                Weapon1 = true;
            }
            if (stateKey.IsKeyUp(_keyWeapon1))
            {
                Weapon1 = false;
            }

            if (stateKey.IsKeyDown(_keyWeapon2))
            {
                Weapon2 = true;
            }
            if (stateKey.IsKeyUp(_keyWeapon2))
            {
                Weapon2 = false;
            }

            if (stateKey.IsKeyDown(_keyWeapon3))
            {
                Weapon3 = true;
            }
            if (stateKey.IsKeyUp(_keyWeapon3))
            {
                Weapon3 = false;
            }


        }

    }






}
