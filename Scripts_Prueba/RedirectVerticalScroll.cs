
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RedirectVerticalScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ScrollRect parentScrollRect;
    private Vector2 dragStartPos;
    private bool isVerticalDrag;

    void Start()
    {
        parentScrollRect = FindVerticalParentScrollRect();
    }

    private ScrollRect FindVerticalParentScrollRect()
    {
        Transform current = transform.parent;

        while (current != null)
        {
            ScrollRect scroll = current.GetComponent<ScrollRect>();
            if (scroll != null && scroll.vertical)
                return scroll;

            current = current.parent;
        }

        return null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = eventData.position;
        isVerticalDrag = false;
        Debug.Log($"[RedirectVerticalScroll] Drag started on: {gameObject.name}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragDelta = eventData.position - dragStartPos;

        if (Mathf.Abs(dragDelta.y) > Mathf.Abs(dragDelta.x))
        {
            isVerticalDrag = true;

            // ¡Cancelamos el drag en este scroll para no bloquear el vertical!
            GetComponent<ScrollRect>().OnEndDrag(eventData);

            if (parentScrollRect != null)
            {
                Debug.Log("[RedirectVerticalScroll] Redirecting to parent vertical scroll");
                parentScrollRect.OnBeginDrag(eventData);
                parentScrollRect.OnDrag(eventData);
            }
        }
        else
        {
            // Movimiento horizontal normal
            GetComponent<ScrollRect>().OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isVerticalDrag)
        {
            if (parentScrollRect != null)
                parentScrollRect.OnEndDrag(eventData);
        }
        else
        {
            GetComponent<ScrollRect>().OnEndDrag(eventData);
        }
    }
}
