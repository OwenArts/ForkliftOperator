using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Timeout
    {
        private TimeSpan previous;
        private float interval;

        public Timeout(float interval)
        {
            this.interval = interval;
            previous = DateTime.UtcNow.TimeOfDay;
        }

        public Boolean TimeOutCompleted()
        {
            var now = DateTime.UtcNow.TimeOfDay;

            if (now.TotalSeconds >= previous.TotalSeconds + interval)
            {
                previous = now;
                return true;
            }

            return false;
        }
    }
}