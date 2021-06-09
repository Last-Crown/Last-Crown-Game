using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float[] noiseValues;
    void Start()
    {
        Random.InitState(42);
        noiseValues = new float[100];
        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = Random.value;
            Debug.Log(noiseValues[i]);
        }
    }
}
