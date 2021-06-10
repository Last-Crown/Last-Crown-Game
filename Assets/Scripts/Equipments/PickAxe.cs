using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : Equipment
{
    private void Awake()
    {
        OriginPos = new Vector2(-0.2f, 0.6f);
        OriginRot = new Vector3(0, 0, 90);
        OriginScale = new Vector3(0.8f, 0.8f, 1);
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
