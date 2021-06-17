using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eEquipment
{
    None, Axe, Hammer, PickAxe, Shovel
}

public class Equipment : MonoBehaviour
{
    public eEquipment kinds;    // 도구 종류
    protected Vector3 originPos, originRot, originScale;

    protected string animString;
    public float coolTime, curTime = 0;

    public bool CanUse => curTime <= 0;


    public virtual void Update()
    {
        if (!CanUse)
            curTime -= Time.deltaTime;
    }

    public virtual void Equip(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = originPos;
        transform.localEulerAngles = originRot;
        transform.localScale = originScale;

        GetComponent<SpriteRenderer>().sortingOrder = 2;
        gameObject.layer = 0;
    }

    public virtual void Drop()
    {
        transform.parent = null;

        GetComponent<SpriteRenderer>().sortingOrder = 0;
        gameObject.layer = LayerMask.NameToLayer("Pickable");
    }

    public virtual void Use(Animator anim)
    {
        if (!CanUse) return;

        anim.SetTrigger(animString);
        curTime = coolTime;
    }
}
