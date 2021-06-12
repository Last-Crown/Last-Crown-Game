using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eHarResource    // harvestable resource
{
    None, Tree, Rock
}

public enum eColResource    // collectable resource
{
    None, Wood, Stone
}


public class Resource : MonoBehaviour
{
    public eHarResource H_Kinds;
    public eColResource C_Kinds;
}
