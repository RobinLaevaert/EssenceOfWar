using EssenceOfWar.Enemy;
using EssenceOfWar.MapItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class Map
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        private List<Pickup> pickups = new List<Pickup>();
        private List<EnemyPatrol> vijanden = new List<EnemyPatrol>();
        public List<Bullet> Kogels;
        public List<Bullet> EnemyKogels;
        private ContentManager _content;
        private List<FlagRobin> flags = new List<FlagRobin>();
        public List<FlagRobin> Flags
        {
            get { return flags; }
        }

        public List<Pickup> Pickups
        {
            get { return pickups; }
        }
        
        public List<CollisionTiles> CollisiontTiles
        {
            get { return collisionTiles; }
        }

        public List<EnemyPatrol> Vijanden
        {
            get { return vijanden; }
        }

        private int width, height;
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Map(ContentManager content) { _content = content; }

        public void Generate(int[,]map,int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = (map[y, x] + 1);
                    if (number == 25)
                        vijanden.Add(new EnemyPatrol(_content, new Vector2(x * size, y * size), 240));
                    if (number == 18 | number == 19 | number == 20 | number == 23 | number == 28)
                        pickups.Add(new Pickup(number, new Rectangle(x * size, y * size, size, size),pickups));
                    else if (number == 24)
                        flags.Add(new FlagRobin(number, new Rectangle(x * size, y * size, size, size)));
                    else if (number > 0 && number != 25)
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                    
                }
            
        }
        public void Update(GameTime gameTime)
        {
            foreach (FlagRobin flag in flags)
                flag.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(EnemyPatrol vijand in vijanden)
            {
                vijand.Draw(spriteBatch);
            }
            foreach (CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
            foreach(Pickup pickup in pickups)
            {
                if (pickup.isPicked == false)
                {
                    pickup.Draw(spriteBatch);
                }

            }
            foreach (FlagRobin flag in flags)
                flag.Draw(spriteBatch);

        }
    }
}
