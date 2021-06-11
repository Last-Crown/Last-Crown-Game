using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEquipment
{
    None, Axe, Hammer, PickAxe, Shovel
}

public class PlayerAction : MonoBehaviour
{
    public eEquipment WhatsInHand;

    public Equipment NearByTool;
    private Transform Hands;

    public int PickableMask;

    // private float coolTime, curTime;
    private float navRange;
    public bool canPickEquipment;

    void Awake()
    {
        // coolTime = curTime = 0.8f;
        WhatsInHand = eEquipment.None;

        navRange = 1.2f;
        canPickEquipment = false;
        Hands = transform.GetChild(0).GetChild(1).GetChild(0);
        PickableMask = 1 << LayerMask.NameToLayer("Pickable");
    }

    private void Start()
    {
        StartCoroutine(NavigateAraund());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPickEquipment)
        {
            NearByTool?.Equip(Hands);
        }
    }

    IEnumerator NavigateAraund()
    {
        while (true) // ���� ���°� �ƴ� ���� �ٲٱ�
        {
            float dis = 100, temp;

            // PickableMask�� Ž��
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, navRange, PickableMask);
            foreach (var col in collider2Ds)
            {
                // �� ����� �� ã��
                temp = Vector3.Distance(transform.position, col.transform.position);
                if (dis >= temp)
                {
                    Debug.Log("find");
                    dis = temp;
                    NearByTool = col.GetComponent<Equipment>();
                }
            }

            if (collider2Ds.Length <= 0)
            {
                canPickEquipment = false;
                NearByTool = null;
            }
            else
                canPickEquipment = true;
                

            yield return new WaitForSeconds(0.1f);
        }
    }
}
