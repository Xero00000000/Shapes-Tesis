using TMPro;
using UnityEngine;

public class PartStatsPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text partText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text physicalDefenseText;
    [SerializeField] private TMP_Text magicalDefenseText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text physicalAttackText;
    [SerializeField] private TMP_Text magicalAttackText;
    [SerializeField] private TMP_Text dexterityText;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (panel == null)
            panel = gameObject;

        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = panel.AddComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        Hide();
    }

    private void Update()
    {
        if (panel != null && panel.activeSelf)
        {
            Vector2 pos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                Input.mousePosition,
                null,
                out pos
            );

            rectTransform.anchoredPosition = pos + new Vector2(220, -120);
        }
    }

    internal void Show(ClassData data, int part)
    {
        if (data == null) return;

        panel.SetActive(true);

        titleText.text = data.label;
        partText.text = "Parte: " + GetPartName(part);

        hpText.text = "Vida: " + data.HpModifier;
        energyText.text = "Energía: " + data.Energy;
        physicalDefenseText.text = "Defensa física: " + data.PhysicalDefense;
        magicalDefenseText.text = "Defensa mágica: " + data.MagicalDefense;
        speedText.text = "Velocidad: " + data.Speed;
        physicalAttackText.text = "Ataque físico: " + data.PhysicalAttack;
        magicalAttackText.text = "Ataque mágico: " + data.MagicalAttack;
        dexterityText.text = "Destreza: " + data.DexterityValue;
    }

    internal void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    private string GetPartName(int part)
    {
        switch (part)
        {
            case 0: return "Cabeza";
            case 1: return "Pecho";
            case 2: return "Brazos";
            case 3: return "Piernas";
            default: return "Desconocida";
        }
    }
}