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

    protected eHarResource Kinds { get; set; }
    protected eEquipment MatchedTool { get; set; }

    public virtual void Harvest(float damage, eEquipment tool)
    {
        if (hitLimit > 0 && (tool == MatchedTool || tool == eEquipment.None))
        {
            hitLimit -= damage;
            Debug.Log(Kinds.ToString() + " Hit : " + hitLimit);
        }
    }
}
