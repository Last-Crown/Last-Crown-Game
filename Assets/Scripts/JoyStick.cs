using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // 조이스틱의 방향
    public Vector2 Dir => JoyDirection;

    private RectTransform Stick;
    private Vector2 JoyDirection;
    public Vector2 StickFirstPos;
    public Vector2 CurrentPos;

    private float Radius;   // 조이스틱 배경의 가로 길이의 반

    private void Awake()
    {
        Stick = transform.GetChild(0).GetComponent<RectTransform>();

        JoyDirection = Vector2.zero;

        // 포지션 초기화
        StickFirstPos = CurrentPos = Stick.localPosition;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.3f;

        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // < 드래그 중 >
        CurrentPos = Input.mousePosition;
        
        if (Input.touches.Length > 1)
        {
            // 두 개 이상 터치
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
        Stick.localPosition = StickFirstPos;
        JoyDirection = Vector2.zero;
    }
}
