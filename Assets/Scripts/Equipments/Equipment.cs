using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eEquipment
{
    None, Axe, Hammer, PickAxe, Shovel
}

public class Equipment : MonoBehaviour
{
    public eEquipment Kinds;    // 도구 종류
    protected Vector3 OriginPos, OriginRot, OriginScale;

    public virtual void Equip(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = OriginPos;
        transform.localEulerAngles = OriginRot;
        transform.localScale = OriginScale;

        GetComponent<SpriteRenderer>().sortingOrder = 2;
        gameObject.layer = 0;
    }

    public virtual void Drop()
    {
        transform.parent = null;

        GetComponent<SpriteRenderer>().sortingOrder = 0;
        gameObject.layer = LayerMask.NameToLayer("Pickable");
    }

    public virtual void Use()
    {
        // TODO
    }
}
