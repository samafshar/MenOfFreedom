using UnityEngine;
using System.Collections;

public class SignalingMaterial : MonoBehaviour
{
    public float speed = 1f;

    public float minValue = 70f;
    public float maxValue = 255f;

    float hue;
    float saturation;
    float value;

    int sign = -1;

    Color color = new Color();
        
    void Start()
    {
        color = renderer.materials[0].GetColor("_TintColor");

        RGBToHSV(color.r, color.g, color.b, out hue, out saturation, out value);

        saturation *= 255f;
        value *= 255f;
    }

    void Update()
    {
        value += sign * Time.deltaTime * speed;

        if (value < minValue || value > maxValue)
            sign *= -1;

        value = Mathf.Clamp(value, minValue, maxValue);

        float sat = saturation / 255f;
        float val = value / 255f;

        HsvToRgb(hue, sat, val, out color.r, out color.g, out color.b);

        renderer.materials[0].SetColor("_TintColor", color);
    }        

    void HsvToRgb(float h, float S, float V, out float r, out float g, out float b)
    {
        float H = h;
        while (H < 0) { H += 360f; };
        while (H >= 360f) { H -= 360f; };
        float R, G, B;
        if (V <= 0f)
        { R = G = B = 0f; }
        else if (S <= 0f)
        {
            R = G = B = V;
        }
        else
        {
            float hf = H / 60.0f;
            int i = (int)Mathf.Floor(hf);
            float f = hf - i;
            float pv = V * (1 - S);
            float qv = V * (1 - S * f);
            float tv = V * (1 - S * (1 - f));
            switch (i)
            {

                // Red is the dominant color

                case 0:
                    R = V;
                    G = tv;
                    B = pv;
                    break;

                // Green is the dominant color

                case 1:
                    R = qv;
                    G = V;
                    B = pv;
                    break;
                case 2:
                    R = pv;
                    G = V;
                    B = tv;
                    break;

                // Blue is the dominant color

                case 3:
                    R = pv;
                    G = qv;
                    B = V;
                    break;
                case 4:
                    R = tv;
                    G = pv;
                    B = V;
                    break;

                // Red is the dominant color

                case 5:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                case 6:
                    R = V;
                    G = tv;
                    B = pv;
                    break;
                case -1:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // The color is not defined, we should throw an error.

                default:
                    //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                    R = G = B = V; // Just pretend its black/white
                    break;
            }
        }
        r = (float)Clamp((int)(R * 255.0f));
        g = (float)Clamp((int)(G * 255.0f));
        b = (float)Clamp((int)(B * 255.0f));

        r /= 255f;
        g /= 255f;
        b /= 255f;
    }

    int Clamp(int i)
    {
        if (i < 0) return 0;
        if (i > 255) return 255;
        return i;
    }

    void RGBToHSV(float r, float g, float b, out float h, out float s, out float v)
    {
        float min, max, delta;

        float[] rgb = { r, g, b };

        min = Mathf.Min(rgb);
        max = Mathf.Max(rgb);

        v = max;

        delta = max - min;

        if (max != 0)
        {
            s = delta / max;
        }
        else
        {
            // r = g = b = 0		// s = 0, v is undefined
            s = 0;
            h = -1;
            return;
        }

        if (r == max)
            h = (g - b) / delta;
        else if (g == max)
            h = 2 + (b - r) / delta;
        else
            h = 4 + (r - g) / delta;

        h *= 60f;
        if (h < 0)
            h += 360f;
    }
}