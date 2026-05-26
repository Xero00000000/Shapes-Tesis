using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{
    [Header("Canvas con el logo")]
    public CanvasGroup logoCanvasGroup;

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;

        logoCanvasGroup.alpha = 1f;
        logoCanvasGroup.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        gameStarted = true;

        float duration = 2f;
        float elapsed = 0f;


        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            logoCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            yield return null;
        }

        logoCanvasGroup.alpha = 0f;

        logoCanvasGroup.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}