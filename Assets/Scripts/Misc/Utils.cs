using UnityEngine;

namespace SASTFUtils
{
    public static class Utils {
        public static bool compareColor(Color color1, Color color2)
        {
            bool compareColorComponent(float c1, float c2)
            {
                return Mathf.Abs(c1 - c2) < 0.1f;
            }

            return compareColorComponent(color1.r, color2.r) & compareColorComponent(color1.g, color2.g) & compareColorComponent(color1.b, color2.b) & compareColorComponent(color1.a, color2.a);
        }
    }
}
