using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [Header("Dimensions")]
    public int seed = 0;
    public int width;
    public int height;
    public float gridScale;
    public float scale;
    public Vector2 offset;

    [Header("Height Map")]
    public float[,] heightMap;

    private void Start()
    {
        if (seed < 0)
        {
            seed = Random.Range(0, 999999);
        }
        Random.InitState(seed);

        offset.x = Random.value * 100 * width;
        offset.y = Random.value * 100 * height;

        GenerateMap();
    }

    private void GenerateMap()
    {
        heightMap = NoiseGenerator.Generate(width, height, scale, offset);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                string newFloorName;
                string newResourceName;
                GameObject newFloor;
                GameObject newResource;
                int resourceCount;

                if (heightMap[i, j] > 0.6f)
                {
                    newFloorName = "StoneFloor";
                    newResourceName = "Rock";
                    newFloor = Resources.Load<GameObject>("Prefabs/Floors/" + newFloorName);
                    newResource = Resources.Load<GameObject>("Prefabs/Resources/" + newResourceName);
                    resourceCount = Random.Range(0, 2);
                }
                else
                {
                    newFloorName = "GrassFloor";
                    newResourceName = "Tree";
                    newFloor = Resources.Load<GameObject>("Prefabs/Floors/" + newFloorName);
                    newResource = Resources.Load<GameObject>("Prefabs/Resources/" + newResourceName);
                    resourceCount = Random.Range(0, 3);
                }

                Debug.Log((heightMap[i, j] > 0.6f ? "Stone" : "Grass") + " " + resourceCount);

                newFloor = Instantiate(newFloor);
                newFloor.name = newFloorName + "[" + i + "," + j + "]";
                newFloor.transform.position = new Vector2(gridScale * (i - width / 2), gridScale * (j - height / 2));

                for (int k = 0; k < resourceCount; k++)
                {
                    newResource = Instantiate(newResource);
                    newResource.name = newResourceName + "[" + i + "," + j + "," + k + "]";
                    newResource.transform.position = newFloor.transform.position + new Vector3(gridScale * (0.5f - Random.value), gridScale * (0.5f - Random.value));
                }
            }
        }
    }
}
