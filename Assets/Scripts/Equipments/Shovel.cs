using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Equipment
{
    private void Awake()
    {
        Kinds = eEquipment.Shovel;

        OriginPos = new Vector3(-0.1f, 0.6f, 0);
        OriginRot = new Vector3(0, 0, 90);
        OriginScale = new Vector3(0.75f, 0.75f, 1);

        CoolTime = 1;
        AnimString = "useShovel";
    }

    public override void Equip(Transform hand)
    {
        base.Equip(hand);
    }

    public override void Drop()
    {
        base.Drop();
    }
}
