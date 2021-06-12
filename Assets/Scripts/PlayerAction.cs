using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public eEquipment WhatsInHand;
    public Equipment NearByTool;


    public GameObject FrontObject;
    private Transform Hands;
    private Animator PlayerAnim;
    private PlayerHealth playerHealth;

    private LinkedList<eEquipment> ToolsList = new LinkedList<eEquipment>();
    private Dictionary<eEquipment, Equipment> MyEquipmentsDict = new Dictionary<eEquipment, Equipment>()
    {
        { eEquipment.None, null }
    };


    private float navRange;
    public bool canPickTool;
    private int PickableLayer;


    void Awake()
    {
        WhatsInHand = eEquipment.None;

        navRange = 1.2f;
        canPickTool = false;
        Hands = transform.GetChild(0).GetChild(1).GetChild(0);
        PlayerAnim = GetComponent<Animator>();
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
            MyEquipmentsDict[WhatsInHand].Drop();
            MyEquipmentsDict.Remove(WhatsInHand);
            ToolsList.Remove(WhatsInHand);
            WhatsInHand = eEquipment.None;

            PlayerAnim.SetBool("isHold", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && ToolsList.Count > 0)
        {
            eEquipment obj;
            if (WhatsInHand != eEquipment.None)
            {
                LinkedListNode<eEquipment> currentNode = ToolsList.Find(WhatsInHand);

                // ���� ���� enum ���� (���� ��尡 ������ ����̸� ó�� �ɷ� �ٲ���)
                obj = currentNode == ToolsList.Last ? ToolsList.First.Value : currentNode.Next.Value;
            }
            else
                obj = ToolsList.First.Value;
            
            ChangeEquipment(MyEquipmentsDict[obj]);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Equipment e = MyEquipmentsDict[WhatsInHand];
            
            if (WhatsInHand != eEquipment.None && e.CanUse)
            {
                e.Use(PlayerAnim);
                FrontObject?.GetComponent<IHarvestable>().Hit(1, WhatsInHand);
            }
        }
    }

    // ���� �������� obj�� ��ü
    private void ChangeEquipment(Equipment obj)
    {
        PlayerAnim.SetBool("isHold", true);

        MyEquipmentsDict[WhatsInHand]?.gameObject.SetActive(false);  // ���� ���� ��Ȱ��ȭ

        WhatsInHand = obj.Kinds;
        

        if (!MyEquipmentsDict.ContainsKey(obj.Kinds)) // obj�� ó�� ���� �������
        {
            obj.Equip(Hands);
            MyEquipmentsDict.Add(obj.Kinds, obj);
            ToolsList.AddLast(obj.Kinds);
            return;
        }

        MyEquipmentsDict[obj.Kinds].gameObject.SetActive(true);   // obj Ȱ��ȭ
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
        if (FrontObject == null)
        {
            if (collision.CompareTag("Tree") || collision.CompareTag("Rock"))
            {
                FrontObject = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FrontObject = null;
    }
}
