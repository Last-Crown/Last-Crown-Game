using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnGround : HarvestableResource
{
    public Sprite treeStumpSprite;    // ³ª¹« ¹ØµÕ

    private void Awake()
    {
        hitLimit = 4;
        MatchedTool = eEquipment.Axe;
        Kinds = eResource.Tree;
    }

    public override void UpdateHitLimit(float _hitLimit)
    {
        hitLimit = _hitLimit;
        if (hitLimit <= 0)
        {
            hitLimit = 0;
            GetComponent<SpriteRenderer>().sprite = treeStumpSprite;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
    }
}
