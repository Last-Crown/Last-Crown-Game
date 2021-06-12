using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOnGround : Resource, IHarvestable
{
    private int HitLimit;

    private void Awake()
    {
        HitLimit = 4;
        MatchedTool = eEquipment.PickAxe;
        H_Kinds = eHarResource.Rock;
    }

    public void Hit(int damage, eEquipment equipment)
    {
        if (HitLimit > 0 && equipment == MatchedTool)
        {
            HitLimit -= damage;
            Debug.Log("µ¹ ¸ÂÀ½");
            return;
        }
    }
}
