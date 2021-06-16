using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public eEquipment WhatsInHand;
    public Equipment NearByTool;

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
    private int PickableLayer, ToolCountLimit;


    void Awake()
    {
        WhatsInHand = eEquipment.None;

        navRange = 1.2f;
        canPickTool = false;
        Hands = transform.GetChild(0).GetChild(1).GetChild(0);
        PlayerAnim = GetComponent<Animator>();
        PickableLayer = 1 << LayerMask.NameToLayer("Pickable");
        ToolCountLimit = 2;
    }

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        StartCoroutine(NavigateAraund());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))   // Pick
            PickEquipment();

        if (Input.GetKeyDown(KeyCode.F))    // Drop
            DropEquipment();

        if (Input.GetKeyDown(KeyCode.E)) // Tool Cycle
            CycleTools();

        if (Input.GetKeyDown(KeyCode.Q))    // Tool Activate
            ActivateEquipment();
    }

    public void ActivateEquipment()
    {
        if (WhatsInHand == eEquipment.None)
            return;

        Equipment e = MyEquipmentsDict[WhatsInHand];
        e.Use(PlayerAnim);
    }

    public void CycleTools()
    {
        if (ToolsList.Count <= 0)
            return;

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

    public void PickEquipment()
    {
        if (!canPickTool || NearByTool == null)
            return;

        if (ToolsList.Count >= ToolCountLimit)
            DropEquipment();
        ChangeEquipment(NearByTool);
    }

    private void DropEquipment()
    {
        if (WhatsInHand == eEquipment.None)
            return;

        MyEquipmentsDict[WhatsInHand].Drop();
        MyEquipmentsDict.Remove(WhatsInHand);
        ToolsList.Remove(WhatsInHand);
        WhatsInHand = eEquipment.None;

        PlayerAnim.SetBool("isHold", false);
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
}
