using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Utilities
{
    public static class Ballistics
    {
        private static float g = 9.81f;
        public static float Angle = 60*Mathf.Deg2Rad;
        
        public static float GetBallistics(Vector3 startPos, Vector3 endPos)
        {
            Vector3 fromTo = startPos - endPos;
            float x = new Vector3(fromTo.x, 0, fromTo.z).magnitude;
            float y = fromTo.y;

            float v = Mathf.Sqrt(
                Mathf.Abs((g * x * x) / (2 * (y - Mathf.Tan(Angle) * x) * Mathf.Pow(Mathf.Cos(Angle), 2))));
            return v;
        }
    }
}