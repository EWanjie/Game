using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Drag_and_Drop_V2: MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
   // [SerializeField] private Canvas camera;
    private RectTransform RectTransform;
    private CanvasGroup CanvasGroup;
    Vector2 difference = Vector2.zero;

    // ��� ����� ���-�� �� ������
    public void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        //CanvasGroup.alpha = .6f;
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    // ������� ����� �� ������
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    // ������ �������� �������
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
       // CanvasGroup.blocksRaycasts = false; 
    }

    // ������� �������
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    // ����� �������� �������
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
       // CanvasGroup.blocksRaycasts = false;
        //CanvasGroup.alpha = 1f;
    }

}
