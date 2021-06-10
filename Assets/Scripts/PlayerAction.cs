using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Equipment NearByTool;
    public Transform Hands;

    // private float coolTime, curTime;
    public float navRange;
    private bool canPickEquipment;

    void Awake()
    {
        // coolTime = curTime = 0.8f;
        navRange = 1.2f;
        canPickEquipment = false;
        Hands = transform.GetChild(0).GetChild(1).GetChild(0);
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
        while (true) // 죽은 상태가 아닐 때로 바꾸기
        {
            float dis = navRange, temp;

            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, navRange);
            foreach (var col in collider2Ds)
            {
                if (col.CompareTag("Tool"))
                {
                    canPickEquipment = true;
                    // 더 가까운 것 찾음
                    temp = Vector2.Distance(transform.position, col.transform.position);
                    if (dis > temp)
                    {
                        dis = temp;
                        NearByTool = col.GetComponent<Equipment>();
                    }
                }
            }

            if (collider2Ds.Length <= transform.childCount + 1) // 자기 자신과 자식도 인식해서
            {
                canPickEquipment = false;
                NearByTool = null;
            }
                

            yield return new WaitForSeconds(0.1f);
        }
    }
}
