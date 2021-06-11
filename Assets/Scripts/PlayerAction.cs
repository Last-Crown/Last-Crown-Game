using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public eEquipment WhatsInHand;
    public Equipment NearByTool;

    private Transform Hands;
    private PlayerHealth playerHealth;
    private Dictionary<eEquipment, Equipment> MyEquipments = new Dictionary<eEquipment, Equipment>()
    {
        { eEquipment.None, null }
    };

    public GameObject FrontObject;


    // private float coolTime, curTime;
    private float navRange;
    public bool canPickTool;
    private int PickableLayer;


    void Awake()
    {
        // coolTime = curTime = 0.8f;
        WhatsInHand = eEquipment.None;

        navRange = 1.2f;
        canPickTool = false;
        Hands = transform.GetChild(0).GetChild(1).GetChild(0);
        PickableLayer = 1 << LayerMask.NameToLayer("Pickable");
    }

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        StartCoroutine(NavigateAraund());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPickTool && NearByTool != null)
        {
            ChangeEquipment(NearByTool);
        }
        if (Input.GetKeyDown(KeyCode.F) && WhatsInHand != eEquipment.None)
        {
            MyEquipments[WhatsInHand].Drop();
            MyEquipments.Remove(WhatsInHand);
            WhatsInHand = eEquipment.None;
        }
    }

    // ���� �������� obj�� ��ü
    private void ChangeEquipment(Equipment obj)
    {
        MyEquipments[WhatsInHand]?.gameObject.SetActive(false);  // ���� ���� ��Ȱ��ȭ

        WhatsInHand = obj.Kinds;

        if (!MyEquipments.ContainsKey(obj.Kinds)) // obj�� ó�� ���� �������
        {
            obj.Equip(Hands);
            MyEquipments.Add(obj.Kinds, obj);
            return;
        }

        MyEquipments[obj.Kinds].gameObject.SetActive(true);   // obj Ȱ��ȭ
    }

    IEnumerator NavigateAraund()
    {
        while (playerHealth.IsAlive)
        {
            float dis = 100, temp;

            // PickableMask�� Ž��
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, navRange, PickableLayer);
            foreach (var col in collider2Ds)
            {
                // �� ����� �� ã��
                temp = Vector3.Distance(transform.position, col.transform.position);
                if (dis >= temp)
                {
                    dis = temp;
                    NearByTool = col.GetComponent<Equipment>();
                }
            }

            if (collider2Ds.Length <= 0)
            {
                canPickTool = false;
                NearByTool = null;
            }
            else
                canPickTool = true;
                

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Resource") && FrontObject == null)
            FrontObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FrontObject = null;
    }
}
