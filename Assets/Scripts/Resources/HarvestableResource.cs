using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eHarResource    // harvestable resource
{
    None, Tree, Rock
}

public class HarvestableResource : MonoBehaviour
{
    protected float HitLimit;

    public eHarResource Kinds { get; set; }
    public eEquipment MatchedTool { get; set; }

    public virtual void Harvest(float damage, eEquipment tool)
    {
        if (tool == MatchedTool && HitLimit > 0)
        {
            HitLimit -= damage;
            Debug.Log(Kinds.ToString() + " Hit : " + HitLimit);
        }
    }
}
