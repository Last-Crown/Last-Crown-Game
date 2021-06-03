using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ����� �ڵ��Դϴ�.
/// </summary>
public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform Stick; // ���̽�ƽ
    public Vector2 JoyVec;  // ���̽�ƽ�� ����

    private Vector2 StickFirstPos;
    private Vector2 currentPos;
    private float Radius;           // ���̽�ƽ ����� ���� ������ ��

    void Start()
    {
        if (Stick == null)
            Stick = transform.GetChild(0);

        JoyVec = Vector2.zero;

        // ������ �ʱ�ȭ
        StickFirstPos = currentPos = Stick.transform.position;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.45f;

        // ĵ���� ũ�⿡���� ������ ����.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // < �巡�� �� >
        float touchPosX = Input.mousePosition.x;
        if (Input.touches.Length > 1)
        {
            // �� �� �̻� ��ġ
            foreach (Touch touch in Input.touches)
            {
                if (touchPosX > touch.position.x)
                    touchPosX = touch.position.x;
            }
        }

        currentPos.x = touchPosX;

        JoyVec = (currentPos - StickFirstPos).normalized;

        float Dis = Mathf.Abs(currentPos.x - StickFirstPos.x);

        if (Dis > Radius)
            currentPos.x = StickFirstPos.x + JoyVec.x * Radius;
        Stick.transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        // < �巡�� �� >
        Stick.transform.position = StickFirstPos;
        JoyVec = Vector2.zero;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // < ��ġ ���� >
        currentPos.x = Input.mousePosition.x;
        JoyVec = (currentPos - StickFirstPos).normalized;
        Stick.transform.position = currentPos;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // < ��ġ �� >
        Stick.transform.position = StickFirstPos;
        JoyVec = Vector2.zero;
    }
}
