using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHarvestable
{
    void Hit(int damage, eEquipment equipment);
}

public interface ICollectable
{
    void Collecte();
}

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
    public eEquipment MatchedTool = 0;
    public eHarResource H_Kinds = 0;
    public eColResource C_Kinds = 0;
}
