using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnGround : Resource, IHarvestable
{
    public Sprite TreeStump;    // ���� �ص�
    public Sprite Origin;

    private int HitLimit;

    private void Awake()
    {
        HitLimit = 4;
        MatchedTool = eEquipment.Axe;
        H_Kinds = eHarResource.Tree;
    }

    public void Hit(int damage, eEquipment equipment)
    {
        if (HitLimit > 0 && equipment == MatchedTool)
        {
            HitLimit -= damage;
            Debug.Log("���� ����");
            return;
        }

        GetComponent<SpriteRenderer>().sprite = TreeStump;
    }
}
