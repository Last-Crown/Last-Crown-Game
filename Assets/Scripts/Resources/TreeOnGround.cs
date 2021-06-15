using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnGround : HarvestableResource
{
    public Sprite TreeStump;    // ³ª¹« ¹ØµÕ
    public Sprite Origin;

    private void Awake()
    {
        HitLimit = 4;
        MatchedTool = eEquipment.Axe;
        Kinds = eHarResource.Tree;
    }

    public override void Harvest(float damage, eEquipment tool)
    {
        base.Harvest(damage, tool);

        if (HitLimit <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = TreeStump;
            HitLimit = 0;
        }
    }
}
