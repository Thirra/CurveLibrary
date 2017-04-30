using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CurveLibrary
{
    public class Curve
    {
        //Cubic interpolation?
        public static Vector3 Spline (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 a = 2.0f * p1;
            Vector3 b = p2 - p0;
            Vector3 c = 2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3;
            Vector3 d = -p0 + 3.0f * p1 - 3.0f * p2 + p3;

            Vector3 p = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
            return p;
        }

        //This could also be the same as above
        public static Vector3 CubicInterpolation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float t3 = t * t * t;
            float t2 = t * t;

            Vector3 p = (p3 - p2 - p0 + p1) * t3 + (2.0f * p0 - 2.0f * p1 + p2 - p3) * t2 + (p2 - p0) * t + p1;
            return p;
        }

        public static Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float u = 1.0f - t;

            return u * u * p0 + 2.0f * u * t * p1 + t * t * p2;
        }
        //https://en.wikibooks.org/wiki/Cg_Programming/Unity/B%C3%A9zier_Curves


        public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1.0f - t;
            float t2 = t * t;
            float u2 = u * u;
            float u3 = u2 * u;
            float t3 = t2 * t;

            Vector3 p = (u3) * p0 + (3.0f * u2 * t) * p1 + (3.0f * u * t2) * p2 + (t3) * p3;

            return p;
        }

        public static Vector3 Hermite (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t, float tension, float bias)
        {
            Vector3 m0, m1;
            float a0, a1, a2, a3;

            m0 = (p1 - p0) * (1.0f + bias) * (1.0f - tension) / 2.0f;
            m0 += (p2 - p1) * (1.0f - bias) * (1.0f - tension) / 2.0f;
            m1 = (p2 - p1) * (1.0f + bias) * (1.0f - tension) / 2.0f;
            m1 += (p3 - p2) * (1.0f - bias) * (1.0f - tension) / 2.0f;
            a0 = 2.0f * t * t * t - 3.0f * t * t + 1.0f;
            a1 = t * t * t - 2 * t * t + t;
            a2 = t * t * t - t * t;
            a3 = -2 * t * t * t + 3 * t * t;
            return (a0 * p1 + a1 * m0 + a2 * m1 + a3 * p2);
        }
    }
}
