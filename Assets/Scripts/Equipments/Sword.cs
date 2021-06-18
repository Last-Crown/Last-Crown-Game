using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    private void Awake()
    {
        kinds = eEquipment.Sword;

        originPos = new Vector3(0.2f, 0.7f, 0);
        originRot = new Vector3(0, 0, -15);
        originScale = new Vector3(0.4f, 0.4f, 1);

        damage = 1;
        attackSpeed = 1;
        animString = "useHand";
    }

    public override void Equip(Transform root, Transform hand)
    {
        base.Equip(root, hand.parent.GetChild(1)); // Right hand
    }
}
