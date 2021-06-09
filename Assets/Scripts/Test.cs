using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public RawImage debugImage;

    [Header("Dimensions")]
    public int seed = 0;
    public int width;
    public int height;
    public float scale;
    public Vector2 offset;

    [Header("Height Map")]
    public float[,] heightMap;

    private void Start()
    {
        seed = 300;
        Random.InitState(seed);

        offset.x = Random.value * 100;
        offset.y = Random.value * 100;

        GenerateMap();
    }

    private void GenerateMap()
    {
        heightMap = NoiseGenerator.Generate(width, height, scale, offset);

        Color[] pixels = new Color[width * height];
        int i = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pixels[i] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                i++;
            }
        }

        Texture2D tex = new Texture2D(width, height);
        tex.SetPixels(pixels);
        tex.filterMode = FilterMode.Point;
        tex.Apply();

        debugImage.texture = tex;
    }
}
