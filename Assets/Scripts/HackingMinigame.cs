using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HackingMinigame : MonoBehaviour
{

    // Use this for initialization
    public int[] matchAmplitudes;
    public int[] playerAmplitudes;
    public Slider[] playerSliders;
    public Color backgroundColor;
    public Color graphColor;
    public Color playerColor;
    public float period;


    Texture2D texture;
    Vector2 lastPoint;
    Vector2 playerLastPoint;

    void Start()
    {

        for (int i = 0; i < matchAmplitudes.Length; ++i)
        {
            matchAmplitudes[i] = 8 * Random.Range(0, 10);
        }

        texture = new Texture2D(500, 250, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;

        UpdateGraph();

        GetComponent<RawImage>().texture = texture;
    }

    public void UpdateGraph()
    {
        for (int i = 0; i < playerAmplitudes.Length; ++i)
        {
            playerAmplitudes[i] = 4 * (int)playerSliders[i].value;
        }
        for (int x = 0; x < texture.width; ++x)
            for (int y = 0; y < texture.height; ++y)
                texture.SetPixel(x, y, backgroundColor);
        for (float x = 0; x < texture.width; ++x)
        {
            float y = texture.height / 2;
            float yPlayer = texture.height / 2;
            for (int i = 0; i < matchAmplitudes.Length; ++i)
            {
                y += Mathf.Sin(x / (period * (i + 1))) * matchAmplitudes[i];
                yPlayer += Mathf.Sin(x / (period * (i + 1))) * playerAmplitudes[i];
            }
            if (x > 0)
            {
                DrawLine((int)lastPoint.x, (int)lastPoint.y, (int)x, (int)y, graphColor);
                DrawLine((int)playerLastPoint.x, (int)playerLastPoint.y, (int)x, (int)yPlayer, playerColor);
            }
            else
            {
                texture.SetPixel((int)x, (int)y, graphColor);
                texture.SetPixel((int)x, (int)yPlayer, playerColor);
            }
            lastPoint = new Vector2(x, y);
            playerLastPoint = new Vector2(x, yPlayer);
        }
        texture.Apply();
    }

    void DrawLine(int x, int y, int x2, int y2, Color color)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            texture.SetPixel(x, y, color);
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }

    }
    //void WuLine(float x0, float y0, float x1, float y1, Color color) {
    //    bool steep = Mathf.Abs(y1 - y0) > Mathf.Abs(x1 - x0);
    //    float temp;
    //    if (steep)
    //    {
    //        temp = x0; x0 = y0; y0 = temp;
    //        temp = x1; x1 = y1; y1 = temp;
    //    }
    //    if (x0 > x1)
    //    {
    //        temp = x0; x0 = x1; x1 = temp;
    //        temp = y0; y0 = y1; y1 = temp;
    //    }

    //    double dx = x1 - x0;
    //    double dy = y1 - y0;
    //    double gradient = dy / dx;

    //    double xEnd = Mathf.Round(x0);
    //    double yEnd = y0 + gradient * (xEnd - x0);
    //    double xGap = rfpart(x0 + 0.5);
    //    double xPixel1 = xEnd;
    //    double yPixel1 = (int)(yEnd);

    //    if (steep)
    //    {
    //        texture.SetPixel(yPixel1, xPixel1, new Color(color.r, color.g, color.b, rfpart(yEnd) * xGap);
    //        texture.SetPixel(yPixel1 + 1 , xPixel1, new Color(color.r, color.g, color.b, fpart(yEnd) * xGap);
    //    }
    //    else
    //    {
    //        texture.SetPixel(xPixel1, yPixel1, new Color(color.r, color.g, color.b, rfpart(yEnd) * xGap);
    //        texture.SetPixel(xPixel1, yPixel1 + 1, new Color(color.r, color.g, color.b, fpart(yEnd) * xGap);
    //    }
    //    double intery = yEnd + gradient;

    //    xEnd = Mathf.Round(x1);
    //    yEnd = y1 + gradient * (xEnd - x1);
    //    xGap = fpart(x1 + 0.5);
    //    double xPixel2 = xEnd;
    //    double yPixel2 = ipart(yEnd);
    //    if (steep)
    //    {
    //        plot(bitmap, yPixel2, xPixel2, rfpart(yEnd) * xGap);
    //        plot(bitmap, yPixel2 + 1, xPixel2, fpart(yEnd) * xGap);
    //    }
    //    else
    //    {
    //        plot(bitmap, xPixel2, yPixel2, rfpart(yEnd) * xGap);
    //        plot(bitmap, xPixel2, yPixel2 + 1, fpart(yEnd) * xGap);
    //    }

    //    if (steep)
    //    {
    //        for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
    //        {
    //            plot(bitmap, ipart(intery), x, rfpart(intery));
    //            plot(bitmap, ipart(intery) + 1, x, fpart(intery));
    //            intery += gradient;
    //        }
    //    }
    //    else
    //    {
    //        for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
    //        {
    //            plot(bitmap, x, ipart(intery), rfpart(intery));
    //            plot(bitmap, x, ipart(intery) + 1, fpart(intery));
    //            intery += gradient;
    //        }
    //    }
    //}
}
