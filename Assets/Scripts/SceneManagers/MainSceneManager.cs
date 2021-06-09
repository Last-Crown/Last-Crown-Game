using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public void EmitName()
    {
        string name = GameObject.Find("InputField").GetComponent<InputField>().text;

        GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().EmitName(name);
    }
}
