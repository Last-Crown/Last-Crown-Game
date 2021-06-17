using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Equipment
{
    private void Awake()
    {
        kinds = eEquipment.Hammer;

        originPos = new Vector3(0.5f, 0.6f, 0);
        originRot = new Vector3(0, 0, -90);
        originScale = new Vector3(0.6f, 0.7f, 1);

        coolTime = 1;
        animString = "useAxe";
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
