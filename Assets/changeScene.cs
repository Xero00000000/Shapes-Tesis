using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public string desiredScene;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(desiredScene);
    }
}
