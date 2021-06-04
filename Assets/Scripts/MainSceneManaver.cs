using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManaver : MonoBehaviour
{
    public void EmitName()
    {
        string name = GameObject.Find("InputField").GetComponent<InputField>().text;

        GameObject.Find("Server").GetComponent<ServerInitializer>().EmitName(name);
    }
}
