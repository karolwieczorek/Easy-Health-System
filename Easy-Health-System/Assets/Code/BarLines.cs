using UnityEngine;
using UnityEngine.UI;

namespace EasyHealthSystem {
    public class BarLines : MonoBehaviour {
        public int widht = 1;

        [ContextMenu("Test bar")]
        void Test()
        {
            UpdateBar(.25f);
        }

        public void UpdateBar(float linePercent)
        {
            var rectTransform = GetComponent<RectTransform>();

            var tex = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height, TextureFormat.ARGB32, false);

            Color fillColor = Color.clear;
            Color[] fillPixels = new Color[tex.width * tex.height];

            for (int i = 0; i < fillPixels.Length; i++)
            {
                fillPixels[i] = fillColor;
            }

            tex.SetPixels(fillPixels);
            
            for(float filled = linePercent; filled < 1; filled += linePercent)
            {
                DrawFillLine(tex, filled);
            }
            
            tex.Apply();
            GetComponent<RawImage>().texture = tex;
        }

        private void DrawFillLine(Texture2D tex, float fill)
        {
            var rectTransform = GetComponent<RectTransform>();

            int x0 = (int)(rectTransform.rect.width * fill);
            int y0 = 0;
            int x1 = (int)(rectTransform.rect.width * fill); ;
            int y1 = (int)(rectTransform.rect.height); ;
            Color c = Color.white;
            
            for (int i = 0; i < widht; i++)
                DrawLine(tex, x0 + i, y0, x1 + i, y1, c);
        }

        static void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
        {
            int dy = (int)(y1 - y0);
            int dx = (int)(x1 - x0);
            int stepx, stepy;

            if (dy < 0) { dy = -dy; stepy = -1; }
            else { stepy = 1; }
            if (dx < 0) { dx = -dx; stepx = -1; }
            else { stepx = 1; }
            dy <<= 1;
            dx <<= 1;

            float fraction = 0;

            tex.SetPixel(x0, y0, col);
            if (dx > dy)
            {
                fraction = dy - (dx >> 1);
                while (Mathf.Abs(x0 - x1) > 1)
                {
                    if (fraction >= 0)
                    {
                        y0 += stepy;
                        fraction -= dx;
                    }
                    x0 += stepx;
                    fraction += dy;
                    tex.SetPixel(x0, y0, col);
                }
            }
            else
            {
                fraction = dx - (dy >> 1);
                while (Mathf.Abs(y0 - y1) > 1)
                {
                    if (fraction >= 0)
                    {
                        x0 += stepx;
                        fraction -= dy;
                    }
                    y0 += stepy;
                    fraction += dx;
                    tex.SetPixel(x0, y0, col);
                }
            }
        }
    }
}
