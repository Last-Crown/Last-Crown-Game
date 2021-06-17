using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance ??= FindObjectOfType<UIManager>();
    private static UIManager instance;

    private PlayerAction pa;

    private void Awake()
    {
        pa = GameObject.FindWithTag("Player").GetComponent<PlayerAction>();
    }

    public void ButtonActivate() => pa.ActivateEquipment();
    public void ButtonPick() => pa.PickEquipment();
    public void ButtonCycle() => pa.CycleTools();
}
