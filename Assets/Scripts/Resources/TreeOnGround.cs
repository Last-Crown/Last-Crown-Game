using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnGround : HarvestableResource
{
    public Sprite treeStumpSprite;    // ³ª¹« ¹ØµÕ
    public Sprite originSprite;

    private void Awake()
    {
        hitLimit = 4;
        MatchedTool = eEquipment.Axe;
        Kinds = eHarResource.Tree;
    }

    public override void Harvest(float damage, eEquipment tool)
    {
        base.Harvest(damage, tool);

        if (hitLimit <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = treeStumpSprite;
            hitLimit = 0;
        }
    }
}
