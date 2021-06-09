using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // ���̽�ƽ�� ����
    public Vector2 Dir => JoyDirection;

    private RectTransform joyStick;
    private RectTransform Stick;
    private Vector2 JoyDirection;

    private float Radius;   // ���̽�ƽ ����� ���� ������ ��

    private void Awake()
    {
        joyStick = GetComponent<RectTransform>();
        Stick = transform.GetChild(0).GetComponent<RectTransform>();

        JoyDirection = Vector2.zero;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.3f;

        // ĵ���� ũ�⿡���� ������ ����.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position - (Vector2)joyStick.position;

        JoyDirection = currentPos.normalized;

        currentPos = Vector2.ClampMagnitude(currentPos, Radius);
        Stick.anchoredPosition = currentPos;
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
        Stick.anchoredPosition = Vector2.zero;
        JoyDirection = Vector2.zero;
    }
}
