using Microsoft.Xna.Framework;
using System;

namespace UniverseOfSwords.Utilities
{
    public partial class UniverseUtils
    {
        public struct Easings
        {
            public static float EaseInBack(float x)
            {
                double c1 = 1.70158;
                float c3 = (float)c1 + 1;

                return (float)(c3 * x * x * x - c1 * x * x);
            }

            public static float EaseInSine(float x) => 1f - MathF.Cos(x * MathHelper.Pi / 2);

            public static float EaseInCubic(float x) => x * x * x;

            public static float EaseInOutQuad(float x) => x < 0.5 ? 2 * x * x : 1 - MathF.Pow(-2 * x + 2, 2) / 2;

            public static float EaseOutCirc(float x) => MathF.Sqrt(1 - MathF.Pow(x - 1, 2));

            public static float EaseInOutCirc(float x) => x < 0.5 ? (1 - MathF.Sqrt(1f - MathF.Pow(2 * x, 2))) / 2 : (MathF.Sqrt(1f - MathF.Pow(-2 * x + 2, 2)) + 1) / 2;

            public static float EaseInCirc(float x) => 1f - MathF.Sqrt(1f - MathF.Pow(x, 2));
        }
    }
}
