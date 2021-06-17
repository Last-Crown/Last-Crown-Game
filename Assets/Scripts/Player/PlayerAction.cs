using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public eEquipment whatsInHand = eEquipment.None;
    public Equipment nearByTool;

    private Transform hands;
    private Animator playerAnim;

    // Player
    private PlayerHealth playerHealth;

    private LinkedList<eEquipment> toolsList = new LinkedList<eEquipment>();
    private Dictionary<eEquipment, Equipment> myEquipmentsDict = new Dictionary<eEquipment, Equipment>()
    {
        { eEquipment.None, null }
    };

    private float navRange;
    public bool canPickTool;
    private int pickableLayer, toolCountLimit;


    void Awake()
    {
        navRange = 1.2f;
        canPickTool = false;
        pickableLayer = 1 << LayerMask.NameToLayer("Pickable");
        toolCountLimit = 2;
        hands = transform.GetChild(0).GetChild(1).GetChild(0);

        playerAnim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
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
        if (whatsInHand == eEquipment.None)
            return;

        Equipment e = myEquipmentsDict[whatsInHand];
        e.Use(playerAnim);
    }

    public void CycleTools()
    {
        if (toolsList.Count <= 0)
            return;

        eEquipment obj;
        if (whatsInHand != eEquipment.None)
        {
            LinkedListNode<eEquipment> currentNode = toolsList.Find(whatsInHand);

            // ���� ���� enum ���� (���� ��尡 ������ ����̸� ó�� �ɷ� �ٲ���)
            obj = currentNode == toolsList.Last ? toolsList.First.Value : currentNode.Next.Value;
        }
        else
            obj = toolsList.First.Value;

        ChangeEquipment(myEquipmentsDict[obj]);
    }

    public void PickEquipment()
    {
        if (!canPickTool || nearByTool == null)
            return;

        if (toolsList.Count >= toolCountLimit)
            DropEquipment();
        ChangeEquipment(nearByTool);
    }

    private void DropEquipment()
    {
        if (whatsInHand == eEquipment.None)
            return;

        myEquipmentsDict[whatsInHand].Drop();
        myEquipmentsDict.Remove(whatsInHand);
        toolsList.Remove(whatsInHand);
        whatsInHand = eEquipment.None;

        playerAnim.SetBool("isHold", false);
    }

    // ���� �������� obj�� ��ü
    private void ChangeEquipment(Equipment obj)
    {
        playerAnim.SetBool("isHold", true);

        myEquipmentsDict[whatsInHand]?.gameObject.SetActive(false);  // ���� ���� ��Ȱ��ȭ

        whatsInHand = obj.kinds;

        if (!myEquipmentsDict.ContainsKey(obj.kinds)) // obj�� ó�� ���� �������
        {
            obj.Equip(hands);
            myEquipmentsDict.Add(obj.kinds, obj);
            toolsList.AddLast(obj.kinds);
            return;
        }

        myEquipmentsDict[obj.kinds].gameObject.SetActive(true);   // obj Ȱ��ȭ
    }

    IEnumerator NavigateAraund()
    {
        while (playerHealth.IsAlive)
        {
            float dis = 100, temp;

            // PickableMask�� Ž��
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, navRange, pickableLayer);
            foreach (var col in collider2Ds)
            {
                // �� ����� �� ã��
                temp = Vector3.Distance(transform.position, col.transform.position);
                if (dis >= temp)
                {
                    dis = temp;
                    nearByTool = col.GetComponent<Equipment>();
                }
            }

            if (collider2Ds.Length <= 0)
            {
                canPickTool = false;
                nearByTool = null;
            }
            else
                canPickTool = true;
                

            yield return new WaitForSeconds(0.2f);
        }
    }
}
