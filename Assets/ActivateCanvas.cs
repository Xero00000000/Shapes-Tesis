using UnityEngine;

public class ActivateCanvas : MonoBehaviour
{
    [SerializeField] Canvas Canvas;

    private void OnTriggerEnter(Collider other)
    {
        Canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.gameObject.SetActive(false);
    }

}
