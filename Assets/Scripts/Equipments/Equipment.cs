using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    protected Vector2 OriginPos;
    protected Vector3 OriginRot;

    public virtual void Equip(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = OriginPos;
        transform.localEulerAngles = OriginRot;
        Debug.Log("Equipped");
    }

    public virtual void UnEquip()
    {
        transform.parent = null;
        Debug.Log("UnEquipped");
    }
}
