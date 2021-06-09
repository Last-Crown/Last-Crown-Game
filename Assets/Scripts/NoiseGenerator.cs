using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public static float[,] Generate(int width, int height, float scale, Vector2 offset)
    {
        float[,] noiseMap = new float[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float sampleX = i * scale + offset.x;
                float smapleY = j * scale + offset.y;

                noiseMap[i, j] = Mathf.PerlinNoise(sampleX, smapleY);
            }
        }

        return noiseMap;
    }
}
