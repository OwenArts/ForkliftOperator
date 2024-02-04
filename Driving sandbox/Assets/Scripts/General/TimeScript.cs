using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace General
{
    // This script sets the actual time on the start and then slowly updates it. 
    public class TimeScript : MonoBehaviour
    {
        [SerializeField] private DateTime actualTime;
        [SerializeField] private int updateIntervalInMinutes = 1;

        void Start() => StartCoroutine(UpdateTime());

        private IEnumerator UpdateTime()
        {
            while (true)
            {
                float angle = CalculateClockAngle(DateTime.Now);
                transform.rotation = Quaternion.Euler(angle, 0, 0);
                Debug.Log($"Current clock angle: {angle}");
                yield return new WaitForSeconds(60 * updateIntervalInMinutes);
            }
        }

        private float CalculateClockAngle(DateTime time)
        {
            // Reference angles for 00:00 and 12:00
            const float SecondsPerDay = 86400.0f; // total amount of seconds in a day = 24 * 60 * 60
            const float offset = -90.0f; // Offset added to the angle.
            const float maxAngle = 360.0f; // Offset added to the angle.
            int TimeInSeconds =
                (time.Hour * 3600) + (time.Minute * 60) + time.Second; //calculate all the seconds of the time.

            /*
             * 1. Calc percentage of sec compared to max possible amount of sec.
             * 2. Multiply by max angle of a circle.
             * 3. Add the offset because 12:00:00 = the angle of 270, not 180.
             * 4. Modulo by max angle to no value larger than the amx angle is returned.
             */
            return ((((TimeInSeconds / SecondsPerDay) * maxAngle) + offset) % maxAngle);
        }
    }
}