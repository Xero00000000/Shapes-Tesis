using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

public class PartSlot : MenuSlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggablePart draggableItem = dropped.GetComponent<DraggablePart>();

        draggableItem.SwappingPart();

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
}
