using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicScrollDirection : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScrollRect verticalScrollRect;
    public List<ScrollRect> horizontalCarrusels;

    private Vector2 dragStartPos;
    private bool isVertical = false;
    private bool raycastWasDisabled = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = eventData.position;
        isVertical = false;
        raycastWasDisabled = false;
        Debug.Log("[ScrollDirection] Begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragDelta = eventData.position - dragStartPos;

        // Detecta dirección principal del gesto
        if (!isVertical && Mathf.Abs(dragDelta.y) > Mathf.Abs(dragDelta.x))
        {
            isVertical = true;
            Debug.Log("[ScrollDirection] Vertical gesture detected");

            // Activar scroll vertical
            if (verticalScrollRect != null)
            {
                verticalScrollRect.vertical = true;
                verticalScrollRect.OnBeginDrag(eventData);
                verticalScrollRect.OnDrag(eventData);
            }

            // Desactivar scroll horizontal y raycast de los carruseles
            foreach (var hScroll in horizontalCarrusels)
            {
                if (hScroll != null)
                {
                    hScroll.horizontal = false;
                    SetRaycastTargets(hScroll.gameObject, false);
                    raycastWasDisabled = true;
                    Debug.Log($"[ScrollDirection] Raycast disabled on {hScroll.name}");
                }
            }
        }

        // Si es horizontal, dejamos los carruseles activos como están
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("[ScrollDirection] End drag");

        if (isVertical)
        {
            if (verticalScrollRect != null)
                verticalScrollRect.OnEndDrag(eventData);
        }

        foreach (var hScroll in horizontalCarrusels)
        {
            if (hScroll != null)
                hScroll.OnEndDrag(eventData);
        }

        // Restaurar raycast y scroll horizontal
        if (raycastWasDisabled)
        {
            foreach (var hScroll in horizontalCarrusels)
            {
                if (hScroll != null)
                {
                    hScroll.horizontal = true;
                    SetRaycastTargets(hScroll.gameObject, true);
                    Debug.Log($"[ScrollDirection] Raycast re-enabled on {hScroll.name}");
                }
            }
        }

        // Restaurar scroll vertical por si acaso
        if (verticalScrollRect != null)
            verticalScrollRect.vertical = true;
    }

    private void SetRaycastTargets(GameObject root, bool state)
    {
        Graphic[] graphics = root.GetComponentsInChildren<Graphic>(true);
        foreach (Graphic g in graphics)
        {
            g.raycastTarget = state;
        }
    }
}
