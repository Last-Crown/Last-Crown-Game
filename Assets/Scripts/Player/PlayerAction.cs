using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Equipment nearByTool;

    private Transform hands;
    private Animator playerAnim;
    private PlayerHealth playerHealth;

    public eEquipment whatsInHand = eEquipment.None;
    public LinkedList<eEquipment> toolsList = new LinkedList<eEquipment>();
    public Dictionary<eEquipment, Equipment> myEquipmentsDict = new Dictionary<eEquipment, Equipment>()
    {
        { eEquipment.None, null }
    };

    private float navRange;
    public bool canPickTool;
    private int pickableLayer, toolCountLimit;


    void Awake()
    {
        navRange = 1.2f;
        toolCountLimit = 2;
        canPickTool = false;
        pickableLayer = 1 << LayerMask.NameToLayer("Pickable");
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
        {
            playerAnim.SetTrigger("useHand");
            return;
        }

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

            // 다음 도구 enum 저장 (현재 노드가 마지막 노드이면 처음 걸로 바꿔줌)
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

    // 현재 도구에서 obj로 교체
    public void ChangeEquipment(Equipment obj)
    {
        playerAnim.SetBool("isHold", true);

        myEquipmentsDict[whatsInHand]?.gameObject.SetActive(false);  // 현재 도구 비활성화

        whatsInHand = obj.kinds;

        if (!myEquipmentsDict.ContainsKey(obj.kinds)) // obj가 처음 집은 도구라면
        {
            obj.Equip(transform, hands);
            myEquipmentsDict.Add(obj.kinds, obj);
            toolsList.AddLast(obj.kinds);
            return;
        }

        myEquipmentsDict[obj.kinds].gameObject.SetActive(true);   // obj 활성화
    }

    IEnumerator NavigateAraund()
    {
        while (playerHealth.IsAlive)
        {
            float dis = 100, temp;

            // PickableMask만 탐색
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, navRange, pickableLayer);
            foreach (var col in collider2Ds)
            {
                // 더 가까운 것 찾음
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
