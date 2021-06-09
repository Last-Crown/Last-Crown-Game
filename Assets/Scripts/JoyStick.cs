using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // ���̽�ƽ�� ����
    public Vector2 Dir => JoyDirection;

    private RectTransform Stick;
    private Vector2 JoyDirection;
    public Vector2 StickFirstPos;
    public Vector2 CurrentPos;

    private float Radius;   // ���̽�ƽ ����� ���� ������ ��

    private void Awake()
    {
        Stick = transform.GetChild(0).GetComponent<RectTransform>();

        JoyDirection = Vector2.zero;

        // ������ �ʱ�ȭ
        StickFirstPos = CurrentPos = Stick.localPosition;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.3f;

        // ĵ���� ũ�⿡���� ������ ����.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // < �巡�� �� >
        CurrentPos = Input.mousePosition;
        
        if (Input.touches.Length > 1)
        {
            // �� �� �̻� ��ġ
            foreach (Touch touch in Input.touches)
                if (CurrentPos.x > touch.position.x)
                    CurrentPos = touch.position;
        }

        JoyDirection = (CurrentPos - StickFirstPos).normalized;

        float distance = Vector2.Distance(CurrentPos, StickFirstPos);

        if (distance > Radius)
            CurrentPos = StickFirstPos + JoyDirection * Radius;
        Stick.localPosition = CurrentPos;
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
        Stick.localPosition = StickFirstPos;
        JoyDirection = Vector2.zero;
    }
}
