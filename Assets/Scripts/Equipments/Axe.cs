using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Equipment
{
    private void Awake()
    {
        kinds = eEquipment.Axe;

        originPos = new Vector3(0.2f, 0.8f, 0);
        originRot = new Vector3(0, 0, 90);
        originScale = new Vector3(0.75f, 0.75f, 1);

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
