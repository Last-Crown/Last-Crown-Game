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

    
}
