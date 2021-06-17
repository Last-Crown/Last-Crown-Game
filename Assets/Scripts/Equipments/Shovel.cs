using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Equipment
{
    private void Awake()
    {
        kinds = eEquipment.Shovel;

        originPos = new Vector3(-0.1f, 0.6f, 0);
        originRot = new Vector3(0, 0, 90);
        originScale = new Vector3(0.75f, 0.75f, 1);

        attackSpeed = 1;
        animString = "useShovel";
    }
}
