using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    protected Vector3 OriginPos, OriginRot, OriginScale;

    public virtual void Equip(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = OriginPos;
        transform.localEulerAngles = OriginRot;
        transform.localScale = OriginScale;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        Debug.Log("Equipped");
    }

    public virtual void Drop()
    {
        transform.parent = null;

        GetComponent<SpriteRenderer>().sortingOrder = 0;
        Debug.Log("Dropped");
    }
}
