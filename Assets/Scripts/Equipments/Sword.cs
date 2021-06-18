using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    private void Awake()
    {
        kinds = eEquipment.Sword;

        originPos = new Vector3(0.15f, 0.5f, 0);
        originRot = new Vector3(0, 0, -15);
        originScale = new Vector3(1f, 1.3f, 1);

        damage = 1;
        attackSpeed = 1;
        animString = "useHand";
    }
}
