using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HackingMinigame : MonoBehaviour {

    // Use this for initialization
    public int[] matchAmplitudes = { 0, 0, 0, 0, 0 };
    public int[] sinAmplitudes = { 0, 0, 0, 0, 0};
    public Color backgroundColor;
    public Color graphColor;

	void Start () {
        var texture = new Texture2D(255, 255, TextureFormat.ARGB32, false);

        for(int i = 0; i < matchAmplitudes.Length; ++i)
        {
            matchAmplitudes[i] = Random.Range(0, 30);
        }
        Color[] background;
        for (int x = 0; x < texture.width; ++x)
            for (int y = 0; y < texture.height; ++y)
                texture.SetPixel(x, y, backgroundColor);
        for (float x = 0; x < 256; x += 0.25f)
            {
                float y = 128;
                for (int i = 0; i < matchAmplitudes.Length; ++i)
                {
                    y += Mathf.Sin(x / (2*(i + 1))) * matchAmplitudes[i];
                 }
                texture.SetPixel((int)x, (int)y, graphColor);
            Debug.Log(matchAmplitudes.Length);
            }
        texture.Apply();
        GetComponent<RawImage>().texture = texture;
    }

    void Update()
    {


    }

}
