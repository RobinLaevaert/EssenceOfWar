using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfWar
{
    class Delay
    {
        private TimeSpan delayRate;
        private TimeSpan previousCallTime;

        public Delay(float rate)
        {
            delayRate = TimeSpan.FromSeconds(rate);
            previousCallTime = TimeSpan.FromSeconds(0.0f);
        }

        public void setDelay(float rate)
        {
            delayRate = TimeSpan.FromSeconds(rate);
        }

        public float getDelay()
        {
            return (float)delayRate.TotalMilliseconds / 1000.0f;
        }

        public bool timerDone(GameTime gametime)
        {
            if (gametime.TotalGameTime - previousCallTime > delayRate)
            {
                previousCallTime = gametime.TotalGameTime;
                return true;
            }
            return false;
        }
    }
}
