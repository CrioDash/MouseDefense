using UnityEngine;

namespace Utilities
{
    public static class Bezier
    {
        public static Vector3 GetBezier(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            t = Mathf.Clamp01(t);
            float t1 = 1 - t;
            return t1 * t1 * t1 * p1 + 3f * t1 * t1 * t * p2 + 3 * t1 * t * t * p3 + t * t * t * p4;
        }

        public static Vector3 BezierRotation(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            t = Mathf.Clamp01(t);
            float t1 = 1 - t;
            return 3f * t1 * t1 * (p2 - p1) + 6f * t1 * t * (p3 - p2) + 3f * t * t * (p4 - p3);
        }

    }
}