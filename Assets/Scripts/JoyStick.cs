using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 Dir { get; } // ���̽�ƽ�� ����

    private Transform Stick;
    private Vector2 JoyDirection, StickFirstPos, currentPos;
    private float Radius;   // ���̽�ƽ ����� ���� ������ ��

    void Start()
    {
        Stick = transform.GetChild(0);

        JoyDirection = Vector2.zero;

        // ������ �ʱ�ȭ
        StickFirstPos = currentPos = Stick.transform.position;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.45f;

        // ĵ���� ũ�⿡���� ������ ����.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // < �巡�� �� >
        currentPos = Input.mousePosition;

        if (Input.touches.Length > 1)
        {
            // �� �� �̻� ��ġ
            foreach (Touch touch in Input.touches)
                if (currentPos.x > touch.position.x)
                    currentPos = touch.position;
        }

        JoyDirection = (currentPos - StickFirstPos).normalized;

        float distance = Vector2.Distance(currentPos, StickFirstPos);

        if (distance > Radius)
            currentPos = StickFirstPos + JoyDirection * Radius;
        Stick.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // < �巡�� �� >
        OnPointerUp(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // < ��ġ ���� >
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // < ��ġ �� >
        Stick.transform.position = StickFirstPos;
        JoyDirection = Vector2.zero;
    }
}
