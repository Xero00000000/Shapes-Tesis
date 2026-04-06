using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

public class MenuSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {//probablemente sea mejor usar parent constraint component para esto, asi no uso tantos ifs en MenuSlot, despues me fijo
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (transform.childCount != 0)
        {
            GameObject current = transform.GetChild(0).gameObject;
            DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

            currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
        }

        if (dropped.GetComponent<DraggableItem>() != null)
        {
            draggableItem.parentAfterDrag = transform;
            Tween.Scale(transform, endValue: 1f, duration: 0.05f, ease: Ease.InOutQuad);
            Tween.Scale(dropped.transform, endValue: 1f, duration: 0.05f, ease: Ease.InOutQuad);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Tween.Scale(transform, endValue: 1.25f, duration: 0.05f, ease: Ease.InOutQuad);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Tween.Scale(transform, endValue: 1f, duration: 0.05f, ease: Ease.InOutQuad);
        }
    }
}
