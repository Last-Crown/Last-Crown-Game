using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Equipment
{
    private void Awake()
    {
        Kinds = eEquipment.Hammer;

        OriginPos = new Vector3(-0.5f, 0.6f, 0);
        OriginRot = new Vector3(0, 0, 90);
        OriginScale = new Vector3(0.7f, 0.7f, 1);
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
