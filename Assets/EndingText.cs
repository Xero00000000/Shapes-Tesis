using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private Image logo;

    [Header("Messages")]
    [TextArea]
    [SerializeField] private string firstMessage;

    [TextArea]
    [SerializeField] private string secondMessage;

    [Header("Typewriter")]
    [SerializeField] private float letterDelay = 0.05f;

    private IEnumerator Start()
    {
        logo.gameObject.SetActive(false);

        yield return StartCoroutine(TypeText(firstMessage));

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(TypeText(secondMessage));

        yield return new WaitForSeconds(5f);

        // Mostrar logo
        textBox.gameObject.SetActive(false);
        yield return StartCoroutine(FadeLogo());

        yield return new WaitForSeconds(5f);

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator TypeText(string message)
    {
        textBox.text = "";

        foreach (char letter in message)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }
    private IEnumerator FadeLogo()
    {
        Color color = logo.color;
        color.a = 0;
        logo.color = color;

        logo.gameObject.SetActive(true);

        float time = 0;

        while (time < 2f)
        {
            time += Time.deltaTime;

            color.a = time / 2f;
            logo.color = color;

            yield return null;
        }
    }
}