using UnityEngine;
using UnityEngine.EventSystems;

public class PartStatsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DraggablePart draggablePart;
    [SerializeField] private PartStatsPanel statsPanel;

    private void Awake()
    {
        if (draggablePart == null)
            draggablePart = GetComponentInParent<DraggablePart>();

        if (statsPanel == null)
            statsPanel = FindFirstObjectByType<PartStatsPanel>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (draggablePart == null || statsPanel == null) return;

        statsPanel.Show(draggablePart.TheClass, draggablePart.PartValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (statsPanel != null)
            statsPanel.Hide();
    }
}