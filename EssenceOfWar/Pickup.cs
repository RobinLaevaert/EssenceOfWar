using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class Pickup:Tile
    {
        public bool isPicked = false;
        private string _type;
        
        public Pickup(int ID, Rectangle newRectangle, List<Pickup> pickups)
        {
            this.id = ID;

            switch (id)
            {
                case 18:
                    _type = "Ammo";
                    break;
                case 19:
                    _type = "Armor";
                    break;
                case 20:
                    _type = "Health";
                    break;
                case 23:
                    _type = "KeyRed";
                    break;
            }
            texture = Content.Load<Texture2D>(_type);
            this.Rectangle = newRectangle;
            
            
            

        }

        public string pak()
        {
            
            return _type;

        }
    }
}
