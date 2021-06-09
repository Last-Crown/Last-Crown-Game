using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform actionPoint;
    public Vector2 boxSize;


    private float coolTime, curTime;

    void Awake()
    {
        coolTime = curTime = 0.8f;
    }

    void Update()
    {
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(actionPoint.position, boxSize, transform.GetChild(0).localEulerAngles.z);
                foreach (var collider in collider2Ds)
                {
                    if (collider.CompareTag("Rock"))
                        Debug.Log("Rock");
                }

                curTime = coolTime;
            }
        } else
        {
            curTime -= Time.deltaTime;
        }
    }

    // �ڽ� �ݶ��̴� ���̰� ���ִ� �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(actionPoint.position, boxSize); // �������°� ���� �̻���,,
    }
}
