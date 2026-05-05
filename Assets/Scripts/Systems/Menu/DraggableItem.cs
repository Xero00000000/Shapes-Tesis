using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Transform parentAfterDrag;

    public void OnPointerDown(PointerEventData eventData)
    {
        HideStatsPanel();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        HideStatsPanel();

        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root, true);
        transform.SetAsLastSibling();

        if (image != null)
            image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag, false);
        transform.localPosition = Vector3.zero;

        if (image != null)
            image.raycastTarget = true;

        HideStatsPanel();
    }

    private void HideStatsPanel()
    {
        PartStatsPanel statsPanel = FindFirstObjectByType<PartStatsPanel>();

        if (statsPanel != null)
            statsPanel.Hide();
    }
}