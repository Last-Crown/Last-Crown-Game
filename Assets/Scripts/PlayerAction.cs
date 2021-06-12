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

                // 다음 도구 enum 저장 (현재 노드가 마지막 노드이면 처음 걸로 바꿔줌)
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

    // 현재 도구에서 obj로 교체
    private void ChangeEquipment(Equipment obj)
    {
        PlayerAnim.SetBool("isHold", true);

        MyEquipmentsDict[WhatsInHand]?.gameObject.SetActive(false);  // 현재 도구 비활성화

        WhatsInHand = obj.Kinds;
        

        if (!MyEquipmentsDict.ContainsKey(obj.Kinds)) // obj가 처음 집은 도구라면
        {
            obj.Equip(Hands);
            MyEquipmentsDict.Add(obj.Kinds, obj);
            ToolsList.AddLast(obj.Kinds);
            return;
        }

        MyEquipmentsDict[obj.Kinds].gameObject.SetActive(true);   // obj 활성화
    }

    IEnumerator NavigateAraund()
    {
        while (playerHealth.IsAlive)
        {
            float dis = 100, temp;

            // PickableMask만 탐색
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, navRange, PickableLayer);
            foreach (var col in collider2Ds)
            {
                // 더 가까운 것 찾음
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
