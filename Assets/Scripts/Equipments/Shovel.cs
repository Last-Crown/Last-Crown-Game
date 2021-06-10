using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Equipment
{
    private void Awake()
    {
        OriginPos = new Vector2(-0.1f, 0.6f);
        OriginRot = new Vector3(0, 0, 50);
    }

    public override void Equip(Transform hand)
    {
        base.Equip(hand);
    }

    public override void UnEquip()
    {
        base.UnEquip();
    }
}
