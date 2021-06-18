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
            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = treeStumpSprite;
            sr.sortingOrder = 1;
        }
    }
}
