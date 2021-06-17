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
    public float damage, attackSpeed, curTime = 0;
    public bool CanUse => curTime <= 0;

    protected PlayerStats stats;

    public virtual void Update()
    {
        if (!CanUse)
            curTime -= Time.deltaTime;
    }

    public virtual void Equip(Transform root, Transform hand)
    {
        stats = root.GetComponent<PlayerStats>();

        transform.SetParent(hand);
        transform.localPosition = originPos;
        transform.localEulerAngles = originRot;
        transform.localScale = originScale;

        GetComponent<SpriteRenderer>().sortingOrder = 5;
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

        curTime = 1 / stats.AttackSpeed;    // 공격 속도 지정
        anim.speed = stats.AttackSpeed;     // 애니메이션 속도 지정
        anim.SetTrigger(animString);
    }
}
