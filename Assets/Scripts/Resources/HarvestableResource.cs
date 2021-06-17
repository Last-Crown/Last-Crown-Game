using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eHarResource    // harvestable resource
{
    None, Tree, Rock
}

public class HarvestableResource : MonoBehaviour
{
    protected float hitLimit;

    public eHarResource Kinds { get; set; }
    public eEquipment MatchedTool { get; set; }

    public virtual void Harvest(float damage, eEquipment tool)
    {
        if (tool == MatchedTool && hitLimit > 0)
        {
            hitLimit -= damage;
            Debug.Log(Kinds.ToString() + " Hit : " + hitLimit);
        }
    }
}
