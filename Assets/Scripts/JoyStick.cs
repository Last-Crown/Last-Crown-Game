using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 Dir { get; } // 조이스틱의 방향

    private Transform Stick;
    private Vector2 JoyDirection, StickFirstPos, currentPos;
    private float Radius;   // 조이스틱 배경의 가로 길이의 반

    void Start()
    {
        Stick = transform.GetChild(0);

        JoyDirection = Vector2.zero;

        // 포지션 초기화
        StickFirstPos = currentPos = Stick.transform.position;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.45f;

        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // < 드래그 중 >
        currentPos = Input.mousePosition;

        if (Input.touches.Length > 1)
        {
            // 두 개 이상 터치
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
        Stick.transform.position = StickFirstPos;
        JoyDirection = Vector2.zero;
    }
}
