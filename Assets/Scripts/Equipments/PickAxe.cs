using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : Equipment
{
    private void Awake()
    {
        kinds = eEquipment.PickAxe;

        originPos = new Vector3(0.2f, 0.6f, 0);
        originRot = new Vector3(0, 0, -90);
        originScale = new Vector3(0.8f, 0.8f, 1);

        damage = 1;
        attackSpeed = 1;
        animString = "useAxe";
    }
}
