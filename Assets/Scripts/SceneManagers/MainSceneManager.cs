using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    GameObject nameError;

    private void Awake()
    {
        nameError = GameObject.Find("NameError");

        nameError.SetActive(false);
    }

    public void EnableNameError(string message)
    {
        nameError.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
        nameError.SetActive(true);
    }

    public void EmitName()
    {
        string name = GameObject.Find("InputField").GetComponent<InputField>().text;

        GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().EmitName(name);
    }

    public void OnClickTutorial()
    {
        var loader = GameObject.Find("LevelLoader").GetComponentInChildren<LevelLoader>();
        loader.LoadScene("TutorialScene");
    }
}
