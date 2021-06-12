using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // 조이스틱의 방향
    public Vector2 Dir => JoyDirection;

    private RectTransform joyStick;
    private RectTransform Stick;
    private Vector2 JoyDirection;

    private float Radius;   // 조이스틱 배경의 가로 길이의 반

    private void Awake()
    {
        joyStick = GetComponent<RectTransform>();
        Stick = transform.GetChild(0).GetComponent<RectTransform>();

        JoyDirection = Vector2.zero;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.3f;

        // 캔버스 크기에대한 반지름 조절.
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
        // < 드래그 끝 >
        OnPointerUp(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // < 터치 시작 >
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // < 터치 뗌 >
        Stick.anchoredPosition = Vector2.zero;
        JoyDirection = Vector2.zero;
    }
}
